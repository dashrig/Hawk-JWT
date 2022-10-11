namespace HawkMiddlewares.Data
{
    public enum XAuthState
    {
        Unknown = 0,
        Ok = 200,
        Error = -1,
        Unauthorized = 401,
        ResourceDenied = 403,
        ServerError = 502,
        BadRequest = 402
    }
}
