using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Data.Linq;

public partial class _Default : Page
{
    private List<string> list = new List<string>();
    //private CVOrDataContext dal = new CVOrDataContext();
    //private CVAviDataContext dal = new CVAviDataContext();
    //private CVDoronDataContext dal = new CVDoronDataContext();
    //private CVImaDataContext dal = new CVImaDataContext();
    //private CVGalitDataContext dal = new CVGalitDataContext();
    //private CVYaelDataContext dal = new CVYaelDataContext();
    private CVIma2DataContext dal = new CVIma2DataContext();

    private static int i;
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

    protected void runJob_Click(object sender, EventArgs e)
    {
        LastID lastID = this.dal.LastIDs.SingleOrDefault(y => y.sdfsdgdf == "1");
        if (lastID == null)
        {
            return;
        }

        //StreamReader reader = new StreamReader(@"C:\Or\Web\CV\urls_doron.txt");
        //StreamReader reader = new StreamReader(@"C:\Users\Or\Desktop\aviCv.txt");
        StreamReader reader = new StreamReader(@"C:\Or\Web\CV\ima_urls.txt");
        List<string> urls = new List<string>();
        while (!reader.EndOfStream)
        {
            string url = reader.ReadLine();
            if (!string.IsNullOrEmpty(url))
            {
                if (!urls.Contains(url) && !url.Contains("google"))
                {
                    urls.Add(reader.ReadLine());
                }
            }
        }
        if (reader != null)
        {
            reader.Close();
        }

        StreamWriter writer = new StreamWriter(@"C:\Or\Web\CV\mails.txt", true);

        foreach (string m in urls)
        {
            if (!string.IsNullOrEmpty(m))
            {
                this.GetMails(m, writer, lastID);
            }
        }

        int count = 0;
        int totalCount = 0;

        if (this.Session["total"] != null)
        {
            totalCount = (int)this.Session["total"];
        }

        totalCount += count;
        this.Session["total"] = totalCount;

        this.doneLabel.Text = count.ToString();
        this.totalLabel.Text = totalCount.ToString();

        if (writer != null)
        {
            writer.Close();
        }
    }

    public bool ValidateMail(string mail)
    {
        bool IsValid = true;
        if (string.IsNullOrEmpty(mail))
        {
            IsValid = false;
        }

        if (!mail.Contains("@"))
        {
            IsValid = false;
        }

        if (mail.Contains(".jpg"))
        {
            IsValid = false;
        }

        if (mail.Contains(".png"))
        {
            IsValid = false;
        }
        return IsValid;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //PrintToTextFile();

        if (!Page.IsPostBack)
        {
            List<string> list = new List<string>();
            StreamReader r = new StreamReader(@"C:\Or\Web\CV\mails.txt");

            StringBuilder build = new StringBuilder();

            while (!r.EndOfStream)
            {
                string url = r.ReadLine();
                if (!url.Contains("google"))
                {
                    build.Append(r.ReadLine());
                }
            }

            string[] stringer = build.ToString().Split(',');
            foreach (string m in stringer)
            {
                if (!list.Contains(m))
                {
                    this.list.Add(m);
                }
            }

            this.doneLabel.Text = "0";
            this.totalLabel.Text = i.ToString();

            this.Session["total"] = stringer.Count();
            this.Session["list"] = list;

            r.Close();
        }
    }

    private int GetMails(string source, StreamWriter writer, LastID lastID)
    {
        if (this.Session["list"] != null)
        {
            this.list = (List<string>)this.Session["list"];
        }

        string g = this.getPageSource(source);

        if (string.IsNullOrEmpty(g))
        {
            return 0;
        }

        long result = 0;
        int count = 0;

        Regex t = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        foreach (Match m in t.Matches(g))
        {
            if (!list.Contains(m.Value))
            {
                if (ValidateMail(m.Value))
                {
                    string mail = ClearEmail(m.Value);

                    CVMail f = dal.CVMails.SingleOrDefault(b => b.Mail == mail);
                    if (f == null)
                    {
                        try
                        {
                            result = (long)lastID.LastID1;

                            dal.CVMails.InsertOnSubmit(new CVMail
                            {
                                Mail = mail,
                                asdws = (result + 1)
                            });

                            lastID.LastID1++;

                            dal.SubmitChanges();

                            this.list.Add(mail);
                            writer.Write(mail + ", ");
                            count++;
                            i++;
                        }
                        catch (Exception) { }
                    }
                }
            }
        }

        this.Session["list"] = this.list;
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

    //public void PrintToTextFile()
    //{
    //    StreamWriter writer = new StreamWriter(@"C:\Or\Web\CV\mails.txt");
    //    StringBuilder build = new StringBuilder();
    //    Table<Mail> mails = dal.Mails;
    //    foreach (Mail m in mails)
    //    {
    //        string mail = m.Mail1;
    //        mail = ClearEmail(mail);
    //        build.Append(mail + ",");
    //    }
    //    writer.WriteLine(build.ToString());
    //    writer.Close();
    //}
}