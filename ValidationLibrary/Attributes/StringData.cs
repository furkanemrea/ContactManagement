using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class StringData:Attribute
    {
        public int MaxLength { get; set; }
        public StringData(int maxLength)
        {
            MaxLength=maxLength;
        }
    }
}
