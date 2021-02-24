using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Library.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }


        //public Product(string name)
        //{
        //    Name = name;
        //}

        public override string ToString()
        {
            return Name;
        }

    }
}
