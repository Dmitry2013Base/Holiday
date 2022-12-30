using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SkillProfiCompanyTelegramBot.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }

        [JsonConstructor]
        public Project(int id, string header, string imageProject)
        {
            Id = id;
            Header = header;
            Image = imageProject;
         }
    }
}
