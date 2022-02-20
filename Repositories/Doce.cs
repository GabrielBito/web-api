using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Repositories
{
    public static class Doce
    {
        const string chaveCache = "doces";

        public static void save(Models.Doce doce)
        {
            using (SqlConnection conexao = new SqlConnection(Configuration.Parameters.getConnectionString()))
            {
                conexao.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conexao;
                    cmd.CommandText = "insert into doce (descricao, valor, datafab) values (@descricao, @valor, @datafab);";
                    cmd.Parameters.Add(new SqlParameter("@descricao", System.Data.SqlDbType.VarChar)).Value = doce.Descricao;
                    cmd.Parameters.Add(new SqlParameter("@valor", System.Data.SqlDbType.Decimal)).Value = doce.Valor;
                    if (doce.DataFab != null)
                        cmd.Parameters.Add(new SqlParameter("@datafab", System.Data.SqlDbType.Date)).Value = doce.DataFab;
                    else
                        cmd.Parameters.Add(new SqlParameter("@datafab", System.Data.SqlDbType.Date)).Value = DBNull.Value;
                    cmd.ExecuteNonQuery();
                }
            }
            Cache.remove(chaveCache);
        }

        public static List<Models.Doce> getAll()
        {
            List<Models.Doce> doces = (List<Models.Doce>) Cache.get(chaveCache);

            if (doces == null)           
            {
                doces = new List<Models.Doce>();

                using (SqlConnection conexao = new SqlConnection(Configuration.Parameters.getConnectionString()))
                {
                    conexao.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conexao;
                        cmd.CommandText = "select id, descricao, valor, datafab from doce;";
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Models.Doce doce = new Models.Doce();
                            doce.Id = (int)dr["id"];
                            doce.Descricao = (string)dr["descricao"];
                            doce.Valor = (decimal)dr["valor"];
                            doce.DataFab = (DateTime?)(dr["datafab"] != DBNull.Value ? dr["datafab"] : null);
                            doces.Add(doce);
                        }
                    }
                }
                Cache.add(chaveCache, doces, 30);
            }

            return doces;
        }

        public static Models.Doce getById(int id)
        {
            Models.Doce doce = new Models.Doce();

            using (SqlConnection conexao = new SqlConnection(Configuration.Parameters.getConnectionString()))
            {
                conexao.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conexao;
                    cmd.CommandText = "select id, descricao, valor, datafab from doce where id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        doce.Id = (int)dr["id"];
                        doce.Descricao = (string)dr["descricao"];
                        doce.Valor = (decimal)dr["valor"];
                        doce.DataFab = (DateTime?)(dr["datafab"] != DBNull.Value ? dr["datafab"] : null);
                    }
                }
            }
            return doce;
        }

        public static void update(Models.Doce doce)
        {
            using (SqlConnection conexao = new SqlConnection(Configuration.Parameters.getConnectionString()))
            {
                conexao.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conexao;
                    cmd.CommandText = "update doce set descricao=@descricao,valor=@valor,datafab=@datafab where id=@id;";
                    cmd.Parameters.Add(new SqlParameter("@descricao", System.Data.SqlDbType.VarChar)).Value = doce.Descricao;
                    cmd.Parameters.Add(new SqlParameter("@valor", System.Data.SqlDbType.Decimal)).Value = doce.Valor;
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = doce.Id;
                    if (doce.DataFab != null)
                        cmd.Parameters.Add(new SqlParameter("@datafab", System.Data.SqlDbType.Date)).Value = doce.DataFab;
                    else
                        cmd.Parameters.Add(new SqlParameter("@datafab", System.Data.SqlDbType.Date)).Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
            Cache.remove(chaveCache);
        }

        public static void deleteById(int id)
        {
            using (SqlConnection conexao = new SqlConnection(Configuration.Parameters.getConnectionString()))
            {
                conexao.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conexao;
                    cmd.CommandText = "delete from doce where id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            Cache.remove(chaveCache);
        }
    }
}