using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib.DataTransferStruct
{
    public class DataControl : ISerilizeToByteArray
    {
        public Dictionary<MoveEnum, float> Move = new();
        public Dictionary<HandEnum, float> Hand = new();
        public float[] LowPWM = { 0, 0, 0, 0 };
        public bool Stabilization = false;
        public float[] StabilizationTarget = { 0, 0, 0, 0 };

        public byte[] ToArray()
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);

            foreach (var move in Move.Values)
            {
                writer.Write(move);
            }

            foreach (var hand in Hand.Values)
            {
                writer.Write(hand);
            }

            foreach (var pwm in LowPWM)
            {
                writer.Write(pwm);
            }

            writer.Write(Stabilization);

            foreach (var stab in StabilizationTarget)
            {
                writer.Write(stab);
            }

            return stream.ToArray();
        }
    }
}
