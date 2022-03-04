using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.DAL.Interfaces
{
   public interface IEntity<TKey>
   {
      TKey Id { get; set; }
   }
}
