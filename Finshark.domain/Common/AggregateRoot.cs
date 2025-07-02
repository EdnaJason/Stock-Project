using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinShark.Domain.Common
{
    public abstract class AggregateRoot
    {
        public int Id { get; set; }
        //public DateTime CreatedOn { get; protected set; } = DateTime.Now;
        //public DateTime? UpdatedOn { get; protected set; }
    }
}
