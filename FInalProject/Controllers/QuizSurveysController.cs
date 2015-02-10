using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

using FInalProject.Data;
using FInalProject.Data.Models;
using FInalProject.Models.UserViewModels;
using FInalProject.Models;

namespace FInalProject.Controllers
{

    
    [Authorize]
    public class QuizSurveysController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/QuizSurveys
        
        public IEnumerable<QuizSurveyViewModel> GetQuizSurveys()
        {
            var userr= User.Identity.GetUserId();
            //string userId = RequestContext.Principal;
            return db.QuizSurveys.Where(qu=>qu.UserId ==userr).Select(qs => new QuizSurveyViewModel
            {
                QuizSurveyId = qs.QuizSurveyId,
                QuizSurveyTitle = qs.QuestionSurveyTitle,
                Reference = qs.Reference.ToString(),
                CreatedAt = qs.CreatedAt,
                LastUpdated = qs.UpdatedAt,
                Type = qs.Type,
                AttendeCount = db.QuizTaker.Count(x => x.QuizSurveyId == qs.QuizSurveyId)
            }).ToList();
        }


        [AllowAnonymous]
        [Route("api/public/QuizSurveys")]
        public IEnumerable<QuizSurveyViewModel> GetPublicQuizzes()
        {
            return  db.QuizSurveys.Select(qs => new QuizSurveyViewModel
            {
                QuizSurveyId = qs.QuizSurveyId,
                QuizSurveyTitle = qs.QuestionSurveyTitle,
                Reference = qs.Reference.ToString(),
                CreatedAt = qs.CreatedAt,
                LastUpdated = qs.UpdatedAt,
                Type = qs.Type,
                AttendeCount = db.QuizTaker.Count(x => x.QuizSurveyId == qs.QuizSurveyId)
            }).ToList();
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/public/Share")]
        public async Task<IHttpActionResult> PostShareQuiz(QuizSurveyShareViewModel model)
        {
            EmailManger em = new EmailManger();
           bool result = em.SendEmail(model);

            return Ok(result);
        }

        // GET: api/QuizSurveys/5
        [ResponseType(typeof(QuizSurveyViewModel))]
        public async Task<IHttpActionResult> GetQuizSurvey(int id)
        {
            QuizSurvey quizSurvey = await db.QuizSurveys.FindAsync(id);
            if (quizSurvey == null)
            {
                return NotFound();
            }
            QuizSurveyViewModel model = db.QuizSurveys.Select(x => new QuizSurveyViewModel
            {
                CreatedAt = x.CreatedAt,
                LastUpdated = x.UpdatedAt,
                QuizSurveyId = x.QuizSurveyId,
                QuizSurveyTitle = x.QuestionSurveyTitle,
                Description = x.Description,
                Reference = x.Reference.ToString(),
                
                Questions = x.Questions.Select(q => new QuestionViewModel
                {
                    QuestionId = q.QuestionId,
                    QuestionTitle = q.QuestionTitle,
                    
                    
                }).ToList()
            }).Where(y => y.QuizSurveyId == id).FirstOrDefault();
            return Ok(model);
        }

        // PUT: api/QuizSurveys/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQuizSurvey(int id, QuizSurvey quizSurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quizSurvey.QuizSurveyId)
            {
                return BadRequest();
            }

            quizSurvey.UpdatedAt = DateTime.Now;
            db.Entry(quizSurvey).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizSurveyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/QuizSurveys
        [ResponseType(typeof(QuizSurvey))]
        public async Task<IHttpActionResult> PostQuizSurvey(QuizSurvey quizSurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            quizSurvey.UserId = User.Identity.GetUserId();
            db.QuizSurveys.Add(quizSurvey);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = quizSurvey.QuizSurveyId }, quizSurvey);
        }

        // DELETE: api/QuizSurveys/5
        [ResponseType(typeof(QuizSurvey))]
        public async Task<IHttpActionResult> DeleteQuizSurvey(int id)
        {
            QuizSurvey quizSurvey = await db.QuizSurveys.FindAsync(id);
            if (quizSurvey == null)
            {
                return NotFound();
            }

            db.QuizSurveys.Remove(quizSurvey);
            await db.SaveChangesAsync();

            return Ok(quizSurvey);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuizSurveyExists(int id)
        {
            return db.QuizSurveys.Count(e => e.QuizSurveyId == id) > 0;
        }
    }
}