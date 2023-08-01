using MariaCiolca_MVC.Models;
using MariaCiolca_MVC.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;
using System.Runtime.CompilerServices;

namespace MariaCiolca_MVC.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly AnnouncementRepository _repository;
        private readonly IToastNotification _toastNotification;

            public AnnouncementsController(AnnouncementRepository repository, IToastNotification toastnNotification) 
        {
            _repository = repository;
            _toastNotification = toastnNotification;
        }
        // GET: AnnouncementsController
        public ActionResult Index()
        {
            var announcements = _repository.GetAllAnnouncement();
            _toastNotification.AddInfoToastMessage("Se incarca toate anunturile");
            return View(announcements);
        }

        // GET: AnnouncementsController/Details/5
        public ActionResult Details(Guid id)
        {
            var announcements = _repository.GetAnnoucementById(id);
            return View(announcements);
        }

        // GET: AnnouncementsController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnnouncementsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnnoucementModel annoucementModel)
        {
            AnnoucementModel announcementModel = new AnnoucementModel(); //model gol
            try
            {
                if (ModelState.IsValid) //cauta metoda noastra care e valid
                {
                    TryUpdateModelAsync(announcementModel); //returneaza modelul gol cu datele completate
                    _repository.AddAnnouncement(announcementModel);
                    _toastNotification.AddSuccessToastMessage("Anuntul s-a adaugat cu succes");
                    return RedirectToAction(nameof(Index));
                }
                return View(announcementModel); 
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("A aparut o eroare la salvarea anuntului");
                return View();
            }
        }

        // GET: AnnouncementsController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            var announcement = _repository.GetAnnoucementById(id);
            return View(announcement);
        }

        // POST: AnnouncementsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                AnnoucementModel annoucementModel = new AnnoucementModel();
                TryUpdateModelAsync(annoucementModel);
                _repository.UpdateAnnouncement(annoucementModel);
                _toastNotification.AddSuccessToastMessage("Anuntul a fost editat cu succes");


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("A aparut o eroare la editarea anuntului");
                return View();
            }
        }

        // GET: AnnouncementsController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            var announcement = _repository.GetAnnoucementById(id);
            return View(announcement); //returneaza info despre anuntul meu
        }

        // POST: AnnouncementsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _toastNotification.AddWarningToastMessage("Anuntul a fost sters");
                _repository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddWarningToastMessage("Anuntul a fost sters");
                return View();
            }
        }

        public static implicit operator AnnouncementsController(AnnouncementRepository v)
        {
            throw new NotImplementedException();
        }
    }
}
