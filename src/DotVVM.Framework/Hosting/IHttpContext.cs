﻿using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DotVVM.Framework.Hosting
{
    public interface IHttpContext
    {
        ClaimsPrincipal User { get; set; }
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
        IAuthentication Authentication { get; }
        T GetItem<T>(string key);
        void SetItem<T>(string key, T value);
        IEnumerable<Tuple<string, IEnumerable<KeyValuePair<string, object>>>> GetEnvironmentTabs();
    }
}