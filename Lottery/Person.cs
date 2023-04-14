using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    public class Person : INotifyPropertyChanged
    {
        string firstName;
        string lastName;
        public string? Time { get; set; }
        
        public string? FirstName 
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string? LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class PersonComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person? x, Person? y)
        {
            return x.FirstName.Equals(y.FirstName) && x.LastName.Equals(y.LastName);
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
            return obj.FirstName.GetHashCode() + obj.LastName.GetHashCode();
        }
    }

    public class CurrentPerson : INotifyPropertyChanged
    {
        Person person;
        public Person Person
        {
            get => person;
            set
            {
                person = value;
                OnPropertyChanged(nameof(Person));
            }
        }

        public string FirstName
        {
            get => person.FirstName;
            set
            {
                person.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => person.LastName;
            set
            {
                person.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
