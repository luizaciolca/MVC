using MariaCiolca_MVC.Models.Repository;
using MariaCiolca_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MariaCiolca_MVC.Controllers
{
    public class MembershipsController : Controller
    {
        // GET: MembershipsController
        private readonly MembershipsRepository _repository;
        private readonly IToastNotification _toastNotification;
        private readonly MembersRepository _membersRepository;
        private readonly MembershipTypeRepository _membershipTypeRepository;
        // GET: MembershipTypes

        public MembershipsController(MembershipsRepository repository, MembersRepository membersRepository, MembershipTypeRepository membershipTypeRepository, IToastNotification toastNotification)
        {
            _repository = repository;
            _membersRepository = membersRepository;
            _membershipTypeRepository = membershipTypeRepository;
            _toastNotification = toastNotification;
        }
        public ActionResult Index()
        {
            var memberships = _repository.GetAllMemberships();
           _toastNotification.AddInfoToastMessage("Memberships a fost incarcat");
            return View("Index", memberships);
        }

        // GET: MembershipTypes/Details/5
        public ActionResult Details(Guid id)
        {

            var memberships = _repository.GetMembershipsById(id);
            return View(memberships);
        }

        // GET: MembershipTypes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var members = _membersRepository.GetAllMembers();
            ViewBag.Members = members;  
            var membershipType = _membershipTypeRepository.GetAllMembershipTypes();
            ViewBag.MembershipType = membershipType;
            var memberships = _repository.GetAllMemberships();
            return View();
        }

        // POST: MembershipTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            MembershipsModel membershipTypesModel = new MembershipsModel();
            try
            {
                TryUpdateModelAsync(membershipTypesModel);
                _repository.AddMemberships(membershipTypesModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MembershipTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            var memberships = _repository.GetMembershipsById(id);
            return View(memberships);
        }

        // POST: MembershipTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                MembershipsModel membershipsModel = new MembershipsModel();
                TryUpdateModelAsync(membershipsModel);
                _repository.UpdateMemberships(membershipsModel);
                return RedirectToAction(nameof(Index));
                _toastNotification.AddInfoToastMessage("Membership a fost editat cu succes");
            }
            catch
            {
                return View();
            }
        }

        // GET: MembershipTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            var memberships = _repository.GetMembershipsById(id);
            return View(memberships);
        }

        // POST: MembershipTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _repository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
