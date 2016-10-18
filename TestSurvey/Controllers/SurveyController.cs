using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestSurvey;

namespace TestSurvey.Controllers
{
    public class SurveyController : Controller
    {
        private SurveyModel db = new SurveyModel();

        // GET: Survey
        public ActionResult Index()
        {
            //if (!db.SurveyInfos.Any())
            //{
            //    db.SurveyInfos.Add(
            //        new SurveyInfo
            //        {
            //            Name = "First Survey",
            //            Questions = new List<Question>()
            //            {
            //                new Question
            //                {
            //                    Text = "Question 1",
            //                    Answers = new List<Answer>()
            //                    {
            //                        new Answer
            //                        {
            //                            AnswerType = AnswerTypes.Radio,
            //                            Text = "Answer"
            //                        },
            //                        new Answer
            //                        {
            //                            AnswerType = AnswerTypes.Radio,
            //                            Text = "Option"
            //                        },
            //                        new Answer
            //                        {
            //                            AnswerType = AnswerTypes.Radio,
            //                            Text = "Response"
            //                        }
            //                    }
            //                },

            //                new Question
            //                {
            //                    Text = "Question 2",
            //                    Answers = new List<Answer>()
            //                    {
            //                        new Answer
            //                        {
            //                            AnswerType = AnswerTypes.Checkbox,
            //                            Text = "Answer"
            //                        },
            //                        new Answer
            //                        {
            //                            AnswerType = AnswerTypes.Checkbox,
            //                            Text = "Option"
            //                        },
            //                        new Answer
            //                        {
            //                            AnswerType = AnswerTypes.Checkbox,
            //                            Text = "Response"
            //                        },
            //                        new Answer
            //                        {
            //                            AnswerType = AnswerTypes.Checkbox,
            //                            Text = "Response 2"
            //                        }
            //                    }
            //                },

            //                new Question
            //                {
            //                    Text = "Question 3",
            //                    Answers = new List<Answer>()
            //                    {
            //                        new Answer
            //                        {
            //                            AnswerType = AnswerTypes.Text,
            //                            Text = "Answer"
            //                        }
            //                    }
            //                }
            //            },
            //            Respondents = new List<RespondentInfo>() { new RespondentInfo() { Name = "Admin" } }
            //        });

            //    db.SaveChanges();
            //}

            //return View();
            var listA = db.Answers.ToList().OrderBy(c => c.Id);
            var listQ = db.Questions.ToList().OrderBy(c=>c.Id);
            var listS = db.SurveyInfos.ToList().OrderBy(c => c.Id);
            return View(listS);
        }

        public ActionResult SecondPage()
        {
           return View();
        }

        public ActionResult ThirdPage()
        {
            return View();
        }

        // GET: Survey/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyInfo surveyInfo = db.SurveyInfos.Find(id);
            if (surveyInfo == null)
            {
                return HttpNotFound();
            }
            return View(surveyInfo);
        }

        // GET: Survey/Create
        [HttpPost]
        public ActionResult Create(List<Question> questions)
        {
            foreach (var jsonObject in questions) 
            {
                var questionToChange = db.Questions.Find(jsonObject.Id); 
                questionToChange.TypedAnswer = jsonObject.TypedAnswer;
                db.Entry(questionToChange).State = EntityState.Modified;
            }

            db.SaveChanges();

            return View();
        }

        // POST: Survey/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name")] SurveyInfo surveyInfo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SurveyInfos.Add(surveyInfo);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(surveyInfo);
        //}

        // GET: Survey/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyInfo surveyInfo = db.SurveyInfos.Find(id);
            if (surveyInfo == null)
            {
                return HttpNotFound();
            }
            return View(surveyInfo);
        }

        // POST: Survey/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SurveyInfo surveyInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(surveyInfo);
        }

        // GET: Survey/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyInfo surveyInfo = db.SurveyInfos.Find(id);
            if (surveyInfo == null)
            {
                return HttpNotFound();
            }
            return View(surveyInfo);
        }

        // POST: Survey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SurveyInfo surveyInfo = db.SurveyInfos.Find(id);
            db.SurveyInfos.Remove(surveyInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
