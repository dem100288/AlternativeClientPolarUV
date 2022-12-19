using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib
{
    public class ZeroDataObject : ISerilizeToByteArray
    {
        public byte[] ToArray()
        {
            return new byte[0];
        }
    }

    public interface ISerilizeToByteArray
    {
        public static ISerilizeToByteArray ZeroDataObject => new ZeroDataObject();
        byte[] ToArray();
        
    }
}
