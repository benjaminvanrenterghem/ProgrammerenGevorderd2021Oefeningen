using Xunit;

namespace MVVM.Tests
{
    class Pet : ViewModelBase
    {
        private string name;
        public string Name { get => name; set => Set(ref name, value); }
    }

    public class ViewModelBaseTest
    {
        [Fact]
        public void TestAll()
        {
            var called = false;
            var calledPropertyName = "";
            var puppy = new Pet();
            puppy.PropertyChanged += (sender, e) =>
            {
                called = true;
                calledPropertyName = e.PropertyName;
            };
            Assert.False(called);
            var newName = "Bassie";
            puppy.Name = newName;
            Assert.True(called);
            Assert.Equal(nameof(puppy.Name), calledPropertyName);
            Assert.Equal(newName, puppy.Name);
        }
    }

    class Contact : ComputedViewModelBase
    {
        private string name;
        public string Name { get => name; set => Set(ref name, value); }

        private string surname;
        public string Surname { get => surname; set => Set(ref surname, value); }

        [PropertySource(nameof(Name), nameof(Surname))]
        public string Fullname => $"{Name} {Surname}".Trim();
    }

    public class ComputedViewModelBaseTest
    {
        [Fact]
        public void TestAll()
        {
            var computedCalledTimes = 0;
            var lastFullname = "";
            var myContact = new Contact();
            myContact.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(myContact.Fullname))
                {
                    computedCalledTimes += 1;
                    lastFullname = myContact.Fullname;
                }
            };
            Assert.Equal(0, computedCalledTimes);
            Assert.Equal("", lastFullname);
            myContact.Name = "Luc";
            Assert.Equal(1, computedCalledTimes);
            Assert.Equal("Luc", lastFullname);
            myContact.Surname = "Vervoort";
            Assert.Equal(2, computedCalledTimes);
            Assert.Equal("Luc Vervoort", lastFullname);
            Assert.Equal("Luc Vervoort", myContact.Fullname);
        }
    }
}