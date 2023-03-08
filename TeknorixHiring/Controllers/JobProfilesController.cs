using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TeknorixHiring.Models;

namespace TeknorixHiring.Controllers
{
    public class JobProfilesController : ApiController
    {
        private TeknorixEntities db = new TeknorixEntities();

        // GET: api/JobProfiles
        public IQueryable<JobProfile> GetJobProfiles()
        {
            return db.JobProfiles;
        }

        // GET: api/JobProfiles/5
        [ResponseType(typeof(JobProfile))]
        public IHttpActionResult GetJobProfile(int id)
        {
            JobProfile jobProfile = db.JobProfiles.Find(id);
            if (jobProfile == null)
            {
                return NotFound();
            }

            return Ok(jobProfile);
        }

        // PUT: api/JobProfiles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobProfile(int id, JobProfile jobProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobProfile.Id)
            {
                return BadRequest();
            }

            db.Entry(jobProfile).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobProfileExists(id))
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

        // POST: api/JobProfiles
        [ResponseType(typeof(JobProfile))]
        public IHttpActionResult PostJobProfile(JobProfile jobProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobProfiles.Add(jobProfile);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jobProfile.Id }, jobProfile);
        }

        // DELETE: api/JobProfiles/5
        [ResponseType(typeof(JobProfile))]
        public IHttpActionResult DeleteJobProfile(int id)
        {
            JobProfile jobProfile = db.JobProfiles.Find(id);
            if (jobProfile == null)
            {
                return NotFound();
            }

            db.JobProfiles.Remove(jobProfile);
            db.SaveChanges();

            return Ok(jobProfile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobProfileExists(int id)
        {
            return db.JobProfiles.Count(e => e.Id == id) > 0;
        }
    }
}