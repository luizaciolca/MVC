using MariaCiolca_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace MariaCiolca_MVC.Models.Repository
{
    public class CodeSnippetRepository
    {
        private readonly ClubLibraContext _context;

        public CodeSnippetRepository(ClubLibraContext context)
        {
            _context = context;
        }

        public DbSet<CodeSnippetModel> GetAllCodeSnippet()
        {
            return _context.CodeSnippets;
        }
        public void Delete(Guid idCodeSnippet) //metoda care face delate ul
        {
            var codesnippet = _context.CodeSnippets.FirstOrDefault(x => x.idCodeSnippet == idCodeSnippet);
            _context.CodeSnippets.Remove(codesnippet);
            _context.SaveChanges();
        }

        public CodeSnippetModel GetCodeSnippetById(Guid id)  //imi ia delet
        {
            return _context.CodeSnippets.FirstOrDefault(x => x.idCodeSnippet == id);
        }

        public void AddCodeSnippet(CodeSnippetModel codesnippet)
        {
            codesnippet.idCodeSnippet = Guid.NewGuid(); //genereaza un nou identificator

            _context.CodeSnippets.Add(codesnippet);
            _context.SaveChanges();
        }

        public void UpdateCodeSnippet(CodeSnippetModel codesnippet)
        {
            _context.CodeSnippets.Update(codesnippet);
            _context.SaveChanges();
        }
    }
}
