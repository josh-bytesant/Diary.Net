using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diary.NET.Models
{
    public class DiaryBodyModel
    {
        public Int64 Id { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Hidden { get; set; }
        public DateTime LastUpDated { get; set; }
        public string LastUpDatedBy { get; set; }
    }
}