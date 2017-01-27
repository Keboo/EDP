using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EDP.Parser
{
    public class RegexParser : IParser
    {
        private readonly FileInfo _inputFile;
        private readonly Regex _regex;

        public RegexParser(Regex regex, FileInfo inputFile)
        {
            if (regex == null) throw new ArgumentNullException(nameof(regex));
            if (inputFile == null) throw new ArgumentNullException(nameof(inputFile));
            _inputFile = inputFile;
            _regex = regex;
        }

        public Data Parse()
        {
            if (_inputFile != null)
                return ParseFile();
            return Data.Empty;
        }

        private Data ParseFile()
        {
            return new Data(new DataDescription(_regex.GetGroupNames().Skip(1)), ParseFileItems(_inputFile, _regex));
        }

        private static IEnumerable<DataObject> ParseFileItems(FileInfo file, Regex regex)
        {
            using (var streamReader = new StreamReader(file.OpenRead()))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    if (line == null) continue;
                    Match match = regex.Match(line);
                    if (!match.Success) continue;

                    var obj = new DataObject();

                    foreach (string group in regex.GetGroupNames().Skip(1))
                    {
                        obj.Set(group, match.Groups[group].Value);
                    }
                    yield return obj;
                }
            }
        }
    }
}