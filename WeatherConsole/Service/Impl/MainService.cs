using Furion.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherConsole.Service.Interface;

namespace WeatherConsole.Service.Impl
{
    public class MainService : IMainService, IScoped
    {
        private readonly IWeatherService _weatherService;

        public MainService(IWeatherService weatherService) 
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// 跑主服务
        /// </summary>
        public void Run(string cityName, DateTime startTime, DateTime endTime)
        {
            Task.Factory.StartNew(async () => 
            {
                await _weatherService.ExportCityWeatherAsync(cityName, startTime, endTime);
            });
        }
    }
}
