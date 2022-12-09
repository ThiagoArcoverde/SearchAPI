using APISearch.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APISearch.Repositories
{
    public class ResultRepository : IResultRepository
    {
        const string quote = "\"";

        public ResultRepository()
        {
       
        }
        public async Task<IEnumerable<Result>> GetResults(string str)
        {
            HttpClient client = new HttpClient();
            List<Result> resultList = new List<Result>();
            try
            {
                string url = "https://www.google.com/search?q=" + str;
                int index = 1;
                using HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string htmlTxt = await response.Content.ReadAsStringAsync();
                while (htmlTxt != null)
                {
                    htmlTxt = GetTitle(htmlTxt);
                    if(htmlTxt != null)
                    {
                        index = htmlTxt.IndexOf("</div>");
                        string titulo = htmlTxt.Substring(0, index);
                        titulo = FixEncoding(titulo);
                        htmlTxt = GetLink(htmlTxt);
                        index = htmlTxt.IndexOf("</div>");
                        string link = htmlTxt.Substring(0, index);
                        link = FixEncoding(link);
                        link = FixLinkUrl(link);
                        Result result = new Result(titulo, link);
                        resultList.Add(result);
                    }
                }
                return resultList;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }

        static string GetTitle(string str)
        {
            int index = str.IndexOf(quote + "><h3");
            if (index != -1)
            {
            str = str.Remove(0, index);
            index = str.IndexOf("><div");
            str = str.Remove(0, index);
            index = str.IndexOf(quote + ">");
            str = str.Remove(0, index + 2);
            return str;
            }
            return null;
        }   

        static string GetLink(string str)
        {
            if (str != null)
            {
            int index = str.IndexOf("<div class=" + quote);
            str = str.Remove(0, index + 1);
            index = str.IndexOf("<div class=" + quote);
            str = str.Remove(0, index + 1);
            index = str.IndexOf(quote + ">");
            str = str.Remove(0, index + 2);
            return str;
            }
            return null; 
        }

        static string FixEncoding(string str)
        {
            bool verify = true;
            while (verify == true)
            {
                int index = str.IndexOf("&#8211;");
                if (index >= 0)
                {
                    int strtam = str.Length;
                    string aux1 = str.Substring(0, index);
                    string aux2 = str.Substring(index + 7, strtam - (index + 7));
                    str = aux1 + "–" + aux2;
                }
                else
                {
                    verify = false;
                }
            }

            verify = true;
            while (verify == true)
            {
                int index = str.IndexOf(" &#8250; ");
                if (index >= 0)
                {
                    int strtam = str.Length;
                    string aux1 = str.Substring(0, index);
                    string aux2 = str.Substring(index + 9, strtam - (index + 9));
                    str = aux1 + "/" + aux2;
                }
                else
                {
                    verify = false;
                }
            }
            return str;
        }

        static string FixLinkUrl(string str)
        {
            bool verify = str.EndsWith("...");
            if(verify == true)
            {
                str = str.Remove(str.Length - 3);
            }
            return str;
        }
    }
}
