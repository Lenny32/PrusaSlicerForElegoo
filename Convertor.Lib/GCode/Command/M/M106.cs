using System;
using System.Collections.Generic;
using System.Text;

namespace Convertor.Lib.GCode.Command.M
{
    public class M106 : MGeneric<byte>
    {
        //public byte Value { get; set; }
        public override string GetClassName()
        {
            return this.GetType().Name;
        }
        public override string ToString()
        {
            return $"M106 S{Value}; light {(Value > 0 ? "on" : "off")}";
        }
    }
}
