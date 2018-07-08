using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ViewModels
{
    public class PopupPartialViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<ButtonViewModel> ButtonEntity { get; set; }
    }
}