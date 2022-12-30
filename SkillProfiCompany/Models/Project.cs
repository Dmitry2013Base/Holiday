using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Interfaces;

namespace SkillProfiCompany.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string ImageProject { get; set; }

        public Project()
        {

        }

        [JsonConstructor]
        public Project(string header, string description, string image)
        {
            Header = header;
            Description = description;
            ImageProject = image;   
        }
    }
}
