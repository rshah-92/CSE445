using System.ServiceModel;

namespace Project5_Service
{
    [ServiceContract]
    public interface IWeatherService
    {
        [OperationContract]
        string GetWeather(string city, string date);
    }
}
