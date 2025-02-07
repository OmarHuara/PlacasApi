using System.Dynamic;
using System.Net;
using PlacasAPI.Models;

namespace PlacasAPI.Dtos
{
    public class ResponseGeneric<T> where T : class
    {
        public HttpStatusCode HttpCode { get; set; }
        public T? ReturnData { get; set; }
        public ExpandoObject? ReturnError { get; set; }

        public ResponseGeneric<T> WithData(T data)
        {
            this.ReturnData = data;
            this.HttpCode = HttpStatusCode.OK;
            return this;
        }

        public ResponseGeneric<T> WithError(ExpandoObject error)
        {
            this.ReturnError = error;
            this.HttpCode = HttpStatusCode.BadRequest;
            return this;
        }
    }
}
