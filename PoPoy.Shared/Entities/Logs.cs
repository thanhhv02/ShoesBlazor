using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Entities
{
    public class Logs
    {
        [Key]
        public int LogId { get; set; }  

        public LogLevelApp LogLevel { get; set; }

        public string EventName { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreateDate { get; set; }



    }
}
