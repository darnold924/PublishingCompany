using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublishingCompany.Models
{
    public class AuthorVM
    {
        public class Author
        {
            public int AuthorID { get; set; }

            [Required(ErrorMessage = "First Name is required")]
            [StringLength(50)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Last Name is required")]
            [StringLength(50)]
            public string LastName { get; set; }
        }

        public List<Author> Authors { get; set; } = new List<Author>();
    }
}
