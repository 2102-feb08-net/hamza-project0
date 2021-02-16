using System;

namespace GameStore.Library
{
    public class Customer
    {
        private string _userid;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId
        {
            get => _userid;
            set
            {
                //TODO : need to implement input checking
                _userid = value;
            }
        }


    }
}
