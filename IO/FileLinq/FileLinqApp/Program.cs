using System;
using System.Collections.Generic;
using System.Linq; // let op!
using System.Globalization;
using System.Xml.Linq;

namespace FileLinqApp
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Mail { get; set; }
        public string HomeCountry { get; set; }

        public string GetAgeGroup()
        {
            if (this.Age < 13)
                return "Children";
            if (this.Age < 20)
                return "Teenagers";
            return "Adults";
        }
    }

    class Program
    {
        public static void Test1()
        {
            var names = new List<string>()
            {
                "John Doe",
                "Jane Doe",
                "Jenna Doe",
                "Joe Doe"
            };

            // Get the names which are 8 characters or less, using LINQ  
            var shortNames = from name in names where name.Length <= 8 orderby name.Length select name;

            foreach (var name in shortNames)
                Console.WriteLine(name);
        }

        public static void Test2()
        {
            var names = new List<string>()
            {
                "John Doe",
                "Jane Doe",
                "Jenna Doe",
                "Joe Doe"
            };

            // Get the names which are 8 characters or less, using LINQ
            var shortNames = names.Where(name => name.Length <= 8);
            // Order it by length
            shortNames = shortNames.OrderBy(name => name.Length);
            // Add a name to the original list
            names.Add("Zoe Doe");

            // Iterate over it - the query has not actually been executed yet!
            // It will be as soon as we hit the foreach loop though!
            foreach (var name in shortNames)
                Console.WriteLine(name);
        }

        public static void Test3()
        {
            List<int> numbers = new List<int>()
            {
                1, 2, 4, 8, 16, 32
            };
            var smallNumbers = numbers.Where(n => n < 10);
            foreach (var n in smallNumbers)
                Console.WriteLine(n);
        }

        public static void Test4()
        {
            List<int> numbers = new List<int>()
            {
                1, 2, 4, 8, 16, 32
            };
            var smallNumbers = numbers.Where(n => n > 1 && n != 4 && n < 10);
            foreach (var n in smallNumbers)
                Console.WriteLine(n);
        }

        public static void Test5()
        {
            List<int> numbers = new List<int>()
            {
                 1, 2, 4, 7, 8, 16, 29, 32, 64, 128
            };
            List<int> excludedNumbers = new List<int>()
            {
                 7, 29
            };
            var validNumbers = numbers.Where(n => !excludedNumbers.Contains(n));
            foreach (var n in validNumbers)
                Console.WriteLine(n);
        }

        public static void Test6()
        {
            List<User> listOfUsers = new List<User>()
            {
                    new User() { Name = "John Doe", Age = 42 },
                    new User() { Name = "Jane Doe", Age = 34 },
                    new User() { Name = "Joe Doe", Age = 8 },
                    new User() { Name = "Another Doe", Age = 15 },
            };

            var filteredUsers = listOfUsers.Where(user => user.Name.StartsWith("J") && user.Age < 40);
            foreach (User user in filteredUsers)
                Console.WriteLine(user.Name + ": " + user.Age);
        }

        public static void Test7()
        {
            List<int> numbers = new List<int>()
            {
                1, 2, 4, 8, 16, 32
            };
            var smallNumbers = numbers.Where(n => n > 1).Where(n => n != 4).Where(n => n < 10);
            foreach (var n in smallNumbers)
                Console.WriteLine(n);
        }

        public static void Test8()
        {
            List<int> numbers = new List<int>()
            {
                1, 7, 2, 61, 14
            };
            List<int> sortedNumbers = numbers.OrderBy(number => number).ToList();
            foreach (int number in sortedNumbers)
                Console.WriteLine(number);
        }

        public static void Test9()
        {
            List<string> cityNames = new List<string>()
            {
                "Amsterdam", "Berlin", "London", "New York"
            };
            List<string> sortedCityNames = cityNames.OrderByDescending(city => city).ToList();
            foreach (string cityName in sortedCityNames)
                Console.WriteLine(cityName);
        }

        public static void Test10()
        {
            List<User> listOfUsers = new List<User>()
            {
                new User() { Name = "John Doe", Mail = "john@doe.com", Age = 42 },
                new User() { Name = "Jane Doe", Mail = "jane@doe.com", Age = 34 },
                new User() { Name = "Joe Doe", Mail = "joe@doe.com", Age = 8 },
                new User() { Name = "Another Doe", Mail = "another@doe.com", Age = 15 },
            };

            List<User> usersByAge = listOfUsers.OrderBy(user => user.Age).ToList();
            foreach (User user in usersByAge)
                Console.WriteLine(user.Name + ": " + user.Age + " years");
        }

        public static void Test11()
        {
            List<User> listOfUsers = new List<User>()
            {
                new User() { Name = "John Doe", Mail = "john@doe.com", Age = 42 },
                new User() { Name = "Jane Doe", Mail = "jane@doe.com", Age = 42 },
                new User() { Name = "Joe Doe", Mail = "joe@doe.com", Age = 8 },
                new User() { Name = "Jenna Doe", Mail = "another@doe.com", Age = 8 },
            };

            {
                List<User> sortedUsers = listOfUsers.OrderBy(user => user.Age).ThenBy(user => user.Name).ToList();
                foreach (User user in sortedUsers)
                    Console.WriteLine(user.Name + ": " + user.Age + " years");
            }
            {
                List<User> sortedUsers = listOfUsers.OrderBy(user => user.Age).ThenByDescending(user => user.Name).ToList();
                foreach (User user in sortedUsers)
                    Console.WriteLine(user.Name + ": " + user.Age + " years");

                // Query syntax
                List<User> sortedUsersQ = (from user in listOfUsers orderby user.Age ascending, user.Name descending select user).ToList();
            }
        }

        public static void Test12()
        {
            List<string> names = new List<string>()
            {
                "John Doe",
                "Jane Doe",
                "Joe Doe",
                "Jenna Doe",
            };
            var middleNames = names.Skip(1).Take(2).ToList();
            foreach (var name in middleNames)
                Console.WriteLine(name);
        }

        public static void Test13()
        {
            CultureInfo usCulture = new CultureInfo("en-US");
            XDocument xDoc = XDocument.Load("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
            var cubeNodes = xDoc.Descendants().Where(n => n.Name.LocalName == "Cube" && n.Attribute("currency") != null).ToList();
            var currencyRateItems = cubeNodes.Select(node => new
            {
                Currency = node.Attribute("currency").Value,
                Rate = double.Parse(node.Attribute("rate").Value, usCulture)
            });

            int pageSize = 5, pageCounter = 0;
            var pageItems = currencyRateItems.Take(pageSize);
            while (pageItems.Count() > 0)
            {
                foreach (var item in pageItems)
                    Console.WriteLine(item.Currency + ": " + item.Rate.ToString("N2", usCulture));
                Console.WriteLine("Press any key to get the next items...");
                Console.ReadKey();
                pageCounter++;
                // Here's where we use the Skip() and Take() methods!
                pageItems = currencyRateItems.Skip(pageSize * pageCounter).Take(pageSize);
            }
        }

        public static void Test14()
        {
            List<User> listOfUsers = new List<User>()
            {
                new User() { Name = "John Doe", Age = 42 },
                new User() { Name = "Jane Doe", Age = 34 },
                new User() { Name = "Joe Doe", Age = 8 },
                new User() { Name = "Another Doe", Age = 15 },
            };

            List<string> names = listOfUsers.Select(user => user.Name).ToList();

            foreach (string name in names)
                Console.WriteLine(name);
        }

        public static void Test15()
        {
            List<User> listOfUsers = new List<User>()
            {
                new User() { Name = "John Doe", Age = 42 },
                new User() { Name = "Jane Doe", Age = 34 },
                new User() { Name = "Joe Doe", Age = 8 },
                new User() { Name = "Another Doe", Age = 15 },
            };

            List<string> names = listOfUsers.Select(user => user.Name).ToList();

            List<User> users = names.Select(name => new User { Name = name }).ToList();

            foreach (User user in users)
                Console.WriteLine(user.Name);
        }

        public static void Test16()
        {
            List<User> listOfUsers = new List<User>()
            {
                new User() { Name = "John Doe", Mail = "john@doe.com", Age = 42 },
                new User() { Name = "Jane Doe", Mail = "jane@doe.com", Age = 34 },
                new User() { Name = "Joe Doe", Mail = "joe@doe.com", Age = 8 },
                new User() { Name = "Another Doe", Mail = "another@doe.com", Age = 15 },
            };

            var simpleUsers = listOfUsers.Select(user => new
            {
                Name = user.Name,
                Age = user.Age
            });
            foreach (var user in simpleUsers)
                Console.WriteLine(user.Name);

            // Query syntax
            var simpleUsersQ = (from user in listOfUsers
                                select new
                                {
                                    Name = user.Name,
                                    Age = user.Age
                                }).ToList();

            foreach (var user in simpleUsersQ)
                Console.WriteLine(user.Name);
        }

        public static void Test17()
        {
            var users = new List<User>()
            {
                new User { Name = "John Doe", Age = 42, HomeCountry = "USA" },
                new User { Name = "Jane Doe", Age = 38, HomeCountry = "USA" },
                new User { Name = "Joe Doe", Age = 19, HomeCountry = "Germany" },
                new User { Name = "Jenna Doe", Age = 19, HomeCountry = "Germany" },
                new User { Name = "James Doe", Age = 8, HomeCountry = "USA" },
            };
            var usersGroupedByCountry = users.GroupBy(user => user.HomeCountry);
            foreach (var group in usersGroupedByCountry)
            {
                Console.WriteLine("Users from " + group.Key + ":");
                foreach (var user in group)
                    Console.WriteLine("* " + user.Name);
            }
        }

        public static void Test18()
        {
            var users = new List<User>()
            {
                new User { Name = "John Doe", Age = 42, HomeCountry = "USA" },
                new User { Name = "Jane Doe", Age = 38, HomeCountry = "USA" },
                new User { Name = "Joe Doe", Age = 19, HomeCountry = "Germany" },
                new User { Name = "Jenna Doe", Age = 19, HomeCountry = "Germany" },
                new User { Name = "James Doe", Age = 8, HomeCountry = "USA" },
            };
            var usersGroupedByCountry = users.GroupBy(user => user.HomeCountry);
            foreach (var group in usersGroupedByCountry)
            {
                Console.WriteLine("Users from " + group.Key + ":");
                foreach (var user in group)
                    Console.WriteLine("* " + user.Name);
            }
        }

        public static void Test19()
        {
            var users = new List<User>()
            {
                new User { Name = "John Doe", Age = 42, HomeCountry = "USA" },
                new User { Name = "Jane Doe", Age = 38, HomeCountry = "USA" },
                new User { Name = "Joe Doe", Age = 19, HomeCountry = "Germany" },
                new User { Name = "Jenna Doe", Age = 19, HomeCountry = "Germany" },
                new User { Name = "James Doe", Age = 8, HomeCountry = "USA" },
            };
            var usersGroupedByFirstLetters = users.GroupBy(user => user.Name.Substring(0, 2));
            foreach (var group in usersGroupedByFirstLetters)
            {
                Console.WriteLine("Users starting with " + group.Key + ":");
                foreach (var user in group)
                    Console.WriteLine("* " + user.Name);
            }
        }

        public static void Test20()
        {
            var users = new List<User>()
        {
            new User { Name = "John Doe", Age = 42, HomeCountry = "USA" },
            new User { Name = "Jane Doe", Age = 38, HomeCountry = "USA" },
            new User { Name = "Joe Doe", Age = 19, HomeCountry = "Germany" },
            new User { Name = "Jenna Doe", Age = 19, HomeCountry = "Germany" },
            new User { Name = "James Doe", Age = 8, HomeCountry = "USA" },
        };
            var usersGroupedByAgeGroup = users.GroupBy(user => user.GetAgeGroup());
            foreach (var group in usersGroupedByAgeGroup)
            {
                Console.WriteLine(group.Key + ":");
                foreach (var user in group)
                    Console.WriteLine("* " + user.Name + " [" + user.Age + " years]");
            }

            var usersGroupedByAgeGroupLambda = users.GroupBy(user =>
            {
                if (user.Age < 13)
                    return "Children";
                if (user.Age < 20)
                    return "Teenagers";
                return "Adults";
            });
        }

        public static void Test21()
        {
            var users = new List<User>()
            {
                new User { Name = "John Doe", Age = 42, HomeCountry = "USA" },
                new User { Name = "Jane Doe", Age = 38, HomeCountry = "USA" },
                new User { Name = "Joe Doe", Age = 19, HomeCountry = "Germany" },
                new User { Name = "Jenna Doe", Age = 19, HomeCountry = "Germany" },
                new User { Name = "James Doe", Age = 8, HomeCountry = "USA" },
            };

            var usersGroupedByCountryAndAge = users.GroupBy(user => new { user.HomeCountry, user.Age });
            foreach (var group in usersGroupedByCountryAndAge)
            {
                Console.WriteLine("Users from " + group.Key.HomeCountry + " at the age of " + group.Key.Age + ":");
                foreach (var user in group)
                    Console.WriteLine("* " + user.Name + " [" + user.Age + " years]");
            }

            var usersGroupedByCountryAndAgeQ = from user 
                                               in users 
                                               group user by new { user.HomeCountry, user.Age } 
                                               into userGroup select userGroup;
        }

        static void Main(string[] args)
        {
            Test1();
            Test2();
            Test3();
            Test4();
            Test5();
            Test6();
            Test7();
            Test8();
            Test9();
            Test10();
            Test11();
            Test12();
            Test13();
            Test14();
            Test15();
            Test16();
            Test17();
            Test18();
            Test19();
            Test20();
            Test21();
        }
    }
}
