using MayNghien.Common.Models.Entity;

namespace BudgetManBackEnd.DAL.Models.Entity
{
    public class AccountInfo : BaseEntity
    {
        public string UserId { get; set; }

        public string Name { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }

        public double Balance { get; set; }

    }
}
