namespace HelpDeskHQ.Core.Helpers
{
    public class Response<T> : Response where T : class
    {
        public T Data { get; set; }
    }
}
