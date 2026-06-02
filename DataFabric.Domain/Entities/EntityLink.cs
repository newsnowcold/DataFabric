using DataFabric.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFabric.Domain.Entities
{
    public class EntityLink : BaseEntity
    {
        public int Id { get; set; }

        public int SourceEntityId { get; set; }

        public int TargetEntityId { get; set; }

        public RelationshipType RelationshipType { get; set; }

        public string Name { get; set; } = default!;
    }
}
