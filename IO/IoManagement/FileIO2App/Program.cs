using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FileHandlingArticleApp
{
    class Program
    {
        static void Main(string[] args)
        {
             if (File.Exists("test.txt"))
            {
                string content = File.ReadAllText("test.txt");
                System.Diagnostics.Debug.WriteLine("Current content of file:");
                System.Diagnostics.Debug.WriteLine(content);
            }
            Console.WriteLine("Please enter new content for the file:");
            string newContent = Console.ReadLine();
            //File.WriteAllText("test.txt", newContent);
            /*
            while (newContent != "exit")
            {
                File.AppendAllText("test.txt", newContent + System.Environment.NewLine);
                Console.WriteLine("Please enter new content for the file:");
                newContent = Console.ReadLine();
            }
            */
            
            /*
            // Efficienter, want bestand wordt maar eenmaal geopend en gesloten:
            using (StreamWriter sw = new StreamWriter("test.txt")) // using owv de verplicht uit te voeren .Close() om resources vrij te geven!
            {
                while (newContent != "exit")
                {
                    sw.Write(newContent + Environment.NewLine);
                    Console.WriteLine("Please enter new content for the file:");
                    newContent = Console.ReadLine();
                }
            }
            
            FileInfo fi = new(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (fi != null)
                System.Diagnostics.Debug.WriteLine(String.Format("Information about file: {0}, {1} bytes, last modified on {2} - Full path: {3}", fi.Name, fi.Length, fi.LastWriteTime, fi.FullName));

            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            if (di != null)
            {
                FileInfo[] subFiles = di.GetFiles();
                if (subFiles.Length > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Files:");
                    foreach (FileInfo subFile in subFiles)
                    {
                        System.Diagnostics.Debug.WriteLine("   " + subFile.Name + " (" + subFile.Length + " bytes)");
                    }
                }
            }
            */
            var fileContents = File.ReadAllBytes("test.txt"); // dit geeft een array van bytes terug: getallen die overeenkomen met de ascii codes
            foreach(var b in fileContents)
                System.Diagnostics.Debug.Write(Convert.ToChar(b));
            using (var memoryStream = new MemoryStream(fileContents)) // doe alsof dit stuk geheugen een stream is
            {
                using (TextReader textReader = new StreamReader(memoryStream)) // belangrijk omdat de Dispose/Close uitgevoerd moet worden
                {
                    string line;
                    while ((line = textReader.ReadLine()) != null)
                        System.Diagnostics.Debug.WriteLine(line);
                }
            }

            using (var sr = new StreamReader("test.txt"/*, Encoding.ASCII*/))
            {
                string input = sr.ReadLine();
                //sr.Close();
            }
        }
    }
}