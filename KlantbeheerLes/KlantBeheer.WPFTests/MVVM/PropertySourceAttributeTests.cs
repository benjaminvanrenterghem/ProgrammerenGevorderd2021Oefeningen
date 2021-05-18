namespace KlantBeheer.WPF.MVVM.Tests
{
    class Pet : KlantBeheer.WPF.MVVM.ViewModelBase
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
}