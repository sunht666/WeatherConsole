using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherConsole.Model
{
    public class WeatherModel
    {
        /// <summary>
        /// 更新时间
        /// </summary>
        public void Update()
        { 
            this.Time = DateTime.Now;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// 获取时间
        /// </summary>
        public DateTime Time { get; set; } = DateTime.Now;

        /// <summary>
        /// 体感温度
        /// </summary>
        public double? Temperature { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public double? Humidity { get; set; }

        /// <summary>
        /// 风向
        /// </summary>
        public string? WindDirection { get; set; }

        /// <summary>
        /// 风级
        /// </summary>
        public int? WindLevel { get; set; }

        /// <summary>
        /// 能见度距离
        /// </summary>
        public double VisibilityDistance { get; set; }

        /// <summary>
        /// AQI
        /// </summary>
        public double? AQI { get; set; }
        
        /// <summary>
        /// 空气质量
        /// </summary>
        public string? WeatherQuality { get; set; }

        /// <summary>
        /// PM2.5
        /// </summary>
        public double? PM25 { get; set; }

        /// <summary>
        /// PM10
        /// </summary>
        public double? PM10 { get; set; }

        /// <summary>
        /// SO2
        /// </summary>
        public double? SO2 { get; set; }

        /// <summary>
        /// NO2
        /// </summary>
        public double? NO2 { get; set; }
        
        /// <summary>
        /// NO
        /// </summary>
        public double? NO { get; set; }

        /// <summary>
        /// O3
        /// </summary>
        public double? O3 { get; set; }
    }

    public class WeatherHistoryModel
    {
        /// <summary>
        /// 时间
        /// </summary>
        [Description("时间")]
        public DateTime time { get; set; }

        /// <summary>
        /// AQI
        /// </summary>
        [Description("AQI")]
        public double aqi { get; set; }

        /// <summary>
        /// PM2.5
        /// </summary>
        [Description("PM2.5")]
        public double pm2_5 { get; set; }

        /// <summary>
        /// PM10
        /// </summary>
        [Description("PM10")]
        public double pm10 { get; set; }

        /// <summary>
        /// CO
        /// </summary>
        [Description("CO")]
        public double co { get; set; }

        /// <summary>
        /// NO2
        /// </summary>
        [Description("NO2")]
        public double no2 { get; set; }

        /// <summary>
        /// O3
        /// </summary>
        [Description("O3")]
        public double o3 { get; set; }

        /// <summary>
        /// SO2
        /// </summary>
        [Description("SO2")]
        public double so2 { get; set; }

        /// <summary>
        /// 复合指数
        /// </summary>
        [Description("复合指数")]
        public string complexindex { get; set; }

        /// <summary>
        /// 主要指数
        /// </summary>
        [Description("主要指数")]
        public string rank { get; set; }

        /// <summary>
        /// 主要污染物
        /// </summary>
        [Description("主要污染物")]
        public string primary_pollutant { get; set; }

        /// <summary>
        /// 体感温度
        /// </summary>
        [Description("体感温度")]
        public string temp { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        [Description("湿度")]
        public string humi { get; set; }

        /// <summary>
        /// 风速
        /// </summary>
        [Description("风速")]
        public string windlevel { get; set; }

        /// <summary>
        /// 风向
        /// </summary>
        [Description("风向")]
        public string winddirection { get; set; }

        /// <summary>
        /// 天气
        /// </summary>
        [Description("天气")]
        public string weather { get; set; }
    }
}
