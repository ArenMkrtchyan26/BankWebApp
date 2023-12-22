namespace BankWebApp.Models
{
    public class CustomerModel
    {
        //public CustomerModel()
        //{
        //    DateOfBirth=DateTime.Now.ToString("yyyy/MM/dd");
        //}
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string FirstName { get; set; }    
        public string LastName { get; set; }
        public string DateOfBirth { get ; set; }
        public string Gender { get; set; }

    }
}
