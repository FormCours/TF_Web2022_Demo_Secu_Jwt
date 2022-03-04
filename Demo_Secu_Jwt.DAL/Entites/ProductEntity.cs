﻿using Demo_Secu_Jwt.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.DAL.Entites
{
   public class ProductEntity : IEntity<int>
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public decimal Price { get; set; }
      public int Quantity { get; set; }
      public string Category { get; set; }
   }
}
