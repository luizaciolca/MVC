using MariaCiolca_MVC.Models;
using MariaCiolca_MVC.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using NuGet.Protocol.Core.Types;
using System.Data;

namespace MariaCiolca_MVC.Controllers
{
    public class MembershipTypesController : Controller
    {
        private readonly MembershipTypeRepository _repository;
        private readonly IToastNotification _toastNotification;
        // GET: MembershipTypes

        public MembershipTypesController(MembershipTypeRepository repository)
        {
            _repository = repository; 
        }
        public ActionResult Index()
        {
            var membershipTypes = _repository.GetAllMembershipTypes();
          //_toastNotification.AddInfoToastMessage("Se incarca toate tipurile de membru");
            return View("Index", membershipTypes);
        }

        // GET: MembershipTypes/Details/5
        public ActionResult Details(Guid id)
        {
           
            var membershipTypes = _repository.GetMembershipTypesById(id);
            return View(membershipTypes);
        }

        // GET: MembershipTypes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var membershipTypes = _repository.GetAllMembershipTypes();
            return View();
        }

        // POST: MembershipTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            MembershipTypesModel membershipTypesModel = new MembershipTypesModel();
            try
            {
                TryUpdateModelAsync(membershipTypesModel);
                _repository.AddMembershipTypes(membershipTypesModel);
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
            var membershipTypes = _repository.GetMembershipTypesById(id);
            return View(membershipTypes);
        }

        // POST: MembershipTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                MembershipTypesModel membershipTypesModel = new MembershipTypesModel();
                TryUpdateModelAsync(membershipTypesModel);
                _repository.UpdateMembershipTypes(membershipTypesModel);
                return RedirectToAction(nameof(Index));
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
            var membershipTypes = _repository.GetMembershipTypesById(id);
            return View(membershipTypes);
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
