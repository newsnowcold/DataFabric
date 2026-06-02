using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFabric.Domain.Entities
{
    public class EntityRecord : BaseEntity
    {
        public int Id { get; set; }

        public int EntityTypeId { get; set; }

        // Optional: frequently queried fields (indexed)
        public string? Name { get; set; }

        // Main dynamic data storage
        public string Data { get; set; } = "{}"; // JSON
    }
}
