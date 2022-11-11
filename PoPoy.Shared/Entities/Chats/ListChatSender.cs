using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto.Chats
{
    public class UserChat
    {
        public Guid? UserId { get; set; }

        public string FullName { get; set; }

        public string AvatarPath { get; set; }

        public bool IsRead { get; set; }

        public int CountUnRead { get; set; }
    }
    public class ListChatSender
    {
        public Guid Id { get; set; }

        public UserChat User { get; set; }

        public List<ChatDto> ReceiverChats { get; set; }

        public List<ChatDto> SenderChats { get; set; }

    }
}
