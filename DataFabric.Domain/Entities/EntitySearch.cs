using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFabric.Domain.Entities
{
    public class EntitySearch
    {
        public int EntityRecordId { get; set; }

        public string FieldName { get; set; } = default!;
        public string? Value { get; set; }
    }
}
