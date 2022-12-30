using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SkillProfiCompany.Controllers;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Interfaces;

namespace SkillProfiCompany.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }

        public Service()
        {

        }

        [JsonConstructor]
        public Service(string header, string description)
        {
            Header = header;
            Description = description;
        }
    }
}
