using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace L2
{
    class Exell
    {
        private string file = "";

        public Exell(string namefile)
        {
            namefile = namefile.Insert(namefile.Length, ".csv");
            file = namefile;
            File.Create(file);
        }

        public void Type(string type)
        {
            if (file == "")
                return;

            if (!File.Exists(file))
                return;

            using (StreamWriter writer = new StreamWriter(file, true, Encoding.UTF8))
                writer.WriteLine(type);
        }
    }

    class Finder
    {
        private string head;
        private uint nest = 0;
        private uint count = 0;
        private uint maxNesting = 8;
        private uint maxCount = 31;
        public event Action<string> info;
        private Hashtable ht = new Hashtable();
        private Hashtable htMail = new Hashtable();

        private void Info(string name, string adrs, uint nest, string mail)
        {
            var typer = info;
            if (typer != null)
            {
                string str;
                string ne = nest.ToString();
                name = name.Remove(0, 7);
                name = name.Remove(name.Length - 8, 8);

                for (int i = 1; i < nest; i++)
                {
                    int offset = (i - 1) * 3;
                    name = name.Insert(offset, "|--");
                }

                name = name.Insert(name.Length, "; ");
                adrs = adrs.Insert(adrs.Length, "; ");
                ne = ne.Insert(ne.Length, "; ");
                mail = mail.Insert(mail.Length, "; ");
                str = String.Concat(name, adrs, ne, mail);
                info(str);
            }
        }

        public Finder(string _head)
        {
            head = _head;
        }

        public void Find(string str)
        {
           
            if (count > maxCount)
                return;

            if (nest > maxNesting)
                return;

            if (ht.ContainsKey(str.GetHashCode()))
                return;

            WebClient client = new WebClient();

            string page;
            try { 
                page = client.DownloadString(new Uri(str)); }
            catch (WebException) { return; }

            ht.Add(str.GetHashCode(), str); count++;
            var mails = Regex.Matches(page, @"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+[-\._0-9a-z]");

            foreach (var mail in mails)
            {
                if (htMail.ContainsKey((mail.ToString()).GetHashCode()))
                    continue;

                htMail.Add((mail.ToString()).GetHashCode(), mail.ToString());
                var sites = Regex.Matches(page, @"<title>+[""\/\w-\.:а-яА-Я0-9\s]+<\/title>");
                foreach (var site in sites)
                    Info(site.ToString(), str, nest, mail.ToString());
            }

            var items = Regex.Matches(page, @"<a href=[""\/\w-\.:]+>");
            foreach (var item in items)
            {
                string tmp = item.ToString();
                if (tmp.Contains("www"))
                {
                    nest++; Find(tmp); nest--;
                    continue;
                }

                tmp = tmp.Remove(tmp.Length - 2, 2);
                tmp = tmp.Remove(0, 9);
                tmp = String.Concat(head, tmp);
                nest++; Find(tmp); nest--;
            }
        }
    }

    class GO
    {
        static void Main()
        {
            Finder p = new Finder("http://www.susu.ru");
            Exell excel = new Exell("output");

            p.info += str =>
            {
                Console.WriteLine("");
                Console.WriteLine("FIND NEW MAIL");
                Console.WriteLine(str, System.Text.Encoding.GetEncoding(1251));
                excel.Type(str);
                Console.WriteLine("");
            };

            p.Find("http://www.susu.ru");
            Console.ReadKey();
            
            
        }
    }
}
