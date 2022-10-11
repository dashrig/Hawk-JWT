namespace HawkMiddlewares
{
    public class JwtOptions
    {
        public string TokenKey { get;  set; }
        public string TokenIssuer { get;  set; }
        public string TokenAudience { get;  set; }
        public double TokenDefaultExpire { get; set; }
        public double CheckUserInterval { get; set; }
        public bool CheckUserExist { get; set; }
    }
}


