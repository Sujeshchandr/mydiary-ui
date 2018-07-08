using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.MongoFillingService.Parser
{
    public class MyDiaryOptions
    {
        public FileTypeEnum FileType { get; set; }

        public string Option  { get; set; }

    }

    [Flags]
    public enum FileTypeEnum
    {
        Production = 1,
        Submission = 2
    }
}
