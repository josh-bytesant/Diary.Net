using Diary.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Diary.NET.Repositories
{
    public interface IDiaryUtil
    {
        Int64 NewDiary(NewDiaryModel diary);
        DiaryBodyModel GetDiary(Int64 id);
        int UpDateDiary(NewDiaryModel id);
        int DeleteDiary(Int64 id);
        List<DiaryBodyModel> GetAllDiaries();
        List<DiaryUtilityModel> GetStatus();
        List<DiaryUtilityModel> GetCategories();
        bool NewDiaryAttachmentsHelper(HttpPostedFileBase[] files, long id);
        long NewDiaryAttachment(DiaryAttachmentsModel attachment);
        List<DiaryAttachmentsModel> GetDiaryAttachments(long id);
    }
}
