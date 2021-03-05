using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace IoLesStreet
{
    public class FileProcessor
    {
        public string RootDirectory { get; }
        public string ExtractionSubdirectory { get; }
        public string Filename { get; }

        private Dictionary<string, Dictionary<string, SortedSet<string>>> _data = new Dictionary<string, Dictionary<string, SortedSet<string>>>(); // key: stad, key: provincie, set: straten
        
        public FileProcessor(string rootDirectory, string extractionDirectory, string filename)
        {
            RootDirectory = rootDirectory;
            ExtractionSubdirectory = extractionDirectory;
            Filename = filename;
        }

        public void Dump(string gemeente)
        {            
            if(_data.ContainsKey(gemeente))
            {
                foreach(var province in _data[gemeente].Keys) // provincies
                {
                    System.Diagnostics.Debug.WriteLine("Gemeente " + gemeente + " in " + province + ":");
                    foreach(var s in _data[gemeente][province])
                    {
                        System.Diagnostics.Debug.WriteLine("\t" + s); // tab
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Village " + gemeente + " not found");
            }
        }

        public void ReadFiles(string fileName)
        {
            string p = Path.Combine(RootDirectory, ExtractionSubdirectory, fileName);
            using(var sr = new StreamReader(p))
            {
                string[] names; // provincie + dorp + straat
                while((names = sr.ReadLine()?.Trim()?.Split(",")) != null) // einde bestand: stoppen
                {
                    if(!_data.ContainsKey(names[1])) // zit het dorp er al in
                    {
                        _data.Add(names[1], new Dictionary<string, SortedSet<string>> { { names[0], new SortedSet<string>() { names[2] } } });
                    }
                    else
                    {
                        var d = _data[names[1]]; // dorp is al bekend
                        if (!d.ContainsKey(names[0]))
                            d.Add(names[0], new SortedSet<string>() { names[2] }); // nieuwe provincie
                        else
                            d[names[0]].Add(names[2]); // enkel nog de straat toe te voegen
                    }
                }
            }
        }

        public void UnZip()
        {
            try
            {
                var source = Path.Combine(RootDirectory, Filename);
                var destination = Path.Combine(RootDirectory, ExtractionSubdirectory);
                ZipFile.ExtractToDirectory(source, destination);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
