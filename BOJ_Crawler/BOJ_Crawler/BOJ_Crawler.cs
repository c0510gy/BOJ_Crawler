using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.XPath;

class BOJCrawler
{
    const string url = "https://www.acmicpc.net/user/";

    public class Info
    {
        // 랭킹, 푼 문제, 제출, 맞았습니다, 출력 형식, 틀렸습니다, 시간 초과, 메모리 초과, 출력 초과, 런타임 에러, 컴파일 에러
        // 푼 문제, 시도했지만 풀지 못한 문제
        public int rank;
        public int solved;
        public int submitted;
        public int AC; //Accepted
        public int PE; //Presentation Error
        public int WA; //Wrong Answer
        public int TLE; //Time Limit Exceed
        public int MLE; //Memory Limit Exceed
        public int OLE; //Output Limit Exceed
        public int RE; //Runtime Error
        public int CE; //Compile Error
        public int[] solved_list; //푼 문제
        public int[] tried_list; //시도했지만 풀지 못한 문제
    }

    public BOJCrawler()
    {

    }

    private string get_split(string str, string spl, int idx)
    {
        return str.Split(new string[] { spl }, StringSplitOptions.None)[idx];
    }

    public Info get_user_info(string user_name)
    {
        Info info = new Info();
        Uri targetUri = new Uri(url + user_name); HttpWebRequest webRequest = HttpWebRequest.Create(targetUri) as HttpWebRequest;
        try
        {
            using (HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse)
            using (Stream webResponseStream = webResponse.GetResponseStream())
            {
                HtmlDocument BOJdoc = new HtmlDocument();
                Encoding encoding = Encoding.UTF8;
                BOJdoc.Load(webResponseStream, encoding, true);
                string table = BOJdoc.DocumentNode.SelectSingleNode("//table[@id='statics']").InnerHtml;

                HtmlNode solved_list_html = BOJdoc.DocumentNode.SelectNodes("//div[@class='panel panel-default']")[0];
                HtmlNodeCollection solved_list = solved_list_html.SelectNodes("div[@class='panel-body']/span[@class='problem_number']");
                HtmlNode tried_list_html = BOJdoc.DocumentNode.SelectNodes("//div[@class='panel panel-default']")[1];
                HtmlNodeCollection tried_list = tried_list_html.SelectNodes("div[@class='panel-body']/span[@class='problem_number']");

                if (solved_list != null)
                {
                    info.solved_list = new int[solved_list.Count];
                    for (int j = 0; j < solved_list.Count; j++)
                    {
                        info.solved_list[j] = Convert.ToInt32(solved_list[j].SelectSingleNode("a").InnerText);
                    }
                }
                else
                {
                    info.solved_list = new int[0];
                }
                if (tried_list != null)
                {
                    info.tried_list = new int[tried_list.Count];
                    for (int j = 0; j < tried_list.Count; j++)
                    {
                        info.tried_list[j] = Convert.ToInt32(tried_list[j].SelectSingleNode("a").InnerText);
                    }
                }
                else
                {
                    info.tried_list = new int[0];
                }

                string rank = table.Contains("<th>랭킹</th>") ? get_split(get_split(get_split(table, "<th>랭킹</th>", 1), "</td>", 0), "<td>", 1) : "-1";
                string solved = table.Contains("<th>푼 문제</th>") ? get_split(get_split(get_split(table, "<th>푼 문제</th>", 1), "</a>", 0), "'>", 1) : "-1";
                string submitted = table.Contains("<th>제출</th>") ? get_split(get_split(get_split(table, "<th>제출</th>", 1), "</a>", 0), "'>", 1) : "-1";
                string AC = table.Contains("<th>맞았습니다</th>") ? get_split(get_split(get_split(table, "<th>맞았습니다</th>", 1), "</a>", 0), "\">", 1) : "-1";
                string PE = table.Contains("<th>출력 형식</th>") ? get_split(get_split(get_split(table, "<th>출력 형식</th>", 1), "</a>", 0), "\">", 1) : "-1";
                string WA = table.Contains("<th>틀렸습니다</th>") ? get_split(get_split(get_split(table, "<th>틀렸습니다</th>", 1), "</a>", 0), "\">", 1) : "-1";
                string TLE = table.Contains("<th>시간 초과</th>") ? get_split(get_split(get_split(table, "<th>시간 초과</th>", 1), "</a>", 0), "\">", 1) : "-1";
                string MLE = table.Contains("<th>메모리 초과</th>") ? get_split(get_split(get_split(table, "<th>메모리 초과</th>", 1), "</a>", 0), "\">", 1) : "-1";
                string OLE = table.Contains("<th>출력 초과</th>") ? get_split(get_split(get_split(table, "<th>출력 초과</th>", 1), "</a>", 0), "\">", 1) : "-1";
                string RE = table.Contains("<th>런타임 에러</th>") ? get_split(get_split(get_split(table, "<th>런타임 에러</th>", 1), "</a>", 0), "\">", 1) : "-1";
                string CE = table.Contains("<th>컴파일 에러</th>") ? get_split(get_split(get_split(table, "<th>컴파일 에러</th>", 1), "</a>", 0), "\">", 1) : "-1";
                info.rank = Convert.ToInt32(rank);
                info.solved = Convert.ToInt32(solved);
                info.submitted = Convert.ToInt32(submitted);
                info.AC = Convert.ToInt32(AC);
                info.PE = Convert.ToInt32(PE);
                info.WA = Convert.ToInt32(WA);
                info.TLE = Convert.ToInt32(TLE);
                info.MLE = Convert.ToInt32(MLE);
                info.OLE = Convert.ToInt32(OLE);
                info.RE = Convert.ToInt32(RE);
                info.CE = Convert.ToInt32(CE);
            }
        }
        catch
        {
            return null;
        }
        
        return info;
    }
}