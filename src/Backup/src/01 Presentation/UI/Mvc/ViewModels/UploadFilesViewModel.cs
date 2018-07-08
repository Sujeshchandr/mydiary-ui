using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ViewModels
{
    public class UploadFilesViewModel
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public string Type { get; set; }
        public string UserGuid { get; set; }
    }
}