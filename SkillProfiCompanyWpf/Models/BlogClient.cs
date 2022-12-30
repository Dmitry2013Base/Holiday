using Newtonsoft.Json;
using SkillProfiCompanyWpf.Interface;
using SkillProfiCompanyWpf.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SkillProfiCompanyWpf
{
    public class BlogClient
    {
        private string _description;

        public string Id { get; set; }
        public string Header { get; set; }
        public string Description { get => _description; set => _description = value; }
        public string ShortDescription 
        {
            get
            {
                int count = 200;
                if (_description.Length > count)
                {
                    return _description.Remove(count).Insert(count, "...");
                }
                return _description;
            }
        }
        public string Date { get; }
        public BitmapImage Image { get; set; }
        public string Img { get; set; }


        [JsonConstructor]
        public BlogClient(string id, string header, string description, string date, string imageBlog)
        {
            Id = id;
            Header = header;
            _description = description;
            Date = date;
            Image = ImageHelper.ConvertFromBase64ToBitmapImage(imageBlog);
            Img = imageBlog;  
        }
    }
}
