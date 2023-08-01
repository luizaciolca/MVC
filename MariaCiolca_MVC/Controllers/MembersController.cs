using MariaCiolca_MVC.Data;
using MariaCiolca_MVC.Models;
using MariaCiolca_MVC.Models.Repository;
using MariaCiolca_MVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using NuGet.Protocol.Core.Types;
using System.Linq;
using PagedList;
using X.PagedList; 

namespace MariaCiolca_MVC.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class MembersController : Controller
    {
        private readonly MembersRepository _membersRepository;
        private readonly IToastNotification _toastNotification;
        private readonly ClubLibraContext _context;




        public MembersController(MembersRepository membersRepository, IToastNotification toastNotification, ClubLibraContext context)
        {
            _membersRepository = membersRepository;
            _toastNotification = toastNotification;
            _context = context;
        }

        // GET: MembersController
        //  public async Task<IActionResult> Index(string searchString) 

        public async Task<IActionResult> Index (string sortOder, string currentFilter, string searchString, int? pageNumber) 
        {
            ViewData["CurrentSort"] = sortOder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (_context.Members == null)
            {
                return Problem("Entity set 'MvcMemberContext.Movie' is null.");
            }

            var members = from m in _context.Members
                          select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                members = members.Where(s => s.Title!.Contains(searchString));
            }

          //  return View(await members.ToListAsync());

             int pageSize = 5;
            return View(await CMariaCiolca_MVC.PaginatedList<MemberModel>.CreateAsync(members.AsNoTracking(), pageNumber ?? 1, pageSize));


        }



        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }


        // GET: MembersController/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(Guid id)
        {
            var member = _membersRepository.GetMemberById(id);
            return View(member);
        }

        // GET: MembersController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {

            return View();
        }

        // POST: MembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberModel member)
        {
            MemberModel memberModel = new MemberModel();
            try
            {
                _membersRepository.AddMember(member);

                //TryUpdateModelAsync(memberModel);
                // _membersRepository.AddMember(memberModel);
                _toastNotification.AddSuccessToastMessage("Noul membru s-a adaugat cu succes");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("A aparut o eroare la salvarea noului membru");
                return View();
            }
        }

        // GET: MembersController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            MemberModel member = _membersRepository.GetMemberById(id);
            return View(member);
        }

        // POST: MembersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, MemberModel member)
        {
            try
            {

                _membersRepository.UpdateMember(member);
                _toastNotification.AddSuccessToastMessage("Membrul a fost editat cu succes");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MembersController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            MemberModel member = _membersRepository.GetMemberById(id);
            return View(member);
        }

        // POST: MembersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, MemberModel member)
        {
            try
            {
                if (_membersRepository.HasCodeSnippets(id))
                {
                    _toastNotification.AddErrorToastMessage("Membrul nu poate fi sters pentru ca are cod adaugat");
                }
                else
                {
                    _membersRepository.Delete(id);
                    _toastNotification.AddErrorToastMessage("Membrul a fost sters");
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DetailWithCodeSnippet(Guid id)
        {

            MemberCodeSnippetViewModel viewModel = _membersRepository.GetMemberCodeSnippets(id);

            return View(viewModel);
        }
    }
}
