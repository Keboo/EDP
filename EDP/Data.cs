using System;
using System.Collections.Generic;
using System.Linq;

namespace EDP
{
    public class Data
    {
        public Data(DataDescription description, IEnumerable<DataObject> items)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (items == null) throw new ArgumentNullException(nameof(items));
            Description = description;
            Items = items;
        }

        public static Data Empty => new Data(DataDescription.Empty, Enumerable.Empty<DataObject>());

        public DataDescription Description { get; }

        public IEnumerable<IDataObject> Items { get; }
    }
}