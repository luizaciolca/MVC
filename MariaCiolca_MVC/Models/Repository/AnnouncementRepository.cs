using MariaCiolca_MVC.Data;
using MariaCiolca_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MariaCiolca_MVC.Models.Repository
{
    public class AnnouncementRepository
    {
        private readonly ClubLibraContext _context;

        public AnnouncementRepository(ClubLibraContext context)
        {
            _context = context;
        }

        public DbSet<AnnoucementModel> GetAllAnnouncement()
        {
            return _context.Announcements;
        }

        public void Delete(Guid idAnnouncement) //metoda care face delate ul
        {
            var announcement = _context.Announcements.FirstOrDefault(x => x.IdAnnouncement == idAnnouncement);
            _context.Announcements.Remove(announcement);
            _context.SaveChanges(); }

        public AnnoucementModel GetAnnoucementById(Guid id)  //imi ia delet
        {
            return _context.Announcements.FirstOrDefault(x => x.IdAnnouncement == id);
        }

        public void AddAnnouncement(AnnoucementModel announcement)
        {
            announcement.IdAnnouncement = Guid.NewGuid();

            _context.Announcements.Add(announcement);
            _context.SaveChanges();
        }

        public void UpdateAnnouncement(AnnoucementModel announcement)
        {
            _context.Announcements.Update(announcement);
            _context.SaveChanges();

            


        }
       
        }
       
        }


