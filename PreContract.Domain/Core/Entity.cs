using System;

namespace Contracts.Api.Domain.Core
{
    public class Entity
    {
        public DateTime? DateRegister { get; set; }
        public string RegisterCodeUserRP { get; set; }
        public string UserFullName { get; set; }
    }
}
