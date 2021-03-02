using System;
using ExtensionMethods;

namespace ExtensionMethods
{
    public static class IntExtensions
    {
        public static bool IsGreaterThan(this int i, int value) // static is vereist en this is cruciaal: zegt bij welk type/class deze method hoort
        {
            return i > value;
        }
    }
}

namespace LambdaExamples
{
    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        delegate bool IsTeenager(Student s); // delegate type met naam
        delegate void DoIt();
        delegate bool AgeTest(Student s, int age);

        static bool TestTeenager(Student s)
        {
            return s.Age > 12 && s.Age < 20;
        }

        static void Main(string[] args)
        {
            // Anonymous methods, delegates, lambda's:
            {
                IsTeenager myClassicTeenager = TestTeenager;

                IsTeenager myDelegate = delegate (Student s) { return s.Age > 12 && s.Age < 20; }; // anonieme methode
                IsTeenager myLambda = s => s.Age > 12 && s.Age < 20; // lambda is een verkorte delegate

                Student s1 = new() { Name = "Jef", Age = 15 };
                var result1 = myClassicTeenager(s1);
                var result2 = myDelegate(s1);
                var result3 = myLambda(s1);

                DoIt doIt = delegate () { System.Diagnostics.Debug.WriteLine("Parameterless lambda expression"); };
                DoIt doIt2 = () => System.Diagnostics.Debug.WriteLine("Parameterless lambda expression");

                doIt();
                doIt2?.Invoke();

                AgeTest myYoungAgeTest1 = (s, youngAge) => { System.Diagnostics.Debug.WriteLine("testing"); return s.Age <= youngAge; };
                var result4 = myYoungAgeTest1(s1, 10);
                AgeTest myYoungAgeTest2 = (s, youngAge) => s.Age <= youngAge;
                var result5 = myYoungAgeTest2(s1, 10);

                // Toekennen aan een Func<> is ook mogelijk:
                Func<Student, bool> isStudentTeenAger = s => s.Age > 12 && s.Age < 20;
                var isTeenAger = isStudentTeenAger(s1);
                if (isTeenAger)
                {
                    System.Diagnostics.Debug.WriteLine("Student is teenager");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Student is not a teenager");
                }
            }

            // Extension method example:
            {
                int i = 10;

                bool result = i.IsGreaterThan(100);
            }
        }
    }
}
