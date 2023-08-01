using MariaCiolca_MVC.Models;

namespace MariaCiolca_MVC.ViewModel
{
    public class MemberCodeSnippetViewModel
    {
        public string Name { get; set; }    
        public string Position { get; set; }

        public List<CodeSnippetModel> CodeSnippets = new List<CodeSnippetModel>();
    }
}
