using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Democracy.Models;

namespace Democracy.Controllers
{
    [Authorize]
    public class VotingsController : Controller
    {
        private DemocracyContext db = new DemocracyContext();

        // GET: Votings
        public ActionResult Index()
        {
            var votings = db.Votings.Include(v => v.State);
            return View(votings.ToList());
        }

        // GET: Votings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voting voting = db.Votings.Find(id);
            if (voting == null)
            {
                return HttpNotFound();
            }
            return View(voting);
        }

        // GET: Votings/Create
        public ActionResult Create()
        {
            ViewBag.StateId = new SelectList(db.State, "StateId", "Descripcion");
            var view = new VotingView
            {
                DateStart=DateTime.Now,
                DateEnd=DateTime.Now,
            };

            return View(view);
        }

        // POST: Votings/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( VotingView view)
        {
            if (ModelState.IsValid)
            {
                var voting=new Voting
                {
                    DateTimeEnd=view.DateEnd
                                .AddHours(view.TimeEnd.Hour)
                                .AddMinutes(view.TimeEnd.Minute),
                    DateTimeStart = view.DateStart
                                .AddHours(view.TimeStart.Hour)
                                .AddMinutes(view.TimeStart.Minute),
                    Description=view.Description,
                    IsEnableBlakVote=view.IsEnableBlakVote,
                    IsForAllUsers=view.IsForAllUsers,
                    Remarks=view.Remarks,
                    StateId = view.StateId,

                };
                db.Votings.Add(voting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateId = new SelectList(db.State, "StateId", "Descripcion", view.StateId);
            return View(view);
        }

        // GET: Votings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var voting = db.Votings.Find(id);
            if (voting == null)
            {
                return HttpNotFound();
            }

            var view = new VotingView
            {
                DateEnd = voting.DateTimeEnd,
                DateStart = voting.DateTimeStart,
                Description=voting.Description,
                IsEnableBlakVote=voting.IsEnableBlakVote,
                IsForAllUsers=voting.IsForAllUsers,   
                Remarks=voting.Remarks,
                StateId=voting.StateId,
                TimeEnd=voting.DateTimeEnd,
                TimeStart=voting.DateTimeStart,
                VotingId=voting.VotingId,

            };

            ViewBag.StateId = new SelectList(db.State, "StateId", "Descripcion", voting.StateId);
            return View(view);
        }

        // POST: Votings/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VotingView view)
        {
            if (ModelState.IsValid)
            {
                var voting = new Voting
                {
                    DateTimeEnd = view.DateEnd
                                .AddHours(view.TimeEnd.Hour)
                                .AddMinutes(view.TimeEnd.Minute),
                    DateTimeStart = view.DateStart
                                .AddHours(view.TimeStart.Hour)
                                .AddMinutes(view.TimeStart.Minute),
                    Description = view.Description,
                    IsEnableBlakVote = view.IsEnableBlakVote,
                    IsForAllUsers = view.IsForAllUsers,
                    Remarks = view.Remarks,
                    StateId = view.StateId,
                    VotingId=view.VotingId,

                };

                db.Entry(voting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateId = new SelectList(db.State, "StateId", "Descripcion", view.StateId);
            return View(view);
        }

        // GET: Votings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voting voting = db.Votings.Find(id);
            if (voting == null)
            {
                return HttpNotFound();
            }
            return View(voting);
        }

        // POST: Votings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voting voting = db.Votings.Find(id);
            db.Votings.Remove(voting);
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
