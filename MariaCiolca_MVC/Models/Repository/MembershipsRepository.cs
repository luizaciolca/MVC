using MariaCiolca_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace MariaCiolca_MVC.Models.Repository
{
    public class MembershipsRepository
    {
        private readonly ClubLibraContext _context;

        public MembershipsRepository(ClubLibraContext context)
        {
            _context = context;
        }

        public DbSet<MembershipsModel> GetAllMemberships()
        {
            return _context.Memberships;
        }

        public void Delete(Guid idMembership) //metoda care face delate ul
        {
            var memberships = _context.Memberships.FirstOrDefault(x => x.idMembership == idMembership);
            _context.Memberships.Remove(memberships);
            _context.SaveChanges();
        }

        public MembershipsModel GetMembershipsById(Guid id)  //imi ia delet
        {
            return _context.Memberships.FirstOrDefault(x => x.idMembership == id);
        }

        public void AddMemberships(MembershipsModel memberships)
        {
            memberships.idMembership = Guid.NewGuid();

            _context.Memberships.Add(memberships);
            _context.SaveChanges();
        }

        public void UpdateMemberships(MembershipsModel memberships)
        {
            _context.Memberships.Update(memberships);
            _context.SaveChanges();

        }

    }

}



