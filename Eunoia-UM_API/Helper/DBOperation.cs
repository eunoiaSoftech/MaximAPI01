using System.Data.Common;
using System.Data;
using System.Web;
using System.Data.SqlClient;

namespace Eunoia_UM_API.Helper
{
    public class DBOperation
    {

        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataAdapter adpt;
        private static DataSet ds;
        private static DataTable dt;

        static DBOperation() => con = new SqlConnection(DBCS.GetConnectionString());

        public static int ExecuteQuery(string spName, SqlParameter[] param)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(spName, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd = AddParameters(cmd, param);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ;
            }
            finally
            {
                con.Close();
            }
        }
        public static DataTable FillDataTable(string spName)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(spName, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable FillDataTable(string spName, SqlParameter[] param)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(spName, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd = AddParameters(cmd, param);
            dt = new DataTable();
            adpt = new SqlDataAdapter(cmd);
            try
            {
                adpt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public static SqlCommand AddParameters(SqlCommand cmd, SqlParameter[] param)
        {
            if (param != null)
            {
                foreach (SqlParameter sqlParameter in param)
                {
                    if (sqlParameter.Value == null || sqlParameter.Value.ToString() == "")
                        sqlParameter.Value = (object)DBNull.Value;
                    if (sqlParameter.SqlDbType != SqlDbType.VarBinary && sqlParameter.SqlDbType != SqlDbType.Image)
                        sqlParameter.Value = (object)HttpUtility.HtmlDecode(sqlParameter.Value.ToString());
                    cmd.Parameters.Add(sqlParameter);
                }
            }
            return cmd;
        }

        public static DataSet FillDataSet(string spName)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(spName, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            ds = new DataSet();
            adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            con.Close();
            return ds;
        }

        public static DataSet FillDataSet(string spName, SqlParameter[] param)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(spName, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd = AddParameters(cmd, param);
            ds = new DataSet();
            adpt = new SqlDataAdapter(cmd);
            try
            {
                adpt.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public static object ExecuteScalarFunction(string spName)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(spName, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd.ExecuteScalar();
        }

        public static object ExecuteScalarFunction(string spName, SqlParameter[] param)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(spName, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd = AddParameters(cmd, param);
            object obj = cmd.ExecuteScalar();
            con.Close();
            return obj;
        }

        public static string ExecuteScalarFunction_String(string spName)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(spName, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            string str = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return str;
        }

        public static Dictionary<string, string> ExecuteQueryWithMulipleOutputType(
          string spName,
          SqlParameter[] param)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand(spName, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd = AddParameters(cmd, param);
                cmd.ExecuteNonQuery();
                foreach (SqlParameter parameter in (DbParameterCollection)cmd.Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.ReturnValue || parameter.Direction == ParameterDirection.InputOutput)
                        dictionary.Add(parameter.ParameterName, parameter.Value.ToString());
                }
                return dictionary;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
