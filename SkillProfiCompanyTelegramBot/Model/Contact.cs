using Newtonsoft.Json;

namespace SkillProfiCompanyTelegramBot.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public string Value { get; set; }

        [JsonConstructor]
        public Contact(int id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
