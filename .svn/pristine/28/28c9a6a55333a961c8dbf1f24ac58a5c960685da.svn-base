using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.FileStorageService
{
    public interface IStorageService
    {
        string GetFileUrl(string fileName);
        Task SaveFileAsync(Stream mediaBinaryStream, string filename);
        Task DeleteFileAsync(string fileName);
    }
}
