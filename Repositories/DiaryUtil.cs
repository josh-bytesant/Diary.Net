using Diary.NET.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Diary.NET.Repositories
{
    public class DiaryUtil : IDiaryUtil
    {
        string cs = ConfigurationManager.ConnectionStrings["DiaryEntities"].ConnectionString;
        public int DeleteDiary(long id)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = @"DELETE [Diary].[dbo].[Diary Entries]
                           WHERE [AutoId] = @id";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.Add(new SqlParameter("@id", id));


            int rowcount = (int)command.ExecuteNonQuery();
            con.Close();

            return rowcount;
        }

        public List<DiaryBodyModel> GetAllDiaries()
        {
            List<DiaryBodyModel> diaryList = new List<DiaryBodyModel>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = @"SELECT e.[AutoId]
                                ,ds.[Description] as [Status]
                                ,c.[Description] as [Category] 
                                ,e.[Title]
                                ,e.[Body]
                                ,e.[Hidden]
                                ,e.[Modified Date]
                                ,e.[Modified By]
                            FROM [Diary].[dbo].[Diary Entries] e LEFT OUTER JOIN
                                 [dbo].[Diary Categories] c ON e.Category = c.Code LEFT OUTER JOIN
                                 [dbo].[Diary Status] ds ON e.[Status] = ds.Code
                ";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DiaryBodyModel diary = new DiaryBodyModel();
                            diary.Id = Convert.ToInt64(reader["AutoId"]);
                            diary.Status = reader["Status"].ToString();
                            diary.Category = reader["Category"].ToString();
                            if (!(reader.IsDBNull(reader.GetOrdinal("Title")))) diary.Title = reader.GetString(reader.GetOrdinal("Title"));
                            diary.Body = reader["Body"].ToString();
                            diary.Hidden = reader.GetBoolean(reader.GetOrdinal("Hidden"));
                            diary.LastUpDated = reader.GetDateTime(reader.GetOrdinal("Modified Date"));
                            diary.LastUpDatedBy = reader["Modified By"].ToString();
                            diaryList.Add(diary);
                        }
                    }
                }
            }
            return diaryList;
        }

        public DiaryBodyModel GetDiary(long id)
        {
            DiaryBodyModel diary = new DiaryBodyModel();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = @"SELECT e.[AutoId]
                                ,ds.[Description] as [Status]
                                ,c.[Description] as [Category] 
                                ,e.[Title]
                                ,e.[Body]
                                ,e.[Hidden]
                                ,e.[Modified Date]
                                ,e.[Modified By]
                            FROM [Diary].[dbo].[Diary Entries] e LEFT OUTER JOIN
                                 [dbo].[Diary Categories] c ON e.Category = c.Code LEFT OUTER JOIN
                                 [dbo].[Diary Status] ds ON e.[Status] = ds.Code
                            WHERE e.[AutoId] = @id
                ";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            diary.Id = Convert.ToInt64(reader["AutoId"]);
                            diary.Status = reader["Status"].ToString();
                            diary.Category = reader["Category"].ToString();
                            if (!(reader.IsDBNull(reader.GetOrdinal("Title")))) diary.Title = reader.GetString(reader.GetOrdinal("Title"));
                            diary.Body = reader["Body"].ToString();
                            diary.Hidden = reader.GetBoolean(reader.GetOrdinal("Hidden"));
                            diary.LastUpDated = reader.GetDateTime(reader.GetOrdinal("Modified Date"));
                            diary.LastUpDatedBy = reader["Modified By"].ToString();
                        }
                    }
                }
            }
            return diary;
        }

        public long NewDiary(NewDiaryModel diary)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = @"INSERT INTO [Diary].[dbo].[Diary Entries] (
                     [Status]
                    ,[Category]
                    ,[Title]
                    ,[Body]
                    ,[Hidden]
                    ) output INSERTED.AutoId
            VALUES(@status,@category,@title,@body,@hidden)";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.Add(new SqlParameter("@status", diary.SelectedStatus));
            command.Parameters.Add(new SqlParameter("@category", diary.SelectedCategory));
            command.Parameters.Add(new SqlParameter("@title", diary.Title));
            command.Parameters.Add(new SqlParameter("@body", diary.Body));
            command.Parameters.Add(new SqlParameter("@hidden", diary.Hidden));
            Int64 Id = (Int64)command.ExecuteScalar();
            con.Close();

            return Id;
        }

        public int UpDateDiary(NewDiaryModel model)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = @"[Status] = @status
                            ,[Category] = @category
                            ,[Title] = @title
                            ,[Body] = @body
                            ,[Hidden] = @hidden
                            ,[Modified Date] = @modifieddate";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.Add(new SqlParameter("@status", model.SelectedStatus));
            command.Parameters.Add(new SqlParameter("@category", model.SelectedCategory));
            command.Parameters.Add(new SqlParameter("@title", model.Title));
            command.Parameters.Add(new SqlParameter("@body", model.Body));
            command.Parameters.Add(new SqlParameter("@hidden", model.Hidden));
            command.Parameters.Add(new SqlParameter("@modifieddate", DateTime.Now));


            int rowcount = (int)command.ExecuteNonQuery();
            con.Close();

            return rowcount;
        }

        public List<DiaryUtilityModel> GetStatus()
        {
            List<DiaryUtilityModel> statusList = new List<DiaryUtilityModel>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = @"SELECT [Code]
                                ,[Description]
                            FROM [Diary].[dbo].[Diary Status]
                ";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DiaryUtilityModel status = new DiaryUtilityModel();
                            status.Code = Convert.ToInt32(reader["Code"]);
                            status.Description = reader["Description"].ToString();
                            statusList.Add(status);
                        }
                    }
                }
            }
            return statusList;
        }

        public List<DiaryUtilityModel> GetCategories()
        {
            List<DiaryUtilityModel> categoryList = new List<DiaryUtilityModel>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = @"SELECT [Code]
                                ,[Description]
                            FROM [Diary].[dbo].[Diary Categories]
                ";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DiaryUtilityModel category = new DiaryUtilityModel();
                            category.Code = Convert.ToInt32(reader["Code"]);
                            category.Description = reader["Description"].ToString();
                            categoryList.Add(category);
                        }
                    }
                }
            }
            return categoryList;
        }

        public bool NewDiaryAttachmentsHelper(HttpPostedFileBase[] files, long id)
        {
            int year = @DateTime.Now.Year;
            int month = @DateTime.Now.Month;
            if (files.Length > 0)
            {
                    try
                    {   //  Get all files from Request object  
                    for (int i = 0; i < files.Length; i++)
                    {
                        HttpPostedFileBase file = files[i];

                        var extension = System.IO.Path.GetExtension(file.FileName);
                        var label = file.FileName;
                        var dbFileName = Guid.NewGuid().ToString() + extension;
                        string dirPath = HostingEnvironment.MapPath("~/Files/" + year + "/" + month + "/");
                        if (!System.IO.Directory.Exists(dirPath))
                        {
                            System.IO.Directory.CreateDirectory(dirPath);
                        }

                        // Get the complete folder path and store the file inside it.  
                        var dbPath = "Files/" + year + "/" + month + "/" + dbFileName;
                        var filePath = HostingEnvironment.MapPath("~/" + dbPath);
                        file.SaveAs(filePath);

                        var dbAttachment = new DiaryAttachmentsModel
                        {
                            Id = id,
                            Label = label,
                            MediaType = 1,
                            Path = dbPath
                        };
                        NewDiaryAttachment(dbAttachment);
                    }
                    return true;
                    // Returns message that successfully uploaded  
                    //return Json("File Uploaded Successfully!");
                }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            else
            {
                return false;
            }
            
        }

        public long NewDiaryAttachment(DiaryAttachmentsModel attachment)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = @"INSERT INTO [Diary].[dbo].[Diary Attachments] (
                     [EntryId]
                    ,[Media Type Id]
                    ,[Path]
                    ,[Label]
                    ) output INSERTED.AutoId
            VALUES(@entryid,@mediatype,@path,@label)";
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.Add(new SqlParameter("@entryid", attachment.Id));
            command.Parameters.Add(new SqlParameter("@mediatype", attachment.MediaType));
            command.Parameters.Add(new SqlParameter("@path", attachment.Path));
            command.Parameters.Add(new SqlParameter("@label", attachment.Label));
            Int64 Id = (Int64)command.ExecuteScalar();
            con.Close();

            return Id;
        }

        public List<DiaryAttachmentsModel> GetDiaryAttachments(long id)
        {
            List<DiaryAttachmentsModel> attachmentList = new List<DiaryAttachmentsModel>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = @"SELECT [AutoId]
                                       ,[EntryId]
                                       ,[Media Type Id]
                                       ,[Path]
                                       ,[Label]
                            FROM [Diary].[dbo].[Diary Attachments]
                            WHERE [EntryId] = @id
                ";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DiaryAttachmentsModel attachment = new DiaryAttachmentsModel();
                            attachment.Id = Convert.ToInt64(reader["AutoId"]);
                            attachment.EntryId = Convert.ToInt64(reader["EntryId"]);
                            attachment.MediaType = Convert.ToInt32(reader["Media Type Id"]);
                            attachment.Path = reader["Path"].ToString();
                            attachment.Label = reader["Label"].ToString();
                            attachmentList.Add(attachment);
                        }
                    }
                }
            }
            return attachmentList;
        }
    }
}