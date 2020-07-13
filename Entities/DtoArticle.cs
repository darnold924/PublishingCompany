using System;

namespace Entities
{
    public class DtoArticle
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
