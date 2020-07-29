using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diary.NET.Models
{
    public class DiaryAttachmentsModel
    {
        public Int64 Id { get; set; }
        public Int64 EntryId { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
        public int MediaType { get; set; }
    }
}