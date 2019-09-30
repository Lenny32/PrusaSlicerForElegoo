using System;
using System.Collections.Generic;
using System.Text;

namespace Convertor.Lib.GCode.Command.M
{
    public class M6054 : MGeneric<string>
    {
        //public int Value { get; set; }
        public override string GetClassName()
        {
            return this.GetType().Name;
        }
        public override string ToString()
        {
            return $"M6054 \"{Value}\";show Image";
        }
    }

    
}
