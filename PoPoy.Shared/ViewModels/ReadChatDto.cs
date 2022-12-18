using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.ViewModels
{
    public class ReadChatDto
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
