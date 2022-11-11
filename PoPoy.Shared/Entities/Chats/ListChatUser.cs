using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto.Chats
{
    public class ListChatUser
    {
        public ChatDto Chat { get; set; }

        public UserChat UserChat { get; set; }
    }
}
