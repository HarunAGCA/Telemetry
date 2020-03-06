using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futek.Telemetry.Business.Abstract;
using Futek.Telemetry.Entities;
using Futek.Telemetry.Entities.ComplexTypes;
using Futek.Telemetry.MvcWebUI.Dtos;
using Futek.Telemetry.MvcWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Futek.Telemetry.MvcWebUI.Controllers
{
    public class ChartsController : Controller
    {

        private ISensorValueService _sensorValueService;

        public ChartsController(ISensorValueService sensorValueService)
        {
            _sensorValueService = sensorValueService;
        }

        public IActionResult Index()

        {

            Random random = new Random();
            int randomSpeed = random.Next(180);
            int randomTemprature = random.Next(150);

            _sensorValueService.AddValue(new SensorValue
            {
                SensorId = 1,
                Value = randomSpeed,
                ValueReadingTime = DateTime.Now.AddSeconds(20)
            });
            _sensorValueService.AddValue(new SensorValue
            {
                SensorId = 2,
                Value = randomTemprature,
                ValueReadingTime = DateTime.Now.AddSeconds(20)
            });


            List<SensorValue> speedSensorValues = _sensorValueService.GetValues(1).OrderByDescending(sv=>sv.ValueReadingTime).Take(5).Reverse().ToList();
            List<SensorValue> tempratureSensorValues = _sensorValueService.GetValues(2).OrderByDescending(sv => sv.ValueReadingTime).Take(5).Reverse().ToList();

            var chartDto = new ChartDto
            {
                InstantSpeedChartModel = new InstantSpeedChartModel { ChartLabel = "Hız", Value = 80 },
                InstantTempratureChartModel = new InstantTempratureChartModel { ChartLabel = "Motor Sıcaklığı", Value = 40 },
                SpeedHistoryChartModel = new SpeedHistoryChartModel
                {
                    ChartLabel = "Hız Grafiği",
                    XAxisLabel = "Zaman",
                    YAxisLabel = "Hız",
                    Values = TakeValues(speedSensorValues),
                    ReadingTimes = TakeReadingTimes(speedSensorValues)
                    
                },
                TempratureHistoryChartModel = new TempratureHistoryChartModel
                {
                    ChartLabel = "Sıcaklık Grafiği",
                    XAxisLabel = "Zaman",
                    YAxisLabel = "Sıcaklık",
                    Values = TakeValues(tempratureSensorValues),
                    ReadingTimes = TakeReadingTimes(tempratureSensorValues)

                }
            };

            ViewBag.JsonInstantSpeedValue = JsonConvert.SerializeObject(randomSpeed);// chartDto.InstantSpeedChartModel.Value);
            ViewBag.JsonInstantTempratureValue = JsonConvert.SerializeObject(randomTemprature);//chartDto.InstantTempratureChartModel.Value);

            ViewBag.JsonSpeedHistoryLabel = JsonConvert.SerializeObject(chartDto.SpeedHistoryChartModel.ChartLabel);
            ViewBag.JsonSpeedHistoryValues = JsonConvert.SerializeObject(chartDto.SpeedHistoryChartModel.Values);
            ViewBag.JSonSpeedHistoryLabels = JsonConvert.SerializeObject(chartDto.SpeedHistoryChartModel.ReadingTimes);

            ViewBag.JsonTempratureHistoryLabel = JsonConvert.SerializeObject(chartDto.TempratureHistoryChartModel.ChartLabel);
            ViewBag.JsonTempratureHistoryValues = JsonConvert.SerializeObject(chartDto.TempratureHistoryChartModel.Values);
            ViewBag.JSonTempratureHistoryLabels = JsonConvert.SerializeObject(chartDto.TempratureHistoryChartModel.ReadingTimes);


            return View();
        }


        List<float> TakeValues(List<SensorValue> sensorValues)
        {
            List<float> values = new List<float>();

            sensorValues.ForEach(sv =>
            {
                values.Add(sv.Value);
            });

            return values;
        }
        List<DateTime> TakeReadingTimes(List<SensorValue> sensorValues)
        {
            List<DateTime> readingTimes = new List<DateTime>();
            sensorValues.ForEach(sv =>
            {
                readingTimes.Add(sv.ValueReadingTime);
               
            });

            return readingTimes;
        }

    }
}