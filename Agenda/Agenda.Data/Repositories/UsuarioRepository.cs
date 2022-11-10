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
    /// Classe de repositório de dados para Usuario
    /// </summary>
    public class UsuarioRepository
    {
        /// <summary>
        /// Método para inserir um usuário no banco de dados
        /// </summary>
        /// <param name="usuario"></param>
        public void Create(Usuario usuario)
        {
            var query = @"
                    INSERT INTO Usuario
                    VALUES
                    (@IdUsuario,@Nome,@Email,CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha),2), @DataCriacao)
                ";

            using(var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                connection.Execute(query, usuario);
            }
        }
        /// <summary>
        /// Método para consultar um usuário baseado no email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Usuario? GetByEmail(string email)
        {
            var query = @"
                SELECT * FROM Usuario
                WHERE Email = @email
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
               return connection.Query<Usuario>(query, new { email}).FirstOrDefault();
            }
        }
        /// <summary>
        /// Método para consultar um usuário baseado no email e senha
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            var query = @"
                SELECT * FROM Usuario
                WHERE Email = @email and Senha = CONVERT(VARCHAR(32), HASHBYTES('MD5', @senha),2)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                return connection.Query<Usuario>(query, new { email, senha }).FirstOrDefault();
            }
        }
        /// <summary>
        /// Metodo para atualizar a senha do usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="novaSenha"></param>
        public void UpdateSenha(Guid idUsuario, string novaSenha)
        {
            var query = @"
                    UPDATE Usuario
                    SET
                    Senha = CONVERT(VARCHAR(32), HASHBYTES('MD5', @novaSenha),2)
                    where IdUsuario = @IdUsuario
                ";

            using(var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString))
            {
                connection.Execute(query, new {idUsuario, novaSenha});
            }

        }

    }
}
