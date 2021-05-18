using Klantbeheer.Domain;
using KlantBeheer.WPF.MVVM;
using MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KlantBeheer.WPF.ViewModels
{
    public class StudentsViewModel : ViewModelBase
    {
        public ObservableCollection<Student> StudentList { get; set; } = new ObservableCollection<Student>
        {
                new Student { FirstName = "Bruce" },
                new Student { FirstName = "Harry" },
                new Student { FirstName = "Stuart" },
                new Student { FirstName = "Robert" }
        };

        public string SelectedStudent { get; set; }

        private string _selectedName;
        public string SelectedName
        {
            get => _selectedName;
            set
            {
                if (_selectedName == value)
                {
                    return;
                }
                _selectedName = value;
                // Het doorgeven van het veld is niet meer nodig:
                RaisePropertyChanged(/*"SelectedName"*//*nameof(SelectedName)*/);
            }
        }

        private ICommand _updateStudentNameCommand;
        public ICommand UpdateStudentNameCommand
        {
            get => _updateStudentNameCommand;
            set => _updateStudentNameCommand = value;
        }

        // Om te tonen hoe je Mode=TwoWay gebruikt: wordt aangepast wanneer een student geselecteerd wordt
        private Student _selectedStudentItem;
        public Student SelectedStudentItem
        {
            get => _selectedStudentItem;
            set { if (_selectedStudentItem != value) { _selectedStudentItem = value; } }
        }

        public StudentsViewModel()
        {
            UpdateStudentNameCommand = new RelayCommand(o => SelectedStudentDetails(o), o => CanStudentBeShown());
            StudentList = new ObservableCollection<Student>
            {
                new Student { FirstName = "Bruce" },
                new Student { FirstName = "Harry" },
                new Student { FirstName = "Stuart" },
                new Student { FirstName = "Robert" }
            };
        }

        // Wanneer het command uitgevoerd wordt:
        public void SelectedStudentDetails(object parameter)
        {
            if (parameter != null)
                SelectedName = (parameter as Student)?.FirstName;
        }

        // criterium dat zichtbaarheid van knop bepaalt:
        public bool CanStudentBeShown()
        {
            return _selectedStudentItem != null;
        }
    }
}