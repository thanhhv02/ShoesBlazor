using PoPoy.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.UserAvatarService
{
    interface IUserAvatarService
    {
        Task<List<UploadResult>> UploadAvatar(MultipartFormDataContent multipartFormDataContent, string id);
        event Action OnAvatarChanged;
    }
}
