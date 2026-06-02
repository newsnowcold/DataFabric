using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFabric.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
        public string CreatedBy { get; set; } = "System";
        public string? ModifiedBy { get; set; }
        public string AgencyId { get; set; } = default!;
    }
}
