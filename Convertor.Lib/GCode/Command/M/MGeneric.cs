using System;
using System.Collections.Generic;
using System.Text;

namespace Convertor.Lib.GCode.Command.M
{
    public abstract class MGeneric<T> : ICommand
    {
        public T Value { get; set; }

        public abstract string GetClassName();
    }
}
