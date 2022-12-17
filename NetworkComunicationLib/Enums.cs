using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib
{
    public enum TypeEnum
    {
        R,
        W,
        WR
    }

    public enum CodeEnum
    {
        Ok,
        NoContent,
        BadRequest,
        ConnectionError,
        BufferOverflow,
    };
}
