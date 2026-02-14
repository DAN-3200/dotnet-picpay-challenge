namespace PicPay.Application.Dtos;

public static class MapJson
{
    public record AuthorizationTransactionRes(
        string Status,
        AuthorizationData Data
    );

    public record AuthorizationData(
        bool Authorization
    );

}