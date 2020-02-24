using Application.Interfaces.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Providers
{
    public class UrlProvider : IUrlProvider
    {
        private readonly IUrlHelper urlHelper;

        private HttpRequest Request
        {
            get
            {
                return urlHelper.ActionContext.HttpContext.Request;
            }
        }

        public UrlProvider(IUrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        public string GetAbsoluteUrl(string action, string controller, object values)
        {
            return GetBaseUrl() + urlHelper.Action(action, controller, values);
        }

        public string GetBaseUrl()
        {
            return $"{Request.Scheme}://{Request.Host}";
        }
    }
}
