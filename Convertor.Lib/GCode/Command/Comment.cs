using System;
using System.Collections.Generic;
using System.Text;

namespace Convertor.Lib.GCode.Command
{
    public class Comment : ICommand
    {
        public string Value { get; set; }
        public override string ToString()
        {
            return $";{Value}";
        }
    }
}
