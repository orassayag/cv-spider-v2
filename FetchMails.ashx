<%@ WebHandler Language="C#" Class="FetchMails" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

public class FetchMails : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        int count = DoSearch();
        int prev = 0;
        if (!string.IsNullOrEmpty(context.Request["i"]))
        {
            int.TryParse(context.Request["i"], out prev);
        }
        context.Response.Write(count + prev);
        context.Response.End();
    }

    public List<string> MailsListSession = new List<string>();
    private CVIma2DataContext dal = new CVIma2DataContext();

    public int DoSearch()
    {
        int count = 0;
        List<string> Urls = new List<string>();
        string city = Cities.GetRandomCity();
        string profession = Professions.GetRandomProfession();
        string mailType = MailTypes.GetRandomMailType();

        string q = "דרושים " + profession + " ב" + city + " " + mailType;
        q = q.Replace(" ", "+");

        LastID lastID = this.dal.LastIDs.SingleOrDefault(y => y.sdfsdgdf == "1");
        if (lastID == null)
        {
            return count;
        }

        for (int i = 11; i > 1; i--)
        {
            string query = string.Format("http://search.walla.co.il/?q={0}&type=text&page={1}", q, i);
            string g = this.getPageSource(query);
            List<string> urls = this.GetUrls(g);
            foreach (string u in urls)
            {
                count += this.GetMails(u, lastID);
            }
        }
        return count;
    }

    public List<string> GetUrls(string html)
    {
        List<string> List = new List<string>();
        string HRefPattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";
        Regex r = new Regex(HRefPattern);
        try
        {
            MatchCollection mc = r.Matches(html);
            foreach (Match m in mc)
            {
                string t = m.Value;
                if (string.IsNullOrEmpty(t))
                {
                    continue;
                }

                if (t.Contains("walla") || t.Contains(".css") || t.Contains("/?q="))
                {
                    continue;
                }

                t = t.Replace("href=", string.Empty);
                t = t.Replace("href = ", string.Empty);
                t = t.Replace("\"", string.Empty);
                t = t.Replace("\\", string.Empty);
                if (t.Length < 5)
                {
                    continue;
                }
                List.Add(t);
            }
        }
        catch (Exception) { }
        return List;
    }

    public string getPageSource(string URL)
    {
        try
        {
            WebClient webClient = new WebClient();
            string strSource = webClient.DownloadString(URL);
            webClient.Dispose();
            return strSource;
        }
        catch (Exception)
        {
        }
        return string.Empty;
    }

    private int GetMails(string source, LastID lastID)
    {
        int count = 0;
        string g = this.getPageSource(source);
        if (string.IsNullOrEmpty(g))
        {
            return 0;
        }

        long result = 0;
        Regex t = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        foreach (Match m in t.Matches(g))
        {
            if (ValidateMail(m.Value))
            {
                string mail = ClearEmail(m.Value);
                CVMail f = dal.CVMails.SingleOrDefault(b => b.Mail == mail);
                if (f == null)
                {
                    try
                    {
                        lock (this)
                        {
                            result = (long)lastID.LastID1;
                            dal.CVMails.InsertOnSubmit(new CVMail
                            {
                                asdws = (result + 1),
                                Date = DateTime.Now,
                                Mail = mail
                            });

                            lastID.LastID1++;
                            dal.SubmitChanges();
                            count++;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        return count;
    }

    public string ClearEmail(string mail)
    {
        mail = mail.Replace("/", "");
        mail = mail.Replace("\\", "");
        mail = mail.Replace(".co", ".co.il");
        mail = mail.Replace("!", "");
        mail = mail.Replace("'", "");
        mail = mail.Replace("\"", "");
        mail = mail.Replace("?", "");
        mail = mail.Replace(".coil", ".co.il");
        mail = mail.Replace(".ilm", ".il");
        mail = mail.Replace("%", "");
        mail = mail.Replace("|", "");
        mail = mail.Replace("org.i", "org.il");
        mail = mail.Replace("con", "com");
        mail = mail.Replace(".co.ili", ".co.il");
        mail = mail.Replace(".njet", ".net");
        mail = mail.Replace(".net.i", ".net.il");
        mail = mail.Replace(".met", ".net");
        mail = mail.Replace(".co.oil", ".co.il");
        mail = mail.Replace(".ill", ".il");
        mail = mail.Replace(".co.i", ".co.il");
        mail = mail.Replace(".walla.c", ".walla.com");
        mail = mail.Replace(".com2", ".com");
        mail = mail.Replace("@.", "@");
        mail = mail.Replace(".co.ill", ".co.il");
        mail = mail.Replace(".walla.co", ".walla.co.il");
        mail = mail.Replace("gmail.comm", "gmail.com");
        mail = mail.Replace("gmail.com.il", "gmail.com");
        mail = mail.Replace(".org.ill", ".org.il");
        mail = mail.Replace(".gov.i", ".gov.il");
        mail = mail.Replace(".walla.cil", ".walla.co.il");
        mail = mail.Replace("gmail.co", "gmail.com");
        mail = mail.Replace("mailto%20", "");
        mail = mail.Replace("mailto:", "");
        mail = mail.Replace("mailto", "");
        mail = mail.Replace("%20", "");
        mail = mail.Replace(".muni.i", "muni.il");
        mail = mail.Replace("^", "");
        mail = mail.Replace(".netl", ".net");
        mail = mail.Replace(".co.il1", ".co.il");
        mail = mail.Replace(".comcom", ".com");
        mail = mail.Replace(".comm", ".com");
        mail = mail.Replace(".co.ill", ".co.il");
        mail = mail.Replace(".ill", ".il");
        mail = mail.Replace(".co.il.il", ".co.il");
        mail = mail.Replace(".co.il.l", ".co.il");
        mail = mail.Replace(".com.il", ".com");
        mail = mail.Replace(".comn", ".com");
        mail = mail.Replace(".co.il.i", ".co.il");
        mail = mail.Replace(".co.ilcom", ".com");
        mail = mail.Replace(".co.ill", ".co.il");
        if (mail.Contains('='))
        {
            mail = mail.Split('=')[1];
        }
        if (mail.Contains(".."))
        {
            mail = string.Join(".", mail.Split(new char[] { '.' },
                                        StringSplitOptions.RemoveEmptyEntries));
        }
        if (mail.Contains("@."))
        {
            mail = mail.Replace("@.", "@");
        }
        return mail;
    }

    public bool ValidateMail(string mail)
    {
        if (string.IsNullOrEmpty(mail))
        {
            return false;
        }

        if (!mail.Contains("@"))
        {
            return false;
        }

        if (mail.Contains(".jpg"))
        {
            return false;
        }

        if (mail.Contains(".png"))
        {
            return false;
        }

        string[] spliter = mail.Split('@');
        foreach (string m in spliter)
        {
            if (!string.IsNullOrEmpty(m))
            {
                if (m.Trim().Length <= 2)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsReusable { get { return false; } }
}