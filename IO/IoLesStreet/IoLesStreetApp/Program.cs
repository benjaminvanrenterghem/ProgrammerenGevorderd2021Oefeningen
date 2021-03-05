using IoLesStreet;
using System;

namespace IoLesStreetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileProcessor = new FileProcessor(@"c:\tmp", "extract", "adresInfo.zip"); // root directory, subdirectory voor extractie, bestandsnaam zip file
            fileProcessor.UnZip();
            fileProcessor.ReadFiles("adresInfo.txt");
            fileProcessor.Dump("Zoersel");
        }
    }
}
