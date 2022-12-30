using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Interfaces;

namespace SkillProfiCompany.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string ImageBlog { get; set; }

        public Blog()
        {

        }

        [JsonConstructor]
        public Blog(string header, string description, string image)
        {
            Date = DateTime.Today.ToString("d");
            Header = header;
            Description = description;
            ImageBlog = image;
        }
    }
}
