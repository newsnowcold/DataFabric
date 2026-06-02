using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFabric.Domain.ValueObjects
{
    public class DynamicFieldValue
    {
        public string FieldName { get; set; } = default!;

        public object? Value { get; set; }

        public Type ValueType { get; set; } = typeof(string);
    }
}
