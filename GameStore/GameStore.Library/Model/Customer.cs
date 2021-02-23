using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameStore.Library.Model
{
    public class Customer
    {
        public int Id { get; set; }
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _city;
        private string _state;

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

        public string UserName
        {
            get => _userName;
            set
            {
                if (!value.All(Char.IsLetterOrDigit))
                {
                    throw new ArgumentException("||Can only input English letters and numbers for usernames||");
                }
                else if (value.Length < 5 || value.Length > 15)
                {
                    throw new ArgumentException("||Input size must be 5-15 for usernames||");
                }
                else
                {
                    _userName = value;
                }
            }
        }

        public string City
        {
            get => _city;
            set
            {
                string pattern = "^[A-Za-z ]+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException("||Can only input English letters for cities||");
                }
                else if (value.Length < 2 || value.Length > 20)
                {
                    throw new ArgumentException("||Input size must be 2-20 letters for cities||");
                }
                else
                {
                    _city = value;
                }
            }
        }

        public string State
        {
            get => _state;
            set
            {
                string pattern = "^[A-Za-z ]+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException("||Can only input English letters for states||");
                }
                else if (value.Length < 2 || value.Length > 20)
                {
                    throw new ArgumentException("||Input size must be 2-20 letters for states||");
                }
                else
                {
                    _state = value;
                }
            }
        }

        //public Customer() { }

        //public Customer(string userId, string firstName, string lastName)
        //{
        //    UserId = userId;
        //    FirstName = firstName;
        //    LastName = lastName;
        //}

        public string GetFullName() => FirstName + " " + LastName;

    }
}
