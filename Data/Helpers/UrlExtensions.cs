using Data.Models.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

public static class UrlExtensions
{
    public static string GetPageLink(this HttpRequest httpRequest, int page, int? pageSize = null, string pageParamName = "page", string pageSizeParamName = "pagesize")
    {
        var queryParams = HttpUtility.ParseQueryString(httpRequest.QueryString.ToString());
        if (queryParams[pageParamName] != null)
        {
            queryParams[pageParamName] = page.ToString();
        }
        else
        {
            queryParams.Add(pageParamName, page.ToString());
        }

        if(pageSize != null)
        {
            if (queryParams[pageSizeParamName] != null)
            {
                queryParams[pageSizeParamName] = pageSize.ToString();
            }
            else
            {
                queryParams.Add(pageSizeParamName, pageSize.ToString());
            }
        }

        return $"{httpRequest.Path}?{queryParams.ToString()}";
    }
}