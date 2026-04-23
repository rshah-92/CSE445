using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WeatherService
{
    [ServiceContract]
    public interface IWeatherService
    {
        [OperationContract]
        string GetWeather(string city, string date);
    }
}
