using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TestSurvey.Models;

namespace TestSurvey.Controllers
{
    public class SurveyController : Controller
    {
        private SurveyModel db = new SurveyModel();

        public const int PageSize = 8;

        [Route("Survey/{userId:int}/{surveyId:int}/{pageNumber:int}/")]
        public ActionResult Index(int pageNumber = 1, int surveyId = 1, int userId = 1)
        {
            InitializeDb();

            var offset = (pageNumber - 1) * PageSize;
            var survey = db.SurveyInfos.FirstOrDefault(c => c.Id == surveyId);
           
            var respondent = db.RespondentInfos.FirstOrDefault(c => c.Id == userId);
            var questionList = db.Questions
                            .Where(c => c.Survey.Id == surveyId)
                            .OrderBy(c => c.Id)
                            .Skip(offset)
                            .Take(PageSize)
                            .ToList();

            db.Responses.ToList();
            db.Answers.ToList();

            ViewBag.userId = userId;
            ViewBag.surveyId = surveyId;
            ViewBag.pageNumber = pageNumber;
            ViewBag.SurveyName = survey.Title;
            ViewBag.RespondentId = respondent.Id;

            return View(questionList);
        }

        public void InitializeDb()
        {
            if (!db.SurveyInfos.Any())
            {
                db.SurveyInfos.AddOrUpdate(
                new SurveyInfo
                {
                    Title = "First Survey",
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            Text = "Is it very Important Question?",
                            QuestionType = QuestionTypes.Radio,
                            Answers = new List<Answer>
                            {
                                new Answer {Text = "Yes"},
                                new Answer {Text = "No"},
                                new Answer {Text = "In doubt"}
                            }
                        },

                        new Question
                        {
                            Text = "Choose your favorite color(s)",
                            QuestionType = QuestionTypes.Checkbox,
                            Answers = new List<Answer>
                            {
                                new Answer {Text = "Yellow"},
                                new Answer {Text = "Green"},
                                new Answer {Text = "Pink"},
                                new Answer {Text = "Black"}
                            }
                        },

                        new Question
                        {
                            Text = "Write your biggest advantage:",
                            QuestionType = QuestionTypes.Text,
                            Answers = new List<Answer> {new Answer {Text = "Answer"}}
                        },

                        new Question
                        {
                            Text = "How to write if condition in a right way",
                            QuestionType = QuestionTypes.Radio,
                            Answers = new List<Answer>
                            {
                                new Answer {Text = "if (i <> 5)"},
                                new Answer {Text = "if i <> 5"},
                                new Answer {Text = "if i =! 5 then"},
                                new Answer {Text = "if (i != 5)"}
                            }
                        },

                        new Question
                        {
                            Text = "What will show on display the following code? alert(Math.floor(Math.random());",
                            QuestionType = QuestionTypes.Radio,
                            Answers = new List<Answer>
                            {
                                new Answer {Text = "0"},
                                new Answer {Text = "1"},
                                new Answer {Text = "null"},
                                new Answer {Text = "undefiend"},
                                new Answer {Text = "Math.random"}
                            }
                        },

                        new Question
                        {
                            Text = "How to read property Age of object Person",
                            QuestionType = QuestionTypes.Checkbox,
                            Answers = new List<Answer>
                            {
                                new Answer {Text = "person[\"age\"]"},
                                new Answer {Text = "person::age"},
                                new Answer {Text = "person.age"},
                                new Answer {Text = "person->age"},
                                new Answer {Text = "person['age']"},
                                new Answer {Text = "person[age]"},
                            }
                        },

                        new Question
                        {
                            Text = "Enter name of function, that return string with info, that user typed in modal window",
                            QuestionType = QuestionTypes.Text,
                            Answers = new List<Answer> {new Answer {Text = "Answer"}}
                        },

                        new Question
                        {
                            Text = "Which of calls of this function are viable? function func(a) { return \"1\"; } ",
                            QuestionType = QuestionTypes.Checkbox,
                            Answers = new List<Answer>
                            {
                                new Answer {Text = "func(func());"},
                                new Answer {Text = "func();"},
                                new Answer {Text = "func(1, 2);"},
                                new Answer {Text = "func(\"1\");"},
                                new Answer {Text = "func(new Object());"},
                            }
                        },
                    }
                });

                db.SaveChanges();
            }

            if (!db.RespondentInfos.Any())
            {
                var survey = db.SurveyInfos.First(c => c.Id == 1);
                db.RespondentInfos.Add(new RespondentInfo
                {
                    Name = "First Respondent",
                    Email = "respondent@email.com",
                    Password = "12Admin!"
                });

                db.SaveChanges();

                var respondent = db.RespondentInfos.First(c => c.Id == 1);

                foreach (var question in survey.Questions)
                {
                    db.Responses.Add(
                        new Response
                        {
                            Respondent = respondent,
                            Survey = survey,
                            Question = db.Questions.FirstOrDefault(c => c.Id == question.Id)
                        });
                }

                db.SaveChanges();
            }
        }

        [HttpPost]
        public void SaveData(List<Answer> jsonObjects)
        {//todo re-write this method and collect-data js
            foreach (var jsonObject in jsonObjects)
            {
                if (jsonObject.Id == 0) continue;

                var questionToChange = db.Questions.Find(jsonObject.Id);

                if (questionToChange.QuestionType == QuestionTypes.Checkbox || questionToChange.QuestionType == QuestionTypes.Radio)
                {
                    var answer = db.Answers.FirstOrDefault(c => c.Question.Id == questionToChange.Id && c.Text == jsonObject.Text);

                    if (answer == null) continue;

                    //answer.IsChecked = jsonObject.IsChecked;
                                            
                    db.Entry(answer).State = EntityState.Modified;
                }
                else
                {
                    //questionToChange.TypedAnswer = jsonObject.Text;
                }

                db.Entry(questionToChange).State = EntityState.Modified;
            }

            db.SaveChanges();
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
