using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diary.NET.Models
{
    public class NewDiaryModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Display(Name = "Status")]
        public int? SelectedStatus { get; set; }

        [Display(Name = "Category")]
        public int? SelectedCategory { get; set; }

        public bool Hidden { get; set; }

        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] Files { get; set; }
    }
}