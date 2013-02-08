<%@ WebHandler Language="C#" Class="PrintMails" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using JumboMail.Core;

public class PrintMails : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        PrintToNotePad(long.Parse(context.Request["i"]));
    }

    public void PrintToNotePad(long id)
    {
        using (FileStream fs = new FileStream(@"C:\Or\Web\CV\mails1.txt", FileMode.Append, FileAccess.Write))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                List<string> f = BLL.GetCVMails(id);
                foreach (var m in f)
                {
                    writer.Write(m + ", ");
                }
            }
        }
        BLL.UpdateUsedMails(id);
    }

    public bool IsReusable { get { return false; } }
}