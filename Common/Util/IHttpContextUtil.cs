using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Util
{
    public interface IHttpContextUtil
    {
        HttpContext GetContext();
        void SetToItems(string key, object value);
        T GetFromItems<T>(string key) where T : class;
        void SetToSession(string key, object value);
        T GetFromSession<T>(string key) where T : class;
    }
}
