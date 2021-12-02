using System.Diagnostics.CodeAnalysis;

namespace Contracts.Api.Application.Queries.Generic
{
    [ExcludeFromCodeCoverage]
    public class ValuesSettings : IValuesSettings
    {
        public string _timeZone { get; private set; }
        public string _addressSender { get; private set; }
        public string _bucketNamePreContractExcel { get; set; }
        public string _bucketNamePreContractJson { get; set; }
        public string _bucketNamePreContractTemplate { get; set; }


        public ValuesSettings(string timeZone, string addressSender, string bucketNamePreContractExcel, string bucketNamePreContractJson, string bucketNamePreContractTemplate)
        {
            _timeZone = timeZone;
            _addressSender = addressSender;
            _timeZone = timeZone;
            _bucketNamePreContractExcel = bucketNamePreContractExcel;
            _bucketNamePreContractJson = bucketNamePreContractJson;
            _bucketNamePreContractTemplate = bucketNamePreContractTemplate;
        }

        public string GetTimeZone()
        {
            return _timeZone;
        }

        public string GetAddressSender()
        {
            return _addressSender;
        }

        public string getBucketNamePreContractExcel()
        {
            return _bucketNamePreContractExcel;
        }

        public string getBucketNamePreContractJson()
        {
            return _bucketNamePreContractJson;
        }

        public string getBucketNamePreContractTemplate()
        {
            return _bucketNamePreContractTemplate;
        }

    }

    public interface IValuesSettings
    {
        string GetTimeZone();
        string GetAddressSender();
        string getBucketNamePreContractExcel();
        string getBucketNamePreContractJson();
        string getBucketNamePreContractTemplate();
    }

}