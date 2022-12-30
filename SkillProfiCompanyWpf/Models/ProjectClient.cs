using Newtonsoft.Json;
using SkillProfiCompanyWpf.Interface;
using SkillProfiCompanyWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SkillProfiCompanyWpf.Models
{
    public class ProjectClient
    {
        public string Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
        public string Img { get; set; }

        [JsonConstructor]
        public ProjectClient(string id, string header, string description, string imageProject)
        {
            Id = id;
            Header = header;
            Description = description;
            Img = imageProject;
            Image = ImageHelper.ConvertFromBase64ToBitmapImage(imageProject);
        }
    }
}
