using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Author
    {
        public Author()
        {
            Articles = new HashSet<Article>();
            Payrolls = new HashSet<Payroll>();
        }

        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Payroll> Payrolls { get; set; }
    }
}
