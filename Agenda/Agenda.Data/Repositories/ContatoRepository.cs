using Agenda.Data.Configurations;
using Agenda.Data.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Data.Repositories
{
    /// <summary>
    /// Classe de repositório de dados para Contato
    /// </summary>
    public class ContatoRepository
    {
        /// <summary>
        /// Método para inserir um usuário no banco de dados
        /// </summary>
        /// <param name="contato"></param>
        public void Create(Contato contato)
        {
            var query = @"INSERT INTO CONTATO VALUES (@IdContato,@Nome,@Telefone,@Email,@DataNascimento,@Tipo,@IdUsuario)";

            using(var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                connection.Execute(query, contato);
            }

        }
        /// <summary>
        /// Método para atualizar um contato
        /// </summary>
        /// <param name="contato"></param>
        public void Update(Contato contato)
        {
            var query = @"UPDATE Contato
                            SET 
                            Nome = @Nome,
                            Telefone = @Telefone,
                            Email = @Email,
                            DataNascimento = @DataNascimento,
                            Tipo = @Tipo
                        WHERE IdContato = @IdContato
                        ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                connection.Execute(query, contato);
            }
        }
        /// <summary>
        /// Método para deletar um contato
        /// </summary>
        /// <param name="contato"></param>
        public void Delete(Contato contato)
        {
            var query = @"DELETE FROM Contato WHERE IdContato = @IdContato";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                connection.Execute(query, contato);
            }
        }
        /// <summary>
        /// Retorna uma lista de contatos de um usuário
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<Contato> GetAllByUsuario(Guid idUsuario)
        {
            var query = @"SELECT * FROM Contato WHERE IdUsuario = @idUsuario ORDER BY Nome";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {           
               return connection.Query<Contato>(query, new { idUsuario }).ToList();
            }
        }
        /// <summary>
        /// Retorna um contato
        /// </summary>
        /// <param name="idContato"></param>
        /// <returns></returns>
        public Contato? GetById(Guid idContato)
        {
            var query = @"SELECT * FROM Contato WHERE IdContato = @idContato";
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                return connection.Query<Contato>(query, new { idContato }).FirstOrDefault();
            }
        }
    }
}

