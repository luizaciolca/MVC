using MariaCiolca_MVC.Controllers;
using MariaCiolca_MVC.Data;
using MariaCiolca_MVC.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MariaCiolca_MVC.Models.Repository
{
    public class MembersRepository
    {
        public readonly ClubLibraContext _context;



        public MembersRepository(ClubLibraContext context)
        {
            _context = context;
        }
        public DbSet <MemberModel> GetAllMembers()
        {
            return _context.Members;
        }

        public MemberModel GetMemberById(Guid id)
        {
            MemberModel member = _context.Members.FirstOrDefault(x => x.IdMember == id);
            return member;
        }

        public void AddMember(MemberModel member)
        {
            member.IdMember = Guid.NewGuid();

            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public void UpdateMember(MemberModel member)
        {
            _context.Members.Update(member);
            _context.SaveChanges();
        }

     public void Delete(Guid id) //metoda care face delate ul
        {
            MemberModel model = GetMemberById(id);
            _context.Members.Remove(model);
            _context.SaveChanges(); 
        }

        public MemberCodeSnippetViewModel GetMemberCodeSnippets(Guid id)
        {
            MemberCodeSnippetViewModel memberCodeSnippetViewModel = new MemberCodeSnippetViewModel();

                MemberModel member = GetMemberById(id);

            if(member != null) 
            {
                memberCodeSnippetViewModel.Name = member.Name;
                memberCodeSnippetViewModel.Position = member.Position;

                memberCodeSnippetViewModel.CodeSnippets = _context.CodeSnippets.Where( x => x.idMember == member.IdMember).ToList();
            }
            return memberCodeSnippetViewModel;
        }

        public bool HasCodeSnippets(Guid id)
        {
            return _context.CodeSnippets.Any( x => x.idMember == id); //any imi retuneaza daca exista code snippet uri cu id de membru

        }
    }
}
