using System;
using System.Linq;

namespace GameStore.Library.Model
{
    public class Customer
    {
        private string _userId;
        private string _firstName;
        private string _lastName;

        public string FirstName 
        { 
            get => _firstName;
            set 
            {
                if (!value.All(Char.IsLetter))
                {
                    throw new ArgumentException("||Can only input English letters for names||");
                }
                else if (value.Length < 2 || value.Length > 20)
                {
                    throw new ArgumentException("||Input size must be 2-20 letters for names||");
                }
                else
                {
                    _firstName = value;
                }
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (!value.All(Char.IsLetter))
                {
                    throw new ArgumentException("||Can only input English letters for names||");
                }
                else if (value.Length < 2 || value.Length > 20)
                {
                    throw new ArgumentException("||Input size must be 2-20 letters for names||");
                }
                else
                {
                    _lastName = value;
                }
            }
        }

        public string UserId
        {
            get => _userId;
            set
            {
                if (value.Length < 5 || value.Length > 15)
                {
                    throw new ArgumentException("||Input size must be 5-15 for User Ids||");
                }
                else
                {
                    _userId = value;
                }
            }
        }

        public Customer() { }

        public Customer(string userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }

        public string GetFullName() => FirstName + " " + LastName;

    }
}
