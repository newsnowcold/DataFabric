using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataFabric.Domain.Helpers
{
    public static class JsonHelper
    {
        public static Dictionary<string, object?> Deserialize(string json)
        {
            return JsonSerializer.Deserialize<Dictionary<string, object?>>(json)
                   ?? new Dictionary<string, object?>();
        }

        public static string Serialize(Dictionary<string, object?> data)
        {
            return JsonSerializer.Serialize(data);
        }
    }
}
