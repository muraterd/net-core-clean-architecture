using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MediatR.Base.Queries
{
    public class PageQuery<T> : IRequest<T>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
