# -- Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /source

# - cache dependencies stage
COPY main/*.csproj ./main/
COPY tests/*.csproj ./tests/
COPY PicPay.slnx ./
RUN dotnet restore PicPay.slnx

COPY . .

RUN dotnet publish PicPay.slnx \
   --configuration Release \
   --no-restore \
   --output /app 

# -- Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "PicPay.dll"]


