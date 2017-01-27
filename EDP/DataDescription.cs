using System.Collections.Generic;
using System.Linq;

namespace EDP
{
    public class DataDescription
    {
        public static DataDescription Empty => new DataDescription(Enumerable.Empty<string>());

        public IReadOnlyList<string> Properties { get; }

        public DataDescription(IEnumerable<string> properties)
        {
            Properties = properties.ToList().AsReadOnly();
        }
    }
}