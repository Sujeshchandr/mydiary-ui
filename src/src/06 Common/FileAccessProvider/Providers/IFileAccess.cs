using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.FileAccessProvider.Providers
{
    public interface IFileAccess 
    {
        int GetFile(int fileId);
        int GetFile(int fileId, int version);
        string GetFile(string fileName);
        bool Upload(Stream stream);
        bool Upload(byte[] bytes);
    }
}
