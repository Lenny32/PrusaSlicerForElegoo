using System;
using System.Collections.Generic;
using System.Text;

namespace Convertor.Lib.GCode.Command.G
{
    public class G0 : GCommand
    {
        public decimal E { get; set; } = -1;
        public decimal X { get; set; } = -1;
        public decimal Y { get; set; } = -1;
        public decimal Z { get; set; } = -1;
        public int F { get; set; } = -1;

        public override string GetClassName()
        {
            return $"G0";
        }
        public override string ToString()
        {
            return $"G0 {(X > -1 ? $"X{X} " : "")}{(Y > -1 ? $"Y{Y} " : "")}{(Z > -1 ? $"Z{Z} " : "")}{(E > -1 ? $"E{E} " : "")}{(F > -1 ? $"F{F} " : "")}; Linear Move";
        }
    }
}
