FROM mcr.microsoft.com/dotnet/sdk:10.0

# Instalar ferramentas adicionais úteis (Git, curl, procps para debugging)
RUN apt-get update && apt-get install -y --no-install-recommends \
   git \
   curl \
   procps \
   unzip \
   && rm -rf /var/lib/apt/lists/*

RUN useradd -m vscode
USER vscode