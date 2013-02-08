using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace JumboMail.Core
{
    public class BLL
    {
        public static void UpdateUsedMails(long Mail_ID)
        {
            DAL.UpdateUsedMails(Mail_ID);
        }

        public static bool GetMail(string Mail_Value)
        {
            DataRow R = DAL.GetMail(Mail_Value);
            return R == null;
        }

        public static List<string> GetCVMails(long Mail_ID)
        {
            DataTable T = DAL.GetCVMails(Mail_ID);
            List<string> List = new List<string>();
            if (T != null)
            {
                if (T.Rows.Count > 0)
                {
                    foreach(DataRow R in T.Rows)
                    {
                        if (R != null)
                        {
                            if (R["Mail_Value"] != null)
                            {
                                if (!string.IsNullOrEmpty(R["Mail_Value"].ToString()))
                                {
                                    List.Add(R["Mail_Value"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            return List;
        }

        public static void InsertMail(string Mail_Value)
        {
            DAL.InsertMail(Mail_Value);
        }
    }
}