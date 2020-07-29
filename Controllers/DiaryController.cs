using Diary.NET.Models;
using Diary.NET.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diary.NET.Controllers
{
    public class DiaryController : Controller
    {
        private IDiaryUtil m_diaryUtil;

        public DiaryController()
        {
            m_diaryUtil = new DiaryUtil();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            NewDiaryViewModel newDiary = new NewDiaryViewModel();
            var mstatus = m_diaryUtil.GetStatus()
                          .Select(s => new SelectListItem
                          {
                              Value = s.Code.ToString(),
                              Text = s.Description
                          }).ToList();
            newDiary.Status = mstatus;
            
            var mCategory = m_diaryUtil.GetCategories()
                            .Select(s => new SelectListItem
                            {
                                Value = s.Code.ToString(),
                                Text = s.Description
                            }).ToList();
            newDiary.Categories = mCategory;

            return View(newDiary);
        }

        [HttpPost]
        public ActionResult New(NewDiaryViewModel model)
        {
            
            NewDiaryModel diary = model.NewDiary;
            if (diary != null)
            {
                long id = m_diaryUtil.NewDiary(diary);
                m_diaryUtil.NewDiaryAttachmentsHelper(model.NewDiary.Files, id);
            }
            TempData["Message"] = "Diary created";
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            DiaryListModel model = new DiaryListModel();
            model.Diaries = m_diaryUtil.GetAllDiaries();

            if (TempData["Message"] != null)
            {
                model.Message = (string)TempData["Message"];
            }

            return View(model);
        }

        public ActionResult Delete(long id)
        {
            m_diaryUtil.DeleteDiary(id);
            TempData["Message"] = "Diary deleted";
            return RedirectToAction("List");
        }

        public ActionResult Details(long id)
        {
            DiaryContentModel model = new DiaryContentModel();
            model.Diary = m_diaryUtil.GetDiary(id);
            model.Attachments = m_diaryUtil.GetDiaryAttachments(id)
                                .Select(a => new DiaryAttachmentsModel
                                {
                                    Id = a.Id,
                                    EntryId = a.EntryId,
                                    Label = a.Label,
                                    Path = GetHostName() + "/"+ a.Path
                                }).ToList();

            //DiaryBodyModel model = m_diaryUtil.GetDiary(id);
            return View(model);
        }

        protected String GetHostName()
        {
            return Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.IndexOf(Request.Url.AbsolutePath));
        }
    }
}