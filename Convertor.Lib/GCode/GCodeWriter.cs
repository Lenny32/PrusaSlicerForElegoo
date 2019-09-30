using Convertor.Lib.GCode.Command;
using Convertor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Convertor.Lib
{
    public class GCodeWriter
    {
        public StringBuilder _document;
        public GCodeWriter(PrusaPrintConfig prusaPrintConfig)
        {
            _document = new StringBuilder();
            PrusaPrintConfig = prusaPrintConfig;
        }

        public PrusaPrintConfig PrusaPrintConfig { get; }

        public void WriteHeader()
        {
            foreach (var properties in typeof(PrusaPrintConfig).GetProperties())
            {
                Write($";{properties.Name.ToLower()}:{properties.GetValue(PrusaPrintConfig)}");
            }
        }

        public void StartGCode()
        {
            _document.Append(@"
;START_GCODE_BEGIN
G21;
G90;
M106 S0;
G28 Z0;

;START_GCODE_END
");
        }

        public void Write(ICommand command)
        {
            var s = command.ToString();
            Write(s);
        }

        public void Write(IEnumerable<ICommand> commands)
        {
            foreach (var command in commands)
            {
                Write(command);
            }
        }



        public override string ToString()
        {
            return this._document.ToString();
        }

        public void Write()
        {
            Write(string.Empty);
        }

        protected void Write(string line)
        {
            _document.AppendLine(line);
        }
    }
}
