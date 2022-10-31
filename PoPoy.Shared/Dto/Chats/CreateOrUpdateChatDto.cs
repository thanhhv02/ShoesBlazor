using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto.Chats
{
    public class CreateOrUpdateChatDto
    {
        public string Message { get; set; }
        public string Data { get; set; }
        public Guid? ReceiverId { get; set; } 

    }
}
