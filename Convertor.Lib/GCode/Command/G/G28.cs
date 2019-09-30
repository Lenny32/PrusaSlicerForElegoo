using System;
using System.Collections.Generic;
using System.Text;

namespace Convertor.Lib.GCode.Command.G
{
    public class G28 : GCommand
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }

        public override string GetClassName()
        {
            return "G28";
        }
        public override string ToString()
        {
            return $"G28 {(X>-1 ? $"X{X}" : "")}{(Y > -1 ? $"Y{Y}" : "")}{(Z > -1 ? $"Z{Z}" : "")} ;Auto Home";
        }
    }
}
