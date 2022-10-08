using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class NotificationDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string DataUrl { get; set; }


        public string Data { get; set; }

        public bool IsRead { get; set; }

        public Guid? UserId { get; set; }

        public User User { get; set; }
    }
}
