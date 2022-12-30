using Newtonsoft.Json;
using System;

namespace SkillProfiCompanyWpf
{
    public class AppClient
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserMessage { get; set; }
        public DateTime Date { get; set; }
        public StatusClient ApplicationStatus { get; set; }

        [JsonConstructor]
        public AppClient(int id, string fullName, string email, string message, StatusClient status)
        {
            Id = id;
            UserFullName = fullName;
            UserEmail = email;
            UserMessage = message;
            Date = DateTime.Now;
            ApplicationStatus = status;
        }
    }
}
