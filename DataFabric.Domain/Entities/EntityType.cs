using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataFabric.Domain.Entities
{
    public class EntityType : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public ICollection<EntityField> Fields { get; set; } = new List<EntityField>();

        //public Entity(Guid id, string name, string? Description, bool IsSystem, List<Field> fields)
        //{
        //    this.Id = id;
        //    this.Name = name;
        //    this.Description = Description;
        //    this.IsSystem = IsSystem;
        //    this.Fields = fields;
        //}
    }
}
