using System;
using System.IO;
using EDP.Transform;

namespace EDP.Writer
{
    public class CsvWriter : IWriter
    {
        private readonly Stream _stream;
        private readonly ITransform[] _transforms;

        public CsvWriter(Stream stream, params ITransform[] transforms)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            _stream = stream;
            _transforms = transforms;
        }

        public void Write(Data data)
        {
            using (var csvWriter = new CsvHelper.CsvWriter(new StreamWriter(_stream)))
            {
                foreach (string property in data.Description.Properties)
                {
                    csvWriter.WriteField(property);
                }
                csvWriter.NextRecord();

                foreach (IDataObject item in data.Items)
                {
                    if (_transforms?.Length > 0)
                    {
                        foreach (ITransform transform in _transforms)
                        {
                            transform.TransformData(data.Description, item);
                        }
                    }

                    foreach (string property in data.Description.Properties)
                    {
                        csvWriter.WriteField(item.Get<string>(property));
                    }
                    csvWriter.NextRecord();
                }
            }
        }
    }
}