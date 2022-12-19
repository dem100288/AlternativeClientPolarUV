using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib.DataTransferStruct
{
    public class SensorData
    {
        public float[] Rotation = { 0, 0, 0, 0};
        public float[] Acceleration = { 0, 0, 0 };

        public float Depth = 0.0f;

        public float Pressure = 0.0f;

        public float BatteryVoltage = 0.0f;

        public byte[] MotionCalibration = { 0, 0, 0, 0 };

        public bool Euler = true;

        public static SensorData GetDataFromBytes(byte[] bytes)
        {
            var reader = new BinaryReader(new MemoryStream(bytes));
            float[] rotation = { reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle() };
            float[] acceleration = { reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle() };
            byte[] motionCalibration = { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
            SensorData data = new SensorData()
            {
                Rotation = rotation,
                Acceleration = acceleration,
                Depth = reader.ReadSingle(),
                Pressure = reader.ReadSingle(),
                BatteryVoltage = reader.ReadSingle(),
                MotionCalibration = motionCalibration,
                Euler = reader.ReadBoolean(),
            };
            return data;
        }
    }
}
