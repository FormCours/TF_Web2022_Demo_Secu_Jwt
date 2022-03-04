using Demo_Secu_Jwt.DAL.Entites;
using Demo_Secu_Jwt.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.DAL.Services
{
   public class UserService : IRepository<int, UserEntity>
   {
      IDbConnection _DbConnection;

      public UserService(IDbConnection dbConnection)
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

      public int Create(UserEntity entity)
      {
         // Ouverture de la connexion
         _DbConnection.Open();

         // La commande INSERT
         IDbCommand command = _DbConnection.CreateCommand();
         command.CommandType = CommandType.Text;
         command.CommandText = "INSERT INTO UserClient ([Pseudo], [Email], [Password_Hash])" +
                               " OUTPUT inserted.UserClient_Id " +
                               " VALUES (@Pseudo, @Email, @Pwd); ";

         // Ajout des parametres
         AddParameter(command, "@Pseudo", entity.Pseudo);
         AddParameter(command, "@Email", entity.Email);
         AddParameter(command, "@Pwd", entity.PasswordHash);

         // Execution de la requete
         int userId = (int)command.ExecuteScalar();

         // Fermeture de la connexion
         _DbConnection.Close();

         // Envoi du resultat
         return userId;
      }

      public string GetPasswordHash(string email)
      {
         // Ouverture de la connexion
         _DbConnection.Open();

         // La commande INSERT
         IDbCommand command = _DbConnection.CreateCommand();
         command.CommandType = CommandType.Text;
         command.CommandText = "SELECT [Password_Hash] FROM UserClient WHERE [Email] LIKE @Email";

         // Ajout des parametres
         AddParameter(command, "@Email", email);

         // Execution de la requete
         string pwdHash = command.ExecuteScalar().ToString();

         // Fermeture de la connexion
         _DbConnection.Close();

         // Envoi du resultat
         return pwdHash;
      }

      public UserEntity GetByEmail(string email)
      {
         // Ouverture de la connexion
         _DbConnection.Open();

         // La commande INSERT
         IDbCommand command = _DbConnection.CreateCommand();
         command.CommandType = CommandType.Text;
         command.CommandText = "SELECT * FROM UserClient WHERE [Email] LIKE @Email";

         // Ajout des parametres
         AddParameter(command, "@Email", email);

         // Execution de la requete
         IDataReader reader = command.ExecuteReader();
         UserEntity userEntity = null;

         if(reader.Read())
         {
            userEntity = new UserEntity()
            {
               Id = (int)reader["UserClient_Id"],
               Pseudo = reader["Pseudo"].ToString(),
               Email = reader["Email"].ToString()
            };
         }

         // Fermeture de la connexion
         _DbConnection.Close();

         // Envoi de la réponse
         return userEntity;
      }

      public bool Delete(int id)
      {
         throw new NotImplementedException();
      }

      public UserEntity Get(int id)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<UserEntity> GetAll()
      {
         throw new NotImplementedException();
      }

      public bool Update(int id, UserEntity entity)
      {
         throw new NotImplementedException();
      }
   }
}
