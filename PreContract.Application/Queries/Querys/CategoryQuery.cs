using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Api.Application.Queries.Querys
{
    public class CategoryQuery
    {
        public int? categoryId { get; set; }
        public int? typeSeller { get; set; }
        public string categoryCode { get; set; }
        public string sortProperty { get; set; }
        public int? level { get; set; }
    }
}
