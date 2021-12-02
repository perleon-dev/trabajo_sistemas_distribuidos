namespace Contracts.Api.Application.Queries.Generic
{
    public class ValuesSettingsLambda : IValuesSettingsLambda
    {
        private readonly string _addressToMarketPlace;
        private readonly string _addressFrom;
        private readonly string _timeZone;
        private readonly string _addressToIdSummaMarketPlace;

        public ValuesSettingsLambda(string addressToMarketPlace, string addressFrom, string timeZone, string addressToIdSummaMarketPlace)
        {
            this._addressFrom = addressFrom;
            this._addressToMarketPlace = addressToMarketPlace;
            this._timeZone = timeZone;
            this._addressToIdSummaMarketPlace = addressToIdSummaMarketPlace;
        }

        public string GetAddressFrom()
        {
            return _addressFrom;
        }

        public string GetAddressToMarketPlace()
        {
            return _addressToMarketPlace;
        }

        public string GetTimeZone()
        {
            return _timeZone;
        }

        public string GetAddressToIdSummaMarketPlace()
        {
            return _addressToIdSummaMarketPlace;
        }
    }

    public interface IValuesSettingsLambda 
    {
        string GetAddressToMarketPlace();
        string GetAddressFrom();
        string GetTimeZone();
        string GetAddressToIdSummaMarketPlace();
    }

}
