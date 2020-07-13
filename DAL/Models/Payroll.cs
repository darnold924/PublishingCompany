namespace DAL.Models
{
    public partial class Payroll
    {
        public int PayrollId { get; set; }
        public int AuthorId { get; set; }
        public int? Salary { get; set; }

        public virtual Author Author { get; set; }
    }
}
