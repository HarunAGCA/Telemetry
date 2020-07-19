using System;
using System.Collections.Generic;
using Futek.Telemetry.Business.Abstract;
using Futek.Telemetry.Hardware;
using Futek.Telemetry.Hardware.Abstract;
using Futek.Telemetry.MvcWebUI.Dtos;
using Futek.Telemetry.MvcWebUI.Models;
using Futek.Telemetry.MvcWebUI.SignalRHubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Futek.Telemetry.MvcWebUI.Controllers
{
    [Route("[controller]")]
    public class ConnectionController : Controller
    {

        private SerialDataReceiverBase _dataReceiver;
        private IHubContext<SensorValuesHub> _hubContext;
        private ISensorValueService _sensorValueService;
        private SensorValuesDto _sensorValuesDto;

        public ConnectionController(ISensorValueService sensorValueServie, IHubContext<SensorValuesHub> hubContext, SerialDataReceiverBase dataReceiver, SensorValuesDto sensorValuesDto)
        {
            _sensorValueService = sensorValueServie;
            _hubContext = hubContext;
            _dataReceiver = dataReceiver;
            _sensorValuesDto = sensorValuesDto;
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult Configure(ConfigurationViewModel configurationViewModel)//string portName, int baudRate)
        {
            if (!_dataReceiver.GetSerialPort().IsOpen)
            {
                _dataReceiver.SetPortName(configurationViewModel.PortName);
                _dataReceiver.SetBaudRate(configurationViewModel.BaudRate);
                _dataReceiver.ReceivingDataParsed += OnReceivingDataParsed;
            }

            return RedirectToAction("Charts");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Charts()
        {
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Connect()
        {
            if (!_dataReceiver.IsConnected)
            {

                if (_dataReceiver.Connect())
                {
                    return View("charts");

                }
                else
                {
                    return StatusCode(500);
                }

            }
            else
            {
                return Ok("Already connected.");
            }

        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Disconnect()
        {
            if (_dataReceiver.IsConnected)
            {
                _dataReceiver.Disconnect();
                return Ok("Disconnected");
            }
            else
            {
                return Ok("Already disconnected.");
            }
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult TestTheCommunication()
        {
            if (_dataReceiver.TestTheCommunication())
            {
                return View("charts");
            }
            else
            {
                return BadRequest("Test Communication Failed.");
            }
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult StartReceivingData()
        {
            _dataReceiver.StartReceivingData();
            return View("charts");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult StopReceivingData()
        {
            _dataReceiver.StopReceivingData();
            return View("charts");
        }


        private void OnReceivingDataParsed(object sender, SensorValuesEventArgs args)
        {
            _sensorValuesDto.SensorValues = args.SensorValues;
            _hubContext.Clients.All.SendAsync("receiveValues", _sensorValuesDto);
            _sensorValueService.AddRangeAsync(_sensorValuesDto.SensorValues);
        }

    }
}