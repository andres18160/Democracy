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

        public ActionResult DeleteCandidate(int id)
        {
            var candidate = db.Candidates.Find(id);
            if (candidate != null)
            {
                db.Candidates.Remove(candidate);
                db.SaveChanges();
            }
            return RedirectToAction(string.Format("Details/{0}", candidate.VotingId));
        }

        public ActionResult DeleteGroup(int id)
        {
            var votingGroup = db.VotingGoups.Find(id);
            if (votingGroup != null)
            {
                db.VotingGoups.Remove(votingGroup);
                db.SaveChanges();
            }
            else
            {
               // return HttpNotFound();
            }
           return RedirectToAction(string.Format("Details/{0}", votingGroup.VotingId));
        }

        [HttpGet]
        public ActionResult AddCandidate(int id)
        {
            var view = new AddCandidateView
            {
                VotingId=id,
            };
            ViewBag.userId = new SelectList
                (db.Users.OrderBy(u=>u.FirstName)
                .ThenBy(u=>u.LastName),"UserId","FullName");
            return View(view);
        }
        [HttpPost]
        public ActionResult AddCandidate(AddCandidateView view)
        {
            if (ModelState.IsValid)
            {
                var candidate = db.Candidates
                    .Where(c => c.VotingId == view.VotingId &&
                           c.UserId == view.UserId)
                           .FirstOrDefault();
                if (candidate != null)
                {
                    ModelState.AddModelError(string.Empty, "The candidate already belongs to voting");

                    ViewBag.userId = new SelectList
                       (db.Users.OrderBy(u => u.FirstName)
                       .ThenBy(u => u.LastName), "UserId", "FullName");
                    return View(view);
                }

                candidate = new Candidate
                {
                    UserId = view.UserId,
                    VotingId = view.VotingId,
                };

                db.Candidates.Add(candidate);
                db.SaveChanges();
                return RedirectToAction(string.Format("Details/{0}", view.VotingId));
            }
            ViewBag.userId = new SelectList
                    (db.Users.OrderBy(u => u.FirstName)
                    .ThenBy(u => u.LastName), "UserId", "FullName");
            return View(view);
        }


        [HttpGet]
        public ActionResult AddGroup(int id)
        {
            ViewBag.GroupId = new SelectList(
                   db.Groups.OrderBy(g=>g.Descripcion),
                   "GroupId", "Descripcion");
            var view = new AddGroupView
            {
                VotingId=id,
            };

            return View(view);
        }
        [HttpPost]
        public ActionResult AddGroup(AddGroupView view)
        {
            if (ModelState.IsValid)
            {
                var votingGroup = db.VotingGoups
                    .Where(vg => vg.VotingId == view.VotingId &&
                           vg.GroupId == view.GroupId)
                           .FirstOrDefault();
                if (votingGroup != null)
                {
                    ModelState.AddModelError(string.Empty, "The group already belongs to voting.");
                 

                    ViewBag.GroupId = new SelectList(
                              db.Groups.OrderBy(g => g.Descripcion),
                              "GroupId", "Descripcion");
                    return View(view);
                }

                votingGroup = new VotingGoup
                {
                    GroupId=view.GroupId,
                    VotingId=view.VotingId,
                };

                db.VotingGoups.Add(votingGroup);
                db.SaveChanges();
                return RedirectToAction(string.Format("Details/{0}", view.VotingId));
            }
            ViewBag.GroupId = new SelectList(
                   db.Groups.OrderBy(g => g.Descripcion),
                   "GroupId", "Descripcion");
            return View(view);
        }
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
            var view = new DetailsVotingView
            {
               Candidates=voting.Candidates.ToList(),
               CandidateWinId=voting.CandidateWinId,
               DateTimeEnd=voting.DateTimeEnd,
               DateTimeStart=voting.DateTimeStart,
               Description=voting.Description,
               IsEnableBlakVote=voting.IsEnableBlakVote,
               IsForAllUsers=voting.IsForAllUsers,
               QuantityBlackVotes=voting.QuantityBlackVotes,
               QuantityVotes=voting.QuantityVotes,
               Remarks=voting.Remarks,
               StateId=voting.StateId,
               VotingGroups=voting.VotingGroups.ToList(),
               VotingId=voting.VotingId,
               
            };
            return View(view);
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
