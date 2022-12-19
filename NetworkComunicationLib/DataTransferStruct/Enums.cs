using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib.DataTransferStruct
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

    public enum MoveEnum
    {
        Fx = 0,
        Fy = 1,
        Fz = 2,
        Mx = 3,
        My = 4,
        Mz = 5
    };

    public enum HandEnum
    {
        Hand1,
        Hand2,
        Hand3,
        Hand4,
        Hand5,
        Hand6,
    };

    public enum Position
    {
        X = 0,
        Y = 1,
        Z = 2,
        W = 3
    };
}
