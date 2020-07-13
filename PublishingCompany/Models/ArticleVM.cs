using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublishingCompany.Models
{
    public class ArticleVM
    {
        public class Article
        {
            public int ArticleId { get; set; }
            public int AuthorId { get; set; }
            public string AuthorName { get; set; }

            [Required(ErrorMessage = "Title is required")]
            [StringLength(50)]
            public string Title { get; set; }
            public string Body { get; set; }
        }

        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
