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
using FInalProject.Data;
using FInalProject.Data.Models;

namespace FInalProject.Controllers
{
    public class QuestionAnswersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/QuestionAnswers
        public IQueryable<QuestionAnswer> GetQuestionAnswers()
        {
            return db.QuestionAnswers;
        }

        // GET: api/QuestionAnswers/5
        [ResponseType(typeof(QuestionAnswer))]
        public async Task<IHttpActionResult> GetQuestionAnswer(int id)
        {
            QuestionAnswer questionAnswer = await db.QuestionAnswers.FindAsync(id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            return Ok(questionAnswer);
        }

        // PUT: api/QuestionAnswers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQuestionAnswer(int id, QuestionAnswer questionAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questionAnswer.QuestionAnserId)
            {
                return BadRequest();
            }

            db.Entry(questionAnswer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionAnswerExists(id))
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

        // POST: api/QuestionAnswers
        [ResponseType(typeof(QuestionAnswer))]
        public async Task<IHttpActionResult> PostQuestionAnswer(QuestionAnswer questionAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.QuestionAnswers.Add(questionAnswer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = questionAnswer.QuestionAnserId }, questionAnswer);
        }

        // DELETE: api/QuestionAnswers/5
        [ResponseType(typeof(QuestionAnswer))]
        public async Task<IHttpActionResult> DeleteQuestionAnswer(int id)
        {
            QuestionAnswer questionAnswer = await db.QuestionAnswers.FindAsync(id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            db.QuestionAnswers.Remove(questionAnswer);
            await db.SaveChangesAsync();

            return Ok(questionAnswer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionAnswerExists(int id)
        {
            return db.QuestionAnswers.Count(e => e.QuestionAnserId == id) > 0;
        }
    }
}