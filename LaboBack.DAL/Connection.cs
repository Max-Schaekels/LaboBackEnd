using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboBack.DAL
{
    public class Connection
    {
        private readonly string _connectionString;

        // Constructeur : on donne la chaîne de connexion à utiliser
        public Connection(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Méthode pour exécuter une commande qui ne retourne pas de résultat (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(Command command)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand sqlCommand = CreateSqlCommand(command, connection))
            {
                connection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }

        // Méthode pour exécuter une commande qui retourne une seule valeur (ex: SELECT COUNT(*))
        public object ExecuteScalar(Command command)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand sqlCommand = CreateSqlCommand(command, connection))
            {
                connection.Open();
                return sqlCommand.ExecuteScalar();
            }
        }

        // Méthode pour exécuter une commande qui retourne plusieurs lignes (SELECT)
        public IEnumerable<T> ExecuteReader<T>(Command command, Func<SqlDataReader, T> selector)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand sqlCommand = CreateSqlCommand(command, connection))
            {
                connection.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return selector(reader); // Transformation d'une ligne vers T
                    }
                }
            }
        }

        // Méthode privée pour construire un SqlCommand depuis notre Command maison
        private SqlCommand CreateSqlCommand(Command command, SqlConnection connection)
        {
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = command.Query;
            sqlCommand.CommandType = command.CommandType;

            foreach (var parameter in command.Parameters)
            {
                sqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
            }

            return sqlCommand;
        }
    }
}
