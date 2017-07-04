using Dapper;
using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Repository
{
    public class BaseRepository
    {
        private readonly string SqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        /// <summary>
        /// 获取链接
        /// </summary>
        /// <returns></returns>
        private DbConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// 执行查询sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="obj">参数对象</param>
        /// <returns>返回查询结果</returns>
        public object ExecuteSql(string sql, object obj)
        {
            using (var connection = GetConnection())
            {
                var model = connection.Query<object>(sql, obj).FirstOrDefault();
                connection.Close();
                return model;
            }
        }

        /// <summary>
        /// 执行查询sql,返回第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="obj">参数对象</param>
        /// <returns>返回查询结果</returns>
        public object ExecuteScalarSql(string sql, object obj = null)
        {
            using (var connection = GetConnection())
            {
                if (obj == null)
                {
                    var model = connection.ExecuteScalar(sql);
                    connection.Close();
                    return model;
                }
                else
                {
                    var model = connection.ExecuteScalar(sql, obj);
                    connection.Close();
                    return model;
                }
            }
        }

        /// <summary>
        /// 事务执行sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="obj">参数对象</param>
        /// <returns>返回影响行数</returns>
        public bool ExecuteTransactionSql(string sql, object obj)
        {
            using (var connection = GetConnection())
            {
                var trans = connection.BeginTransaction();
                try
                {
                    connection.Execute(sql, obj, trans);
                    trans.Commit();
                    connection.Close();
                    return true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    return false;
                    //throw;
                }
            }
        }

    }
}
