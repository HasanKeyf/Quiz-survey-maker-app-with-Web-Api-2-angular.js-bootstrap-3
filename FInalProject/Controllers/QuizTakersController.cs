using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FInalProject.Data;
using FInalProject.Data.Models;
using FInalProject.Models;

namespace FInalProject.Controllers
{
    public class QuizTakersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/QuizTakers
        public IQueryable<QuizTaker> GetQuizTaker()
        {
            return db.QuizTaker;
        }


        //

        // GET: api/QuizAttendees
        [Route("api/QuizAttendees/{id:int}")]
        public IQueryable<QuizTakerViewModel> GetQuizAttendees(int id)
        {
            return db.QuizTaker.Where(qt=> qt.QuizSurveyId ==id).Select(x=> new QuizTakerViewModel{
            FullName = x.FullName,
            QuizTakerId = x.QuizSurveyId,
            CorrectCount = db.QuestionAnswers.Count(c=> c.QuizId == id && c.QuizTakerId == x.QuizTakerId && c.Answer.IsCorrectAnswer==true),
            WrongCount = db.QuestionAnswers.Count(c=> c.QuizId == id && c.QuizTakerId == x.QuizTakerId && c.Answer.IsCorrectAnswer==false)
            } );
        }


        // GET: api/QuizTakers/5
        [ResponseType(typeof(QuizTaker))]
        public async Task<IHttpActionResult> GetQuizTaker(int id)
        {
            QuizTaker quizTaker = await db.QuizTaker.FindAsync(id);
            if (quizTaker == null)
            {
                return NotFound();
            }

            return Ok(quizTaker);
        }

        // PUT: api/QuizTakers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQuizTaker(int id, QuizTaker quizTaker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quizTaker.QuizTakerId)
            {
                return BadRequest();
            }

            db.Entry(quizTaker).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizTakerExists(id))
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

        // POST: api/QuizTakers
        [ResponseType(typeof(QuizTaker))]
        public async Task<IHttpActionResult> PostQuizTaker(QuizTaker quizTaker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
         
            db.QuizTaker.AddOrUpdate(x=>x.Email,quizTaker);

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = quizTaker.QuizTakerId }, quizTaker);
        }

        // DELETE: api/QuizTakers/5
        [ResponseType(typeof(QuizTaker))]
        public async Task<IHttpActionResult> DeleteQuizTaker(int id)
        {
            QuizTaker quizTaker = await db.QuizTaker.FindAsync(id);
            if (quizTaker == null)
            {
                return NotFound();
            }

            db.QuizTaker.Remove(quizTaker);
            await db.SaveChangesAsync();

            return Ok(quizTaker);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuizTakerExists(int id)
        {
            return db.QuizTaker.Count(e => e.QuizTakerId == id) > 0;
        }
    }
}