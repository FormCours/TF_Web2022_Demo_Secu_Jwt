using Demo_Secu_Jwt.DAL.Entites;
using Demo_Secu_Jwt.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.DAL.Services
{
   public class ProductService : IRepository<int, ProductEntity>
   {
      IDbConnection _DbConnection;

      public ProductService(IDbConnection dbConnection)
      {
         _DbConnection = dbConnection;
      }

      public int Create(ProductEntity entity)
      {
         throw new NotImplementedException();
      }

      public bool Delete(int id)
      {
         throw new NotImplementedException();
      }

      public ProductEntity Get(int id)
      {
         // Génération d'un commande SQL
         IDbCommand command = _DbConnection.CreateCommand();
         command.CommandType = CommandType.Text;
         command.CommandText = "SELECT * FROM VProduct WHERE Product_Id = @id";

         // Ajout d'un parametre (Evite l'injection SQL)
         IDbDataParameter paramId = command.CreateParameter();
         paramId.ParameterName = "@id";
         paramId.Value = id;
         command.Parameters.Add(paramId);

         IDataReader reader = command.ExecuteReader();

         if (reader.Read())
         {
            return new ProductEntity()
            {
               Id = (int)reader["Product_id"],
               Name = reader["Name"].ToString(),
               Description= reader["Description"] is DBNull ? null : reader["Description"].ToString(),
               Price = (decimal) reader["Price"],
               Category = reader["Category"].ToString(),
               Quantity = (int) reader["Quantity"]
            };
         }
         return null;
      }

      public IEnumerable<ProductEntity> GetAll()
      {
         throw new NotImplementedException();
      }

      public bool Update(int id, ProductEntity entity)
      {
         throw new NotImplementedException();
      }
   }
}
