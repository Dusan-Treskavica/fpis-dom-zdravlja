using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.Util
{
    public class HttpContextUtil : IHttpContextUtil
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpContextUtil(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public HttpContext GetContext()
        {
            return httpContextAccessor.HttpContext;
        }

        public void SetToItems(string key, object value)
        {
            this.httpContextAccessor.HttpContext.Items.Add(key, value);
        }

        public T GetFromItems<T>(string key) where T : class
        {
            HttpContext context = this.GetContext();
            T result = null;
            if (context != null && context.Items[key] != null && context.Items[key] is T)
            {
                result = (T)context.Items[key];
            }

            return result;
        }

        public void SetToSession(string key, object value)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            this.httpContextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value, settings));
        }

        public T GetFromSession<T>(string key) where T : class
        {
            HttpContext context = this.GetContext();
            T result = null;
            if (context != null)
            {
                var value = context.Session.GetString(key);
                return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }

            return result;
        }

    }
}
