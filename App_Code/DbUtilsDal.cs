using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class DbUtilsDal
{
    public const string MainDB = "DB";

    private DbUtilsDal() { }

    public static SqlConnection SetConnection(string connectionName)
    {
        string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        SqlConnection connection = null;
        if (!string.IsNullOrEmpty(connectionString))
        {
            connection = new SqlConnection(connectionString);
        }
        return connection;
    }

    public static SqlConnection OpenConnection(string connectionName)
    {
        SqlConnection connection = SetConnection(connectionName);
        if (connection != null && connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        return connection;
    }

    public static SqlCommand CreateCommand(SqlConnection connection, CommandType commandType, string commandText, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandType = commandType;
        cmd.CommandText = commandText;
        for (int index = 0; index < parametersNames.Length; index++)
        {
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = parametersNames[index],
                SqlDbType = parametersTypes[index],
                SqlValue = parametersValues[index]
            });
        }
        return cmd;
    }

    #region ExecuteReader

    /// <summary>
    /// Sends a stored procedure command without parameter to SqlCommand.ExecuteReader method
    /// </summary>
    /// <param name="connection">SqlConnection to connect to</param>
    /// <param name="procedureName">Sql stored procedure name</param>
    /// <returns></returns>

    public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
    {
        return ExecuteReader(connection, commandType, commandText, new string[] { }, new SqlDbType[] { }, new object[] { });
    }

    /// <summary>
    /// Sends a stored procedure command to SqlCommand.ExecuteReader method
    /// </summary>
    /// <param name="connection">SqlConnection to connect to</param>
    /// <param name="procedureName">Sql stored procedure name</param>
    /// <param name="parametersNames">An array of paraneter names to pass to the SqlCommand object</param>
    /// <param name="parametersTypes">An array of paraneter types to pass to the SqlCommand object</param>
    /// <param name="parametersValues">An array of paraneter values to pass to the SqlCommand object</param>
    /// <returns></returns>
    public static SqlDataReader ExecuteReader(SqlConnection connection, string procedureName, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        return ExecuteReader(connection, CommandType.StoredProcedure, procedureName, parametersNames, parametersTypes, parametersValues);
    }

    public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        SqlCommand cmd = CreateCommand(connection, commandType, commandText, parametersNames, parametersTypes, parametersValues);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
    }

    #endregion

    #region ExecuteScalar

    /// <summary>
    /// Sends a stored procedure command without parameter to SqlCommand.ExecuteScalar method
    /// </summary>
    /// <param name="connection">SqlConnection to connect to</param>
    /// <param name="procedureName">Sql stored procedure name</param>
    /// <returns></returns>
    public static object ExecuteScalar(SqlConnection connection, string procedureName)
    {
        return ExecuteScalar(connection, CommandType.StoredProcedure, procedureName, new string[] { }, new SqlDbType[] { }, new object[] { });
    }

    /// <summary>
    /// Sends a stored procedure command to SqlCommand.ExecuteScalar method
    /// </summary>
    /// <param name="connection">SqlConnection to connect to</param>
    /// <param name="procedureName">Sql stored procedure name</param>
    /// <param name="parametersNames">An array of paraneter names to pass to the SqlCommand object</param>
    /// <param name="parametersTypes">An array of paraneter types to pass to the SqlCommand object</param>
    /// <param name="parametersValues">An array of paraneter values to pass to the SqlCommand object</param>
    /// <returns></returns>
    public static object ExecuteScalar(SqlConnection connection, string procedureName, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        return ExecuteScalar(connection, CommandType.StoredProcedure, procedureName, parametersNames, parametersTypes, parametersValues);
    }

    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        SqlCommand cmd = CreateCommand(connection, commandType, commandText, parametersNames, parametersTypes, parametersValues);
        return cmd.ExecuteScalar();
    }

    #endregion

    #region ExecuteNonQuery

    /// <summary>
    /// Sends a stored procedure command without parameter to SqlCommand.ExecuteNonQuery method
    /// </summary>
    /// <param name="connection">SqlConnection to connect to</param>
    /// <param name="procedureName">Sql stored procedure name</param>
    /// <returns></returns>
    public static int ExecuteNonQuery(SqlConnection connection, string procedureName)
    {
        return ExecuteNonQuery(connection, CommandType.StoredProcedure, procedureName, new string[] { }, new SqlDbType[] { }, new object[] { });
    }

    /// <summary>
    /// Sends a stored procedure command to SqlCommand.ExecuteNonQuery method
    /// </summary>
    /// <param name="connection">SqlConnection to connect to</param>
    /// <param name="procedureName">Sql stored procedure name</param>
    /// <param name="parametersNames">An array of paraneter names to pass to the SqlCommand object</param>
    /// <param name="parametersTypes">An array of paraneter types to pass to the SqlCommand object</param>
    /// <param name="parametersValues">An array of paraneter values to pass to the SqlCommand object</param>
    /// <returns></returns>
    public static int ExecuteNonQuery(SqlConnection connection, string procedureName, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        return ExecuteNonQuery(connection, CommandType.StoredProcedure, procedureName, parametersNames, parametersTypes, parametersValues);
    }

    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        SqlCommand cmd = CreateCommand(connection, commandType, commandText, parametersNames, parametersTypes, parametersValues);
        return cmd.ExecuteNonQuery();
    }

    #endregion

    #region ExecuteDataTable

    /// <summary>
    /// Sends a stored procedure command and returns a DataTable object
    /// </summary>
    /// <param name="connection">SqlConnection to connect to</param>
    /// <param name="procedureName">Sql stored procedure name</param>
    /// <returns></returns>
    public static DataTable ExecuteDataTable(SqlConnection connection, string procedureName)
    {
        return ExecuteDataTable(connection, CommandType.StoredProcedure, procedureName, new string[] { }, new SqlDbType[] { }, new object[] { });
    }

    /// <summary>
    /// Sends a stored procedure command and returns a DataTable object
    /// </summary>
    /// <param name="connection">SqlConnection to connect to</param>
    /// <param name="procedureName">Sql stored procedure name</param>
    /// <param name="parametersNames">An array of paraneter names to pass to the SqlCommand object</param>
    /// <param name="parametersTypes">An array of paraneter types to pass to the SqlCommand object</param>
    /// <param name="parametersValues">An array of paraneter values to pass to the SqlCommand object</param>
    /// <returns></returns>
    public static DataTable ExecuteDataTable(SqlConnection connection, string procedureName, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        return ExecuteDataTable(connection, CommandType.StoredProcedure, procedureName, parametersNames, parametersTypes, parametersValues);
    }

    public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        SqlCommand cmd = CreateCommand(connection, commandType, commandText, parametersNames, parametersTypes, parametersValues);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable returnedTable = new DataTable();
        adapter.Fill(returnedTable);
        return returnedTable;
    }

    #endregion

    #region ExecuteDataRow

    /// <summary>
    /// Sends a stored procedure command and returns a DataRow object
    /// </summary>
    /// <param name="connection">SqlConnection to connect to</param>
    /// <param name="procedureName">Sql stored procedure name</param>
    /// <param name="parametersNames">An array of paraneter names to pass to the SqlCommand object</param>
    /// <param name="parametersTypes">An array of paraneter types to pass to the SqlCommand object</param>
    /// <param name="parametersValues">An array of paraneter values to pass to the SqlCommand object</param>
    /// <returns></returns>
    public static DataRow ExecuteDataRow(SqlConnection connection, string procedureName, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        return ExecuteDataRow(connection, CommandType.StoredProcedure, procedureName, parametersNames, parametersTypes, parametersValues);
    }

    public static DataRow ExecuteDataRow(SqlConnection connection, CommandType commandType, string commandText, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
    {
        DataTable table = ExecuteDataTable(connection, commandType, commandText, parametersNames, parametersTypes, parametersValues);
        if (table.Rows.Count > 0)
        {
            return table.Rows[0];
        }
        return null;
    }

    #endregion
}