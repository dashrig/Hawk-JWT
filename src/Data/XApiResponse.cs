namespace HawkMiddlewares.Data
{
    public class XAuthResponse<T> where T : class
    {
        public XAuthState Status { get; set; }

        public string XAuth { get; set; }
        



    }

    
}
