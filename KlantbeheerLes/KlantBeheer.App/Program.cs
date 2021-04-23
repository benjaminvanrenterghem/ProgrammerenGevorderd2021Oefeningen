using Klantbeheer.Domain;
using System;

namespace KlantBeheer.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User { Id = 1, Name = "Luc" };

            Console.WriteLine(user);
        }
    }
}
