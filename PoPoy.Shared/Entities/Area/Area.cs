using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Entities.Area
{
    public class Area
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Result> results { get; set; }
    }
}
