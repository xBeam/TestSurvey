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
            if (!db.SurveyInfos.Any())
            {
                db.SurveyInfos.Add(
                    new SurveyInfo
                    {
                        Name = "First Survey",
                        Questions = new List<Question>()
                        {
                            new Question
                            {
                                Text = "Is it very Important Question?",
                                QuestionType = QuestionTypes.Radio, 
                                Answers = new List<Answer>()
                                {
                                    new Answer { Text = "Yes" },
                                    new Answer { Text = "No" },
                                    new Answer { Text = "In doubt" }
                                }
                            },

                            new Question
                            {
                                Text = "Choose your favorite color(s)",
                                QuestionType = QuestionTypes.Checkbox,
                                Answers = new List<Answer>()
                                {
                                    new Answer { Text = "Yellow" },
                                    new Answer { Text = "Green" },
                                    new Answer { Text = "Pink" },
                                    new Answer { Text = "Black" }
                                }
                            },

                            new Question
                            {
                                Text = "Write your biggest advantage:",
                                QuestionType = QuestionTypes.Checkbox,
                                Answers = new List<Answer>() { new Answer { Text = "Answer" } }
                            }
                        },
                        Respondents = new List<RespondentInfo>() { new RespondentInfo() { Name = "Admin" } }
                    });

                db.SaveChanges();
            }

            //return View();
            var listA = db.Answers.ToList();
            var listQ = db.Questions.ToList();
            var listS = db.SurveyInfos.ToList().OrderBy(c => c.Id);

            //var firstS = db.SurveyInfos.FirstOrDefault(c => c.Id == 2);
            //firstS.Name = "First Survey";
            //db.Entry(firstS).State = EntityState.Modified;

            //var thirdS = db.SurveyInfos.FirstOrDefault(c => c.Id == 3);
            //thirdS.Name = "Second Survey";
            //db.Entry(thirdS).State = EntityState.Modified;

            //var secS = db.SurveyInfos.FirstOrDefault(c => c.Id == 4);
            //secS.Name = "Third Survey";
            //db.Entry(secS).State = EntityState.Modified;

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
        public ActionResult Create(List<Answer> jsonObjects)
        {
            foreach (var jsonObject in jsonObjects) 
            {
                var questionToChange = db.Questions.Find(jsonObject.Id); 
                questionToChange.TypedAnswer = jsonObject.Text;

                if (questionToChange.QuestionType == QuestionTypes.Checkbox || questionToChange.QuestionType == QuestionTypes.Radio)
                {
                    var answer = db.Answers.Where(c => c.QuestionId.Id == questionToChange.Id && c.Text == jsonObject.Text).FirstOrDefault();
                    answer.IsChecked = jsonObject.IsChecked;
                                            
                    db.Entry(answer).State = EntityState.Modified;
                }

                //questionToChange.QuestionType = QuestionTypes.Radio;
                //questionToChange.QuestionType = QuestionTypes.Checkbox;
                //questionToChange.QuestionType = QuestionTypes.Text;

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
