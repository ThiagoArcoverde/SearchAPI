using Microsoft.EntityFrameworkCore;

namespace APISearch.Model
{
    public class Result
    {
        public Result(string title, string link)
        {
            Title = title;
            Link = link;
        }
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
