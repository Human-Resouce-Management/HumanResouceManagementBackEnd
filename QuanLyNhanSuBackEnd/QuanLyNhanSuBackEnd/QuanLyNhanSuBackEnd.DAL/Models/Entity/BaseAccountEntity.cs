using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManBackEnd.DAL.Models.Entity
{
    public class BaseAccountEntity:BaseEntity
    {

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual AccountInfo Account { get; set; }
    }
}
