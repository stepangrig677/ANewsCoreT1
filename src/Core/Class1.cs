﻿using Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class Class1
    {
        public Class1()
        {
            using (var db = new MyDbContext())
            {
                //db.Add
            }
        }
    }
}
