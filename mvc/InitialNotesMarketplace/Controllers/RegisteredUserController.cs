using InitialNotesMarketplace.Models;
using InitialNotesMarketplace.Models.Extended;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace InitialNotesMarketplace.Controllers
{
    [Authorize(Roles = "User")]
    public class RegisteredUserController : Controller
    {
        // GET: RegisteredUser
        public ActionResult UserDashboard()
        {
            User loggedInUser = (User)Session["user"];
            //System.Diagnostics.Debug.WriteLine(loggedInUser.Firstname);
            return View(loggedInUser);
        }

        public ActionResult SellYourNotes()
        {
            return RedirectToAction("UserDashboard", "RegisteredUser");
        }

        public ActionResult AddNote()
        {
            Note note = new Note();
            return View(note);
        }

        [HttpPost]
        public ActionResult AddNote(Note model, FormCollection collection)
        {
            bool isPaid = false;
            if(collection["sell-for"] == "paid")
            {
                isPaid = true;
            }
            User loggedInUser = (User)Session["user"];
            using (var context = new DBEntities())
            {
                Models.Extended.SellerNote newNote = model.sellerNote;

                Models.SellerNote note1 = new Models.SellerNote()
                {
                    SellerID = loggedInUser.ID,
                    Title = newNote.Title,
                    Status = 7,
                    Category = newNote.Category,
                    NoteType = newNote.NoteType,
                    NumberOfPages = newNote.NumberOfPages,
                    Description = newNote.Description,
                    Country = newNote.Country,
                    UniversityName = newNote.UniversityName,
                    Course = newNote.Course,
                    CourseCode = newNote.CourseCode,
                    Professor = newNote.Professor,
                    IsPaid = isPaid,
                    SellingPrice = newNote.SellingPrice,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = loggedInUser.ID,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = loggedInUser.ID
                };

                context.SellerNotes.Add(note1);
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }

                string previewPath = Server.MapPath("~/Members/" + note1.SellerID + "/" + note1.ID);

                if(!Directory.Exists(previewPath))
                {
                    Directory.CreateDirectory(previewPath);
                }

                string previewFileName = "Preview_" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf";
                string fullPreviewFilePath = Path.Combine(previewPath, previewFileName);
                string relativePreviewFilePath = Path.Combine("~/Members/" + note1.SellerID + "/" + note1.ID, previewFileName);
                model.sellerNote.NotesPreview.SaveAs(fullPreviewFilePath);

                var fNote = context.SellerNotes.FirstOrDefault(x => x.ID == note1.ID);
                fNote.NotesPreview = relativePreviewFilePath;

                string displayPicPath = Server.MapPath("~/Members/" + note1.SellerID + "/" + note1.ID);
                string displayPicName = "DisplayPicture" + DateTime.Now.ToString("dd/MM/yyyy") + ".jpg";
                string fulldisplayPicPath = Path.Combine(displayPicPath, displayPicName);
                string relativeDisplayPicPath = Path.Combine("~/Members/" + note1.SellerID + "/" + note1.ID, displayPicName);
                model.sellerNotesAttachment.FilePath.SaveAs(fulldisplayPicPath);
                fNote.DisplayPicture = relativeDisplayPicPath;

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }



                string filePath = Server.MapPath("~/Members/" + note1.SellerID + "/" + note1.ID + "/Attachments");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string fileName = "Attachment_" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf";
                string fullFilePath = Path.Combine(filePath, fileName);
                string relativeFilePath = Path.Combine("~/Members/" + note1.SellerID + "/" + note1.ID + "/Attachments", fileName);
                model.sellerNotesAttachment.FilePath.SaveAs(fullFilePath);

                var requiredNoteId = context.SellerNotes.Max(x => x.ID);
                Models.SellerNotesAttachment notesAttachment = new Models.SellerNotesAttachment()
                {
                    NoteID = requiredNoteId,
                    FileName = fileName,
                    FilePath = relativeFilePath,
                    CreatedDate = DateTime.Now,
                    CreatedBy = note1.SellerID,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = note1.SellerID,
                    IsActive = true
                };

                context.SellerNotesAttachments.Add(notesAttachment);
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }

            }
            return View();
        }

        public ActionResult SearchNotes()
        {
            DBEntities _db = new DBEntities();
            var typeList = _db.NoteTypes.ToList();
            var categoryList = _db.NoteCategories.ToList();
            var countryList = _db.Countries.ToList();
            List<string> universityList = new List<string>();
            foreach(InitialNotesMarketplace.Models.SellerNote row in _db.SellerNotes)
            {
                if(!universityList.Contains(row.UniversityName))
                universityList.Add(row.UniversityName);
            }

            List<string> courseList = new List<string>();
            foreach (InitialNotesMarketplace.Models.SellerNote row in _db.SellerNotes)
            {
                if (!courseList.Contains(row.Course))
                    courseList.Add(row.Course);
            }

            SearchNote searchNote = new SearchNote()
            {
                ListCategories = categoryList,
                ListCountries = countryList,
                ListTypes = typeList,
                ListUniversities = universityList,
                ListCourses = courseList
            };

            Session["searchNote"] = searchNote;
            Models.SellerNote snote = new Models.SellerNote();

            return View(snote);
        }

        public PartialViewResult FilterPartialView(string search, string university, string course, int? category = 0, int? type = 0, int? country = 0)
        {
            DBEntities _db = new DBEntities();
            List<Models.SellerNote> filterResult = _db.SellerNotes.Where(x => x.Status == 9 && x.IsActive == true).ToList();
            if(search != null)
            {
                filterResult = filterResult.Where(x => x.Title.ToLower().Contains(search.ToLower()) ).ToList();
            }
            if (type != 0)
            {
                filterResult = filterResult.Where(x => x.NoteType == type).ToList();
            }
            if (category != 0)
            {
                filterResult = filterResult.Where(x => x.Category == category).ToList();
            }
            if (university != null)
            {
                filterResult = filterResult.Where(x => x.UniversityName == university).ToList();
            }
            if (course != null)
            {
                filterResult = filterResult.Where(x => x.Course == course).ToList();
            }
            if (country != 0)
            {
                filterResult = filterResult.Where(x => x.Country == country).ToList();
            }

            return PartialView("_FilterPartialView", filterResult);
        }

        public ActionResult BuyerRequests()
        {
            User loggedInUser = (User)Session["user"];
            //System.Diagnostics.Debug.WriteLine(loggedInUser.Firstname);
            return View(loggedInUser);
        }

        public ActionResult ViewNote(int noteId)
        {
            DBEntities _db = new DBEntities();
            Models.SellerNote note = _db.SellerNotes.FirstOrDefault(x => x.ID == noteId);
            return View(note);
        }


        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(FormCollection collection)
        {
            ViewData["FullName"] = collection["FullName"];
            ViewData["EmailAddress"] = collection["EmailAddress"];
            ViewData["EmailSubject"] = collection["EmailSubject"];
            ViewData["Comments"] = collection["Comments"];
            SendEmailVerificationMail(collection["EmailSubject"], collection["Comments"], collection["FullName"]);
            return RedirectToAction("ContactUs", "RegisteredUser");
        }

        [NonAction]
        public void SendEmailVerificationMail(string subject, string body, string fullname)
        {
            DBEntities db = new DBEntities();
            SystemConfiguration toMailObj = db.SystemConfigurations.FirstOrDefault(x => x.Key == "Event Email Address");
            SystemConfiguration fromMailObj = db.SystemConfigurations.FirstOrDefault(x => x.Key == "Support Email Address");
            var fromEmail = new MailAddress(fromMailObj.Value, "Darsh Sharma");
            var toMail = new MailAddress(toMailObj.Value);
            var fromEmailPassword = "fsdebzpgtgbmqzzh";
            string emailSubject = subject;
            string emailBody = "Hello, <br><br> " + body + "<br><br>Regards,<br>" + fullname;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toMail)
            {
                Subject = emailSubject,
                Body = emailBody,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        //DESKTOP-EKL0716\VIMARSHSQL
    }
}