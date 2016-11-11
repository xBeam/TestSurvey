using System.Collections.Generic;
using System.Linq;
using TestSurvey.Context;
using TestSurvey.Models;

namespace TestSurvey.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TestSurvey.Context.SurveyDbContext>
    {
        private SurveyDbContext db = new SurveyDbContext();

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TestSurvey.Context.SurveyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

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
                            Answers = new List<Answer>() {new Answer {Text = "Answer"}}
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

            var survey = db.SurveyInfos.First(c => c.Id == 1);
            db.RespondentInfos.AddOrUpdate(
                new RespondentInfo
            {
                Name = "First Respondent",
                Email = "respondent@email.com",
                Password = "12Admin!"
            });

            db.SaveChanges();

            var respondent = db.RespondentInfos.FirstOrDefault(c => c.Id == 1);

            foreach (var question in survey.Questions)
            {
                respondent.Responses.Add(
                    new Response
                    {
                        Survey = survey,
                        Question = db.Questions.FirstOrDefault(c => c.Id == question.Id)
                    });
            }

            db.SaveChanges();
        }
    }
}
