using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace IoManagementApp
{
    [Serializable] // attribuut om complexe classes weg te schrijven en op te lezen
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Wage { get; set; }

        public Student(string name, int age, double wage)
        {
            Name = name;
            Age = age;
            Wage = wage;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ShowDirectoryInfo();
        }

        public static void CreateZipFile(string filename, IEnumerable<string> files)
        {
            var zipFile = ZipFile.Open(filename, ZipArchiveMode.Create); // we maken een nieuw zip bestand
            foreach(var f in files)
            {
                zipFile.CreateEntryFromFile(f, Path.GetFileName(f), CompressionLevel.Optimal);
            }
            // Geef resources vrij:
            zipFile.Dispose();
        }

        private static void ShowDirectoryInfo()
        {
            // Bij IO is het altijd een goed idee om exceptions op te vangen! IO is namelijk met periferie ...
            try
            {
                var wallpaperDirectory = new DirectoryInfo("c:/Windows/Web/Wallpaper");
                var imageFiles = wallpaperDirectory.GetFiles("*.jpg", SearchOption.AllDirectories);
                System.Diagnostics.Debug.WriteLine("We vonden " + imageFiles.Length + " bestanden");

                var dir = new DirectoryInfo(@"c:\tmp\DirectoryTest"); // beter: c:/Windows/... unix manier; deze werkt op windows EN unix
                if (dir.Exists)
                {
                    System.Diagnostics.Debug.WriteLine("Full name: " + dir.FullName);
                    var dirInfo = dir.CreateSubdirectory("MyFolder");
                }

                var myDrives = DriveInfo.GetDrives();
                foreach (var d in myDrives)
                {
                    System.Diagnostics.Debug.WriteLine("Name: " + d.Name);
                    System.Diagnostics.Debug.WriteLine("Format: " + d.DriveType);
                    if (d.IsReady) // zie documentatie
                    {
                        System.Diagnostics.Debug.WriteLine("Total free space: " + d.TotalFreeSpace);
                        System.Diagnostics.Debug.WriteLine("Format: " + d.DriveFormat);
                        System.Diagnostics.Debug.WriteLine("Label: " + d.VolumeLabel);
                    }
                }

                var f1 = new FileInfo("c:/tmp/DirectoryTest/test1.txt");
                var fs1 = f1.Create(); // zal bestand overschrijven als het al bestaat
                fs1.Close();

                var f2 = new FileInfo("c:/tmp/DirectoryTest/test2.txt");
                // met using moet ik .Close() niet doen; als .Close() niet opgeroepen wordt, dan verspil je resources - je loopt uiteindelijk tegen de limieten van Windows
                using (var fs2 = f2.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {

                }

                // Twee soorten IO: (1) text (ascii, ...), (2) binary (bytes)
                // (1) Text
                // --------
                string[] myTasks = { "Fix bathroom sink", "Call Dave", "Call mom and dad", "Play Xbox 1"};
                File.WriteAllLines("c:/tmp/DirectoryTest/test3.txt", myTasks);

                foreach(var t in File.ReadAllLines("c:/tmp/DirectoryTest/test3.txt"))
                {
                    System.Diagnostics.Debug.WriteLine("TO DO: " + t);
                }

                using(StreamWriter sWriter = File.AppendText("c:/tmp/DirectoryTest/test3.txt"))
                {
                    sWriter.WriteLine("Dit is een test");
                }

                // Inlezen lijn per lijn, maar je hebt meteen alles in het geheugen - operaties pas daarna 1 voor 1
                foreach (var t in File.ReadAllLines("c:/tmp/DirectoryTest/test3.txt"))
                {
                    System.Diagnostics.Debug.WriteLine("TO DO: " + t);
                }

                /*
                // Een volledig nieuw bestand:
                using(StreamWriter sWriter2 = new("c:/tmp/DirectoryTest/test3.txt")) // C# 9.0
                {
                    sWriter2.WriteLine("Dit is nog een bijkomende test");
                }
                */

                // Meest gebruikelijke vorm van inlezen van een tekstbestand:
                using(StreamReader sr = File.OpenText("c:/tmp/DirectoryTest/test3.txt"))
                {
                    string input = null;
                    while((input = sr.ReadLine()) != null)
                    {
                        System.Diagnostics.Debug.WriteLine(input);
                    }
                }

                // (2) Binary
                // ----------
                var bf1 = new FileInfo("c:/tmp/DirectoryTest/test4.dat");
                using(var bw = new BinaryWriter(bf1.OpenWrite()))
                {
                    // POD: plain old data types
                    double aDouble = 1234.57;
                    int anInt = 55454;
                    string s = "demo string";
                    bw.Write(aDouble); // let op: in binary geen WriteLine() want dat is voor tekstbestanden
                    bw.Write(anInt);
                    bw.Write(s);
                }

                // Nadeel: ik moet nu zelf weten in welke volgorde welk type weggeschreven is om dit terug op te lezen:
                using (var br = new BinaryReader(bf1.OpenRead()))
                {
                    // Voor alle pod types zijn methods voorgedefinieerd: int, double, float, string, ...
                    var aDouble = br.ReadDouble(); // let op: in binary geen WriteLine() want dat is voor tekstbestanden
                    var anInt = br.ReadInt32();
                    var s = br.ReadString();
                }

                try
                {
                    var path = "c:/tmp/DirectoryTest";
                    var zipPath = "c:/tmp/DirectoryTest.zip";
                    CreateZipFile(zipPath, Directory.EnumerateFiles(path, "*.txt"));
                    var extractPath = "c:/tmp/DirectoryTestExtracted";
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                }
                catch(Exception zipEx)
                {
                    System.Diagnostics.Debug.WriteLine(zipEx.Message);
                }

                // Class/object serialization

                var s1 = new Student("Jef", 25, 1200.5);

                {
                    var binaryFormatter = new BinaryFormatter();
                    using (var stream = new FileStream("c:/tmp/DirectoryTest/MyStudent.bin", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        binaryFormatter.Serialize(stream, s1);
                    }
                    using (var stream = new FileStream("c:/tmp/DirectoryTest/MyStudent.bin", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var s = binaryFormatter.Deserialize(stream) as Student;
                    }
                }
                // De tekst-tegenhanger is: schrijven in json format of xml of ... 
                // Json wordt gedaan met NewtonSoft
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                // Wordt altijd uitgevoerd
                System.Diagnostics.Debug.WriteLine("De operatie zit erop");
            }
        }
    }
}
