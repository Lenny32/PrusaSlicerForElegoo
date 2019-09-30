using System;
using System.Collections.Generic;
using System.Linq;

namespace Convertor.Lib
{
    public class IniParser
    {
        public IniParser(string document)
        {
            Document = document;
        }

        public string Document { get; }

        public T Parse<T>() where T : new()
        {
            var returnValue = new T();

            var rows = Document.Replace("\r", String.Empty).Split('\n');
            var properties = typeof(T).GetProperties();
            foreach (var element in rows.Where(x=> !String.IsNullOrWhiteSpace(x)))
            {
                var keyValue = element.Split('=');
                var name = keyValue[0].TrimEnd();
                var p = properties.FirstOrDefault(x => String.Equals(name, x.Name, StringComparison.InvariantCultureIgnoreCase));
                if(p != null)
                {
                    var value = keyValue[1];
                    value = value.TrimStart();
                    if(p.PropertyType != typeof(string))
                    {
                        var o = Convert.ChangeType(value, p.PropertyType);
                        p.SetValue(returnValue, o);
                    }
                    else
                    {
                        p.SetValue(returnValue, value);
                    }
                    
                }

            }

            return returnValue;
        }
    }
}
