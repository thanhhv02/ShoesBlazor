using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class NotiSendConfig
    {


       /// <summary>
       ///  Tiêu đề thông báo
       /// </summary>
        public string Title { get; set; }

       /// <summary>
       ///  Nội dung của thông báo
       /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Đường dẫn khi nhấp vào thông báo 
        /// </summary>
        public string DataUrl { get; set; }

        /// <summary>
        /// Chuỗi data json muốn đi kèm thông báo
        /// </summary>
        public string Data { get; set; }


        public NotiSendConfig(string title, string message, string dataUrl, string data)
        {
            Title = title;
            Message = message;
            DataUrl = dataUrl;
            Data = data;
        }

        public NotiSendConfig(string title, string message, string dataUrl)
        {
            Title = title;
            Message = message;
            DataUrl = dataUrl;
        }

        public NotiSendConfig(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
