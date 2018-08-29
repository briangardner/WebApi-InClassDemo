﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi_InClassDemo.DAL;
using WebApi_InClassDemo.Models;

namespace WebApi_InClassDemo.Controllers
{
    public class TaskListsController : ApiController
    {
        private TodoDbContext db = new TodoDbContext();

        // GET: api/TaskLists
        public IQueryable<TaskList> GetTaskLists()
        {
            return db.TaskLists;
        }

        // GET: api/TaskLists/5
        [ResponseType(typeof(TaskList))]
        public IHttpActionResult GetTaskList(int id)
        {
            TaskList taskList = db.TaskLists.Include(x=> x.Todos).First(x => x.Id == id);
            if (taskList == null)
            {
                return NotFound();
            }

            return Ok(taskList);
        }

        // PUT: api/TaskLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTaskList(int id, TaskList taskList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskList.Id)
            {
                return BadRequest();
            }

            db.Entry(taskList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskListExists(id))
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

        // POST: api/TaskLists
        [ResponseType(typeof(TaskList))]
        public IHttpActionResult PostTaskList(TaskList taskList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TaskLists.Add(taskList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = taskList.Id }, taskList);
        }

        // DELETE: api/TaskLists/5
        [ResponseType(typeof(TaskList))]
        public IHttpActionResult DeleteTaskList(int id)
        {
            TaskList taskList = db.TaskLists.Find(id);
            if (taskList == null)
            {
                return NotFound();
            }

            db.TaskLists.Remove(taskList);
            db.SaveChanges();

            return Ok(taskList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskListExists(int id)
        {
            return db.TaskLists.Count(e => e.Id == id) > 0;
        }
    }
}