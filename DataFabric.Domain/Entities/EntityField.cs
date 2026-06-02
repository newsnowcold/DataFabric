using DataFabric.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFabric.Domain.Entities
{
    public class EntityField : BaseEntity
    {
        public int Id { get; set; }

        public int EntityTypeId { get; set; }

        public string Name { get; set; } = default!;

        public FieldDataType DataType { get; set; }

        public bool IsHidden { get; set; }

        public bool IsRequired { get; set; }

        public bool IsUnique { get; set; }

        public bool IsSearchable { get; set; }

        public bool IsFilterable { get; set; }

        public int? MaxLength { get; set; }

        public string? DefaultValue { get; set; }

        public int Order { get; set; }
    }
}
