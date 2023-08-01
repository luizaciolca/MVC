using MariaCiolca_MVC.Models;
using MariaCiolca_MVC.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using NuGet.Protocol.Core.Types;
using System.Data;

namespace MariaCiolca_MVC.Controllers
{
    public class CodeSnippetsController : Controller
    {
        private readonly CodeSnippetRepository _repository;
        private readonly IToastNotification _toastNotification;
        private readonly MembersRepository _membersRepository; //injectez repository ul de membrii

        public CodeSnippetsController(CodeSnippetRepository repository, MembersRepository membersRepository)
        {
            _repository = repository;
            _membersRepository = membersRepository;
        }
        // GET: CodeSnippetController
        public ActionResult Index()
        {

            var members = _membersRepository.GetAllMembers();
            ViewBag.Members = members;
            var codesnippets = _repository.GetAllCodeSnippet();
           // _toastNotification.AddInfoToastMessage("Se incarca toate codurile");

            return View("Index",codesnippets);
        }

        // GET: CodeSnippetsController/Details/5
        public ActionResult Details(Guid id)
        {
            var codesnippet = _repository.GetCodeSnippetById(id);
            return View(codesnippet);
            
        }

        // GET: CodeSnippetsController/Create
    
        public ActionResult Create()
        {
            var members = _membersRepository.GetAllMembers(); 
            ViewBag.Members = members; //atasez lista de membri


            return View(); //se returneaza formularul gok
        }

        // POST: CodeSnippetsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            CodeSnippetModel codeSnippetModel = new CodeSnippetModel();   //creez un codesnippet gol

            try
            {
                TryUpdateModelAsync(codeSnippetModel);
                _repository.AddCodeSnippet(codeSnippetModel);
                _toastNotification.AddSuccessToastMessage("Codul s-a adaugat cu succes");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("A aparut o eroare la salvarea codului");
                return View();
            }
        }

        // GET: CodeSnippetsController/Edit/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Edit(Guid id)
        {
            var codesnippet = _repository.GetCodeSnippetById(id);
            return View(codesnippet);
        }

        // POST: CodeSnippetsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
               CodeSnippetModel codeSnippetModel = new CodeSnippetModel();
                TryUpdateModelAsync(codeSnippetModel);
                _repository.UpdateCodeSnippet(codeSnippetModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CodeSnippetsController/Delete/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Delete(Guid id)
        {
            var announcement = _repository.GetCodeSnippetById(id);
            return View(announcement);
            
        }

        // POST: CodeSnippetsController/Delete/5
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
        public static implicit operator CodeSnippetsController(CodeSnippetRepository v)
        {
            throw new NotImplementedException();
        }

    }
        }


