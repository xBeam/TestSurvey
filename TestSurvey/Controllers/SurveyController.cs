using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace TestSurvey.Controllers
{
    public class SurveyController : Controller
    {
        private SurveyModel db = new SurveyModel();

        public const int PageSize = 8;

        [Route("Survey/{userId:int}/{surveyId:int}/{pageNumber:int}/")]
        public ActionResult Index(int pageNumber = 1, int surveyId = 2, int userId = 1)
        {
            InitializeDb();
            db.Answers.ToList();

            var offset = (pageNumber - 1) * PageSize;
            var survey = db.SurveyInfos.FirstOrDefault(c => c.Id == surveyId);

            var questionList = db.Questions
                            .Where(c => c.Survey.Id == surveyId && c.Respondent.Id == userId)
                            .OrderBy(c => c.Id)
                            .Skip(offset)
                            .Take(PageSize)
                            .ToList();

            ViewBag.userId = userId;
            ViewBag.surveyId = surveyId;
            ViewBag.pageNumber = pageNumber;
            ViewBag.SurveyName = survey.Name;

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
                    });

                db.SaveChanges();
            }
        }

        [HttpPost]
        public ActionResult SaveData(List<Answer> jsonObjects)
        {
            foreach (var jsonObject in jsonObjects)
            {
                if (jsonObject.Id == 0) continue;

                var questionToChange = db.Questions.Find(jsonObject.Id);

                if (questionToChange.QuestionType == QuestionTypes.Checkbox || questionToChange.QuestionType == QuestionTypes.Radio)
                {
                    var answer = db.Answers.FirstOrDefault(c => c.Question.Id == questionToChange.Id && c.Text == jsonObject.Text);

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
