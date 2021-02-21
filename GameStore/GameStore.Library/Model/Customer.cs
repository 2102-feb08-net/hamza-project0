using System;

namespace GameStore.Library.Model
{
    public class Customer
    {
        private string _userId;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId
        {
            get => _userId;
            set
            {
                //TODO : need to implement input checking
                _userId = value;
            }
        }

        public Customer(string userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }

    }
}
