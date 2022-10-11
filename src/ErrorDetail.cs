namespace HawkMiddlewares
{
    public class ErrorDetail
    {
        public ErrorDetail()
        {
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ErrorId { get; set; }
        public DateTime Date { get; set; }
    }
}


