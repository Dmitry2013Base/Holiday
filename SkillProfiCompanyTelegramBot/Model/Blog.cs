using Newtonsoft.Json;

namespace SkillProfiCompanyTelegramBot.Model
{
    public class Blog
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }

        [JsonConstructor]
        public Blog(int id, string header, string imageBlog)
        {
            Id = id;
            Header = header;
            Image = imageBlog;
        }
    }
}
