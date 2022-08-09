using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class DateData : Attribute
    {
        public int MinYear { get; set; }
        // public int MaxYear { get; set; } You can use date range validation
        public DateData(int minYear)
        {
            MinYear=minYear;
        }
    }
}
