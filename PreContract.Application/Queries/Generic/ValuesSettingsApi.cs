using System;
using System.Collections.Generic;
using System.Text;

namespace PreContracts.Api.Application.Queries.Generic
{
    public class ValuesSettingsApi : IValuesSettingsApi
    {
        public string _timeZone { get; private set; }

        public ValuesSettingsApi(string timeZone)
        {
            _timeZone = timeZone;
        }

        public string GetTimeZone()
        {
            return _timeZone;
        }
    }

    public interface IValuesSettingsApi
    {
        string GetTimeZone();
    }
}