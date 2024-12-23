﻿namespace HelpDeskHQ.Core.Helpers
{
    public class Response<T> where T : class
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
