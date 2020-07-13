namespace PublishingCompany.Models
{
    public interface IPayRollDM
    {
        PayRollVM GetAll();
        PayRollVM.Payroll Find(int id);
        bool BlnFindPayRollByAuthorId(int id);
        PayRollVM.Payroll Add();
        void Add(PayRollVM.Payroll author);
        PayRollVM.Payroll Update(int id);
        void Update(PayRollVM.Payroll author);
        void Delete(int id);
        PayRollVM.Payroll PopulateSelectedList(PayRollVM.Payroll payroll);
    }
}
