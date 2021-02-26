using System;

namespace FuncExampleApp
{
    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static int Sum(int x, int y)
        {
            return x + y;
        }

        static void ConsolePrint(int i)
        {
            Console.WriteLine(i);
        }

        static void Main(string[] args)
        {
            {
                Func<int, int, int> add = Sum;
                var result = add(10, 10);
                Console.WriteLine("Func add result: " + result);
            }

            // Func met een anonieme methode:
            {
                Func<int> getRandomNumber = delegate () { var rnd = new Random(); return rnd.Next(1, 100); };
                var result = getRandomNumber();
                Console.WriteLine("Func delegate result: " + result);
            }

            // Func met een lambda expressie:
            {
                Func<int> getRandomNumber = () => new Random().Next(1, 100);
                var result = getRandomNumber();
                Console.WriteLine("Func lambda result: " + result);
                Func<int, int, int> sum = (x, y) => x + y;
                result = sum(5, 6);
                Console.WriteLine("Func lambda sum result: " + result);
            }

            {
                Func<Student, bool> isStudentTeenAger = s => s.Age > 12 && s.Age < 20;
                var s = new Student() { Age = 21 };
                var isTeenAger = isStudentTeenAger(s);
                Console.WriteLine("Student is teenager: " + (isTeenAger ? "yes" : "no"));
            }

            // Action<>: Func<> zonder return type
            {
                Action<int> printActionDel = ConsolePrint; // Action<int> printActionDel = new Action<int>(ConsolePrint);
                printActionDel(10);
            }

            // Action met een anonieme methode:
            {
                Action<int> printActionDel = delegate (int i) { Console.WriteLine(i); };
                printActionDel(10);
            }

            // Action met een lambda expressie:
            {
                Action<int> printActionDel = i => Console.WriteLine(i);
                printActionDel(10);
            }

            {
                Action<Student> printStudentDetail = s => Console.WriteLine("Name: {0}, Age: {1} ", s.Name, s.Age);
                var s = new Student() { Name = "Bill", Age = 21 };
                printStudentDetail(s);//output: Name: Bill, Age: 21
            }
        }
    }
}
