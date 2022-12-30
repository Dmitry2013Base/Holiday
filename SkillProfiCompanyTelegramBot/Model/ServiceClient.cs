using Newtonsoft.Json;

namespace SkillProfiCompanyTelegramBot.Model
{
    public class ServiceClient
    {
        public string Header { get; set; }
        public string Description { get; set; }

        [JsonConstructor]
        public ServiceClient(string header, string description)
        {
            Header = header;
            Description = description;
        }
    }
}
