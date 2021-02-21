﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Library.Model
{
    public class Product
    {
        public string Name { get; }

        public Product(string name)
        {
            Name = name;
        }

    }
}