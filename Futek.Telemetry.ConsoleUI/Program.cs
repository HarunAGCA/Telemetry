using Futek.Telemetry.Business.Abstract;
using Futek.Telemetry.Business.Concrete;
using Futek.Telemetry.DataAccess.Concrete.EntityFramework;
using Futek.Telemetry.Hardware;
using Futek.Telemetry.Hardware.Abstract;
using Futek.Telemetry.Hardware.Concrete.SerialPortReceiver;
using System;
using System.IO.Ports;
using System.Linq;

namespace Futek.Telemetry.ConsoleUI
{
    class Program
    {
        private ISensorValueService _sensorValueService = new SensorValueManager(new EfSensorValueDal());
        private ISensorService _sensorService = new SensorManager(new EfSensorDal());

        static void Main(string[] args)
        {


            SerialPort serialPort = new SerialPort
            {
                PortName = "COM5",
                BaudRate = 9600
            };

            SerialDataReceiverBase dataReceiver = new SerialPortDataReceiver();
            dataReceiver.SetPortName( serialPort.PortName);
            dataReceiver.SetBaudRate(serialPort.BaudRate);
            Console.WriteLine("Bağlanıyor...");
            bool a = dataReceiver.Connect();
            while (dataReceiver.IsConnected == false) {  }
            if (dataReceiver.IsConnected)
            {
                Console.WriteLine("Bağlantı başarılı.");
                Console.WriteLine("Test iletişimi sağlanıyor...");
                if (dataReceiver.TestTheCommunication())
                {
                    Console.WriteLine("Test iletişimi başarılı.");
                }
                else
                {
                    Console.WriteLine("Test iletişimi kurulurken bir hata oluştu!");
                }

                Console.WriteLine("Veriler alınıyor...");
                dataReceiver.StartReceivingData();
                dataReceiver.ReceivingDataParsed += new Program().OnReceivingDataParsed;

                if (Console.ReadLine() == "q")
                {
                    dataReceiver.Disconnect();
                    Console.WriteLine("Bağlantı kapatıldı.");
                }

            }
            else
            {
                Console.WriteLine("Bağlantı başarısız.");
            }


        }

        private void OnReceivingDataParsed(object sender, SensorValuesEventArgs args)
        {
            Console.WriteLine(args.SensorValues.ElementAt(0).SensorId + " > " + _sensorService.GetSensor(args.SensorValues.ElementAt(0).SensorId).Name + " >>" + args.SensorValues.ElementAt(0).Value + " >>>" + args.SensorValues.ElementAt(0).ValueReadingTime);
            Console.WriteLine(args.SensorValues.ElementAt(1).SensorId + " > " + _sensorService.GetSensor(args.SensorValues.ElementAt(1).SensorId).Name + " >>" + args.SensorValues.ElementAt(1).Value + " >>>" + args.SensorValues.ElementAt(1).ValueReadingTime);
            foreach (var value in args.SensorValues)
            {
                _sensorValueService.AddValue(value);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
