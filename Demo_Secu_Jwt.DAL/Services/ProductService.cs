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

      private void AddParameter(IDbCommand command, string paramName, object paramValue)
      {
         IDbDataParameter param = command.CreateParameter();
         param.ParameterName = paramName;
         param.Value = paramValue;
         command.Parameters.Add(param);
      }

      private int ManageCategoryId(string category)
      {
         // - Récuperation de l'id de la category
         IDbCommand commandGetCategory = _DbConnection.CreateCommand();
         commandGetCategory.CommandType = CommandType.Text;
         commandGetCategory.CommandText = "SELECT Category_Id "
                                        + "FROM Category "
                                        + "WHERE Name LIKE @Name";
         IDbDataParameter categoryName = commandGetCategory.CreateParameter();
         categoryName.ParameterName = "@Name";
         categoryName.Value = category;
         commandGetCategory.Parameters.Add(categoryName);

         object resultCategoryId = commandGetCategory.ExecuteScalar();
         int? categoryId = resultCategoryId is DBNull ? null : (int)resultCategoryId;

         // - Si la category est null => On l'ajoute et on recup l'id
         if (categoryId is null)
         {
            IDbCommand commandInsertCategory = _DbConnection.CreateCommand();
            commandInsertCategory.CommandType = CommandType.Text;
            commandInsertCategory.CommandText = "INSERT INTO Category(Name) " +
                                                " OUTPUT inserted.Category_Id " +
                                                " VALUES (@Name)";
            IDbDataParameter newCategoryName = commandInsertCategory.CreateParameter();
            newCategoryName.ParameterName = "@Name";
            newCategoryName.Value = category;
            commandInsertCategory.Parameters.Add(newCategoryName);

            categoryId = (int)commandInsertCategory.ExecuteScalar();
         }

         return (int)categoryId;
      }

      public int Create(ProductEntity entity)
      {
         // Ouverture de la connexion
         _DbConnection.Open();

         // Gestion de la category
         int categoryId = ManageCategoryId(entity.Category);

         // Création d'une commande "Product"
         IDbCommand command = _DbConnection.CreateCommand();
         command.CommandType = CommandType.Text;
         command.CommandText = "INSERT INTO Product ([Name], [Description], [Price], [Quantity], [Category_Id]) OUTPUT inserted.Product_Id VALUES (@Name, @Desc, @Price, @Qte, @Cat_Id)";

         // Affecter les parametres
         AddParameter(command, "@Name", entity.Name);
         AddParameter(command, "@Desc", entity.Description);
         AddParameter(command, "@Price", entity.Price);
         AddParameter(command, "@Qte", entity.Quantity);
         AddParameter(command, "@Cat_Id", categoryId);

         // Execution de la requete
         int productId = (int) command.ExecuteScalar();

         // Fermeture de la connexion
         _DbConnection.Close();

         // Envoi de l'id
         return productId;
      }

      public bool Delete(int id)
      {
         // Ouverture de la connexion
         _DbConnection.Open();

         // Création d'une commande "Product"
         IDbCommand command = _DbConnection.CreateCommand();
         command.CommandType = CommandType.Text;
         command.CommandText = "DELETE FROM Product "
                             + " WHERE [Product_Id] = @Product_Id";

         // Affecter les parametres
         AddParameter(command, "@Product_Id", id);

         // Execution de la requete
         int nbRow = command.ExecuteNonQuery();

         // Fermeture de la connexion
         _DbConnection.Close();

         // Envoi de la réponse
         return nbRow == 1;
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

         _DbConnection.Open();
         IDataReader reader = command.ExecuteReader();
         ProductEntity product = null;

         if (reader.Read())
         {
            product = new ProductEntity()
            {
               Id = (int)reader["Product_id"],
               Name = reader["Name"].ToString(),
               Description = reader["Description"] is DBNull ? null : reader["Description"].ToString(),
               Price = (decimal)reader["Price"],
               Category = reader["Category"].ToString(),
               Quantity = (int)reader["Quantity"]
            };
         }

         _DbConnection.Close();
         return product;
      }

      public IEnumerable<ProductEntity> GetAll()
      {
         // Génération d'un commande SQL
         IDbCommand command = _DbConnection.CreateCommand();
         command.CommandType = CommandType.Text;
         command.CommandText = "SELECT * FROM VProduct";

         _DbConnection.Open();
         IDataReader reader = command.ExecuteReader();

         while (reader.Read())
         {
            // Renvoi une enumeration différé
            yield return new ProductEntity()
            {
               Id = (int)reader["Product_id"],
               Name = reader["Name"].ToString(),
               Description = reader["Description"] is DBNull ? null : reader["Description"].ToString(),
               Price = (decimal)reader["Price"],
               Category = reader["Category"].ToString(),
               Quantity = (int)reader["Quantity"]
            };
         }
         _DbConnection.Close();
      }

      public bool Update(int id, ProductEntity entity)
      {
         // Ouverture de la connexion
         _DbConnection.Open();

         // Gestion de la category
         int categoryId = ManageCategoryId(entity.Category);

         // Création d'une commande "Product"
         IDbCommand command = _DbConnection.CreateCommand();
         command.CommandType = CommandType.Text;
         command.CommandText = "UPDATE Product "
                             + " SET [Name] = @Name, "
                             + "     [Description] = @Desc, "
                             + "     [Price] = @Price, "
                             + "     [Quantity] = @Qte, "
                             + "     [Category_Id] = @Cat_Id "
                             + " WHERE [Product_Id] = @Product_Id";

         // Affecter les parametres
         AddParameter(command, "@Product_Id", id);
         AddParameter(command, "@Name", entity.Name);
         AddParameter(command, "@Desc", entity.Description);
         AddParameter(command, "@Price", entity.Price);
         AddParameter(command, "@Qte", entity.Quantity);
         AddParameter(command, "@Cat_Id", categoryId);

         // Execution de la requete
         int nbRow = command.ExecuteNonQuery();

         // Fermeture de la connexion
         _DbConnection.Close();

         // Envoi de la réponse
         return nbRow == 1;
      }
   }
}
