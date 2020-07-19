using Futek.Telemetry.Entities;
using Futek.Telemetry.Hardware.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;

namespace Futek.Telemetry.Hardware.Concrete.SerialPortReceiver
{
    public class SerialPortDataReceiver : SerialDataReceiverBase, ISerialDataReceiver
    {
        public SerialPortDataReceiver()
        {
            _serialPort = new SerialPort();
            _serialPort.DataReceived += DataReceivedEvent;
            _serialPort.ErrorReceived += ErrorReceivedEvent;
        }

        public override bool Connect()
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                _serialPort.Write("CONNECT#");


                Stopwatch sw = new Stopwatch();
                sw.Start();

                while (!IsConnected)
                {
                    if (sw.ElapsedMilliseconds > ConnectionTimeout)
                    {
                        sw.Stop();
                        _serialPort.Close();
                        IsConnected = false;
                        return IsConnected;
                    }
                } // wait until connected

                return IsConnected;

            }
            catch (Exception e)
            {
                throw new Exception("Connection cannot be accomplished.");
            }
        }
        public override bool Disconnect()
        {
            try
            {
                if (IsDataReceiving)
                {
                    StopReceivingData();
                }

                if (_serialPort.IsOpen)
                {
                    _serialPort.Write("DISCONNECT#");

                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    while (IsConnected)
                    {

                        if (sw.ElapsedMilliseconds > ConnectionTimeout)
                        {
                            sw.Stop();
                            return false;
                        }

                    } //wait until the connection is closed
                    _serialPort.Close();
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                throw new Exception("An Error occurred while closing the port");
            }
        }
        public override bool TestTheCommunication()
        {
            _serialPort.Write("TEST#");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (!IsTestOk)
            {
                if (sw.ElapsedMilliseconds > DataReceivingTimeout)
                {
                    sw.Stop();
                    IsTestOk = false;
                    return false;
                }
            }//wait until test data is received

            return IsTestOk;

        }
        public override void StartReceivingData()
        {
            _serialPort.Write("SEND#");
        }
        public override bool StopReceivingData()
        {
            try
            {
                if (IsDataReceiving)
                {
                    _serialPort.Write("STOP#");

                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    while (IsDataReceiving)
                    {

                        if (sw.ElapsedMilliseconds > ConnectionTimeout)
                        {
                            sw.Stop();
                            return false;
                        }

                    } //wait until the connection is closed
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                throw new Exception("An Error occurred while closing the port");
            }
            
        }

        //TODO These functions will be refactor

        /// <summary>
        /// For Simulation
        /// </summary>
        /// <param name="rawString"></param>
        public override void ParseİncomingValues(string rawString)
        {
            string[] rawValues = rawString.Substring(1).Split('/');

            List<SensorValue> sensorValues = new List<SensorValue>();

            foreach (var item in rawValues)
            {
                string[] nameValuePair = item.Split(':');
                sensorValues.Add(new SensorValue
                {
                    SensorId = Convert.ToInt32(nameValuePair[0]),
                    Value = Convert.ToDouble(nameValuePair[1]),
                    ValueReadingTime = DateTime.Now
                });
            }

            OnRecevingDataParsed(sensorValues);

        }
        /// <summary>
        /// For Real Communication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       /* public override void ParseİncomingValues(string rawString)
        {
            string rawValue = rawString.Substring(1);


            List<SensorValue> sensorValues = new List<SensorValue>();

      
                string[] nameValuePair = rawValue.Split(':');
                sensorValues.Add(new SensorValue
                {
                    SensorId = Convert.ToInt32(nameValuePair[0]),
                    Value = Convert.ToDouble(nameValuePair[1]),
                    ValueReadingTime = DateTime.Now
                });
            

            OnRecevingDataParsed(sensorValues);

        }*/

        #region Events

        private void DataReceivedEvent(object sender, SerialDataReceivedEventArgs e)
        {
            // for example ==> $1:80/2:35#
            // $  ==> Start Sign
            // #  ==> End Sign
            // 1  ==> Speed Sensor Id , 2 ==> Temprature Sensor Id
            // 50 ==> Speed Value, 35 ==> Temprature Value
            string incomingData = _serialPort.ReadTo("#");
            if (incomingData == "CONNECTED")
            {
                IsConnected = true;
            }
            else if (incomingData == "DISCONNECTED")
            {
                IsConnected = false;
            }
            else if (incomingData == "TESTOK")
            {
                IsTestOk = true;
            }else if(incomingData == "STOPPED")
            {
                IsDataReceiving = false;
            }

            else if (incomingData.StartsWith('$'))
            {
                IsDataReceiving = true;
                ParseİncomingValues(incomingData);
            }
            else
            {
                 new Exception("Values are not recognized.");
            }

        }
        private void ErrorReceivedEvent(object sender, SerialErrorReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
