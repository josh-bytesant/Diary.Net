using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diary.NET.Models
{
    public class DiaryListModel
    {
        public List<DiaryBodyModel> Diaries { get; set; }
        public string Message { get; set; }


    }
}