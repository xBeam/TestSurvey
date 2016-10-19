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

        public const int PageSize = 3;

        // GET: Survey
        [Route("Survey/{pageNumber:int}")]
        public ActionResult Index(int pageNumber = 1)
        {
            InitializeDb();
            db.Answers.ToList();

            var offset = (pageNumber - 1) * PageSize;

            var questionList = db.Questions
                            .OrderBy(c => c.Id)
                            .Skip(offset)
                            .Take(PageSize)
                            .ToList();

            ViewBag.pageNumber = pageNumber;

            return View(questionList);
        }

        public void InitializeDb()
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
                if (jsonObject.Id == 0) continue;

                var questionToChange = db.Questions.Find(jsonObject.Id);

                if (questionToChange.QuestionType == QuestionTypes.Checkbox || questionToChange.QuestionType == QuestionTypes.Radio)
                {
                    var answer = db.Answers.FirstOrDefault(c => c.QuestionId.Id == questionToChange.Id && c.Text == jsonObject.Text);

                    if (answer == null) continue;

                    answer.IsChecked = jsonObject.IsChecked;
                                            
                    db.Entry(answer).State = EntityState.Modified;
                }
                else
                {
                    questionToChange.TypedAnswer = jsonObject.Text;
                }

                db.Entry(questionToChange).State = EntityState.Modified;
            }

            db.SaveChanges();

            return View();
        }

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
