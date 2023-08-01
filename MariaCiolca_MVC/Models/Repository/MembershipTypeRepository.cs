using MariaCiolca_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace MariaCiolca_MVC.Models.Repository
{
    public class MembershipTypeRepository
    {
        private readonly ClubLibraContext _context;

        public MembershipTypeRepository(ClubLibraContext context)
        {
            _context = context;
        }

        public DbSet<MembershipTypesModel> GetAllMembershipTypes()
        {
            return _context.MembershipTypes;
        }

        public void Delete(Guid idMembershipTypes)
        {
            var membershipTypes = _context.MembershipTypes.FirstOrDefault(x => x.idMembershipType == idMembershipTypes);
            _context.MembershipTypes.Remove(membershipTypes);
            _context.SaveChanges();
        }

        public MembershipTypesModel GetMembershipTypesById(Guid id)
        {
            return _context.MembershipTypes.FirstOrDefault(x => x.idMembershipType == id);

        }

        public void AddMembershipTypes(MembershipTypesModel membershipTypes)
        {
            membershipTypes.idMembershipType = Guid.NewGuid();
            
            _context.MembershipTypes.Add(membershipTypes);
            _context.SaveChanges();
        }

        public void UpdateMembershipTypes(MembershipTypesModel membership)
        {
            _context.MembershipTypes.Update(membership); 
            _context.SaveChanges();
        }


    }
}
