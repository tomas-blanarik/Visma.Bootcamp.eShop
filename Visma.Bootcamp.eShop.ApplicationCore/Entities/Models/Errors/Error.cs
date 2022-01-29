namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Models.Errors
{
    public abstract class Error
    {
        protected Error(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public int StatusCode { get; }
        public string Message { get; set; }
#if DEBUG
        public string DeveloperMessage { get; set; }
#endif
    }
}
