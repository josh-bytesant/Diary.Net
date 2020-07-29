using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diary.NET.Models
{
    public class DiaryContentModel
    {
        public DiaryBodyModel Diary { get; set; }
        public List<DiaryAttachmentsModel> Attachments { get; set; }
        public List<DiaryTagsModel> Tags { get; set; }

         
    }
}