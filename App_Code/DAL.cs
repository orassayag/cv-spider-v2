using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace JumboMail.Core
{
    public class DAL
    {

        internal static void UpdateUsedMails(long Mail_ID)
        {
            using (SqlConnection con = DbUtilsDal.OpenConnection(DbUtilsDal.MainDB))
            {
                DbUtilsDal.ExecuteDataRow(con, "dbo.UpdateUsedMails",
                           new string[] { "@Mail_ID" },
                           new SqlDbType[] { SqlDbType.BigInt },
                           new object[] { Mail_ID });
            }
        }

        internal static DataRow GetMail(string Mail_Value)
        {
            DataRow R = null;
            using (SqlConnection con = DbUtilsDal.OpenConnection(DbUtilsDal.MainDB))
            {
                R = DbUtilsDal.ExecuteDataRow(con, "dbo.GetMail",
                               new string[] { "@Mail_Value" },
                               new SqlDbType[] { SqlDbType.VarChar },
                               new object[] { Mail_Value });
            }
            return R;
        }

        internal static DataTable GetCVMails(long Mail_ID)
        {
            DataTable T = null;
            using (SqlConnection con = DbUtilsDal.OpenConnection(DbUtilsDal.MainDB))
            {
                T = DbUtilsDal.ExecuteDataTable(con, "dbo.GetCVMails",
                               new string[] { "@Mail_ID" },
                               new SqlDbType[] { SqlDbType.BigInt },
                               new object[] { Mail_ID });
            }
            return T;
        }

        internal static void InsertMail(string Mail_Value)
        {
            using (SqlConnection con = DbUtilsDal.OpenConnection(DbUtilsDal.MainDB))
            {
                DbUtilsDal.ExecuteDataRow(con, "dbo.InsertMail",
                               new string[] { "@Mail_Value" },
                               new SqlDbType[] { SqlDbType.VarChar },
                               new object[] { Mail_Value });
            }
        }
    }
}