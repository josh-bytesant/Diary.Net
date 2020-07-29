using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diary.NET.Models
{
    public class NewDiaryViewModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public List<SelectListItem> Status { set; get; }
        public List<SelectListItem> Categories { set; get; }
        public bool Hidden { get; set; }
        public NewDiaryModel NewDiary { get; set; }
        //public DiaryContentModel Diary { get; set; }
    }
}