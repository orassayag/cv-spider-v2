<%@ WebHandler Language="C#" Class="NewFetchMails" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using JumboMail.Core;
using System.Net.Mail;

public class NewFetchMails : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        DoSearchWalla();
        //DoSearchBing();
    }

    public void DoSearchBing()
    {
        string city = Cities.GetRandomCity();
        string profession = Professions.GetRandomProfession();
        string mailType = MailTypes.GetRandomMailType();

        for (int i = 1; i < 30; i++)
        {
            string q1 = "דרוש " + profession + " ב" + city + " " + mailType;
            string q2 = "דרושה " + profession + " ב" + city + " " + mailType;
            string q3 = "דרושים " + profession + " ב" + city + " " + mailType;
            q1 = q1.Replace(" ", "+");
            q2 = q2.Replace(" ", "+");
            q3 = q3.Replace(" ", "+");
            int d = (i * 10) + 1;
            string y1 = string.Format("http://www.bing.com/search?q={0}&go=Submit&qs=n&pq={0}&sc=0-0&sp=-1&sk=&cvid=e0f00a35871b400eae82daf0831aca14&first={1}&FORM=PERE2", q1, d);
            string y2 = string.Format("http://www.bing.com/search?q={0}&go=Submit&qs=n&pq={0}&sc=0-0&sp=-1&sk=&cvid=e0f00a35871b400eae82daf0831aca14&first={1}&FORM=PERE2", q2, d);
            string y3 = string.Format("http://www.bing.com/search?q={0}&go=Submit&qs=n&pq={0}&sc=0-0&sp=-1&sk=&cvid=e0f00a35871b400eae82daf0831aca14&first={1}&FORM=PERE2", q3, d);
            string g1 = this.getPageSource(y1);
            string g2 = this.getPageSource(y2);
            string g3 = this.getPageSource(y3);
            List<string> urls1 = this.GetUrls(g1);
            List<string> urls2 = this.GetUrls(g2);
            List<string> urls3 = this.GetUrls(g3);
            foreach (string u in urls1)
            {
                this.GetMails(u);
            }
            foreach (string u in urls2)
            {
                this.GetMails(u);
            }
            foreach (string u in urls3)
            {
                this.GetMails(u);
            }
        }
    }
   
    public void DoSearchWalla()
    {
        string city = Cities.GetRandomCity();
        string profession = Professions.GetRandomProfession();
        string mailType = MailTypes.GetRandomMailType();

        for (int i = 11; i > 1; i--)
        {
            string q1 = "דרוש " + profession + " ב" + city + " " + mailType;
            string q2 = "דרושה " + profession + " ב" + city + " " + mailType;
            string q3 = "דרושים " + profession + " ב" + city + " " + mailType;
            q1 = q1.Replace(" ", "+");
            q2 = q2.Replace(" ", "+");
            q3 = q3.Replace(" ", "+");
            string y1 = string.Format("http://search.walla.co.il/?q={0}&type=text&page={1}", q1, i);
            string y2 = string.Format("http://search.walla.co.il/?q={0}&type=text&page={1}", q2, i);
            string y3 = string.Format("http://search.walla.co.il/?q={0}&type=text&page={1}", q3, i);
            string g1 = this.getPageSource(y1);
            string g2 = this.getPageSource(y2);
            string g3 = this.getPageSource(y3);
            List<string> urls1 = this.GetUrls(g1);
            List<string> urls2 = this.GetUrls(g2);
            List<string> urls3 = this.GetUrls(g3);
            foreach (string u in urls1)
            {
                this.GetMails(u);
            }
            foreach (string u in urls2)
            {
                this.GetMails(u);
            }
            foreach (string u in urls3)
            {
                this.GetMails(u);
            }
        }
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

    private int GetMails(string source)
    {
        int count = 0;
        string g = this.getPageSource(source);
        if (string.IsNullOrEmpty(g))
        {
            return 0;
        }

        Regex t = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        foreach (Match m in t.Matches(g))
        {
            if (ValidateMail(m.Value))
            {
                string mail = ClearEmail(m.Value);
                bool IsNotExists = BLL.GetMail(mail);
                if (IsNotExists)
                {
                    try
                    {
                        lock (this)
                        {
                            BLL.InsertMail(mail);
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
        return mail.Trim();
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

        string[] splitter = mail.Split('@');
        foreach (string m in splitter)
        {
            if (!string.IsNullOrEmpty(m))
            {
                if (m.Trim().Length <= 2)
                {
                    return false;
                }
            }
        }

        try
        {
            mail = mail.Trim();
            MailAddress m = new MailAddress(mail);
            if (mail.Contains("-"))
            {
                if (mail.Contains("@-"))
                {
                    return false;
                }

                if (mail.Contains("-."))
                {
                    int i = mail.IndexOf("-.");
                    int d = mail.IndexOf("@");
                    if (i > d)
                    {
                        return false;
                    }
                }
            }

            if (mail.Contains(".."))
            {
                return false;
            }
        }
        catch (FormatException)
        {
            return false;
        }
        return true;
    }

    public bool IsReusable { get { return false; } }
}