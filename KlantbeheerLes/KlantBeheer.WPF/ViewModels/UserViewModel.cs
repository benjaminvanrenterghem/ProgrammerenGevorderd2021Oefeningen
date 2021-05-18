using Klantbeheer.Domain;
using System;
using System.ComponentModel;

namespace KlantBeheer.WPF.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private User _user;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get => _user.Id;
            set
            {
                // Waarom if? Indien er geen wijziging nodig is, wordt user interface niet updated -> sneller!!
                if (_user.Id != value)
                {
                    _user.Id = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }   

    public string Name
        {
            get => _user.Name;
            set
            {
                if (_user.Name != value)
                {
                    _user.Name = value; 
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public DateTime Birthday
        {
            get
            {
                return _user.Birthday;
            }

            set
            {
                if (_user.Birthday != value)
                {
                    _user.Birthday = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Birthday)));
                }
            }
        }

        #region Ctor
        public UserViewModel(User user)
        {
            _user = user;
        }
        #endregion
    }
}
