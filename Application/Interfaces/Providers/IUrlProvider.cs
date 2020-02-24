using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Providers
{
    public interface IUrlProvider
    {
        string GetBaseUrl();
        string GetAbsoluteUrl(string action, string controller, object values);
    }
}
