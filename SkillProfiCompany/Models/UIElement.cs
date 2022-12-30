using SkillProfiCompany.DatabaseContext;
using System.Collections.Generic;
using System.Linq;

namespace SkillProfiCompany.Models
{
    public class UIElement
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Page { get; set; }

        public UIElement(string key, string value, string page)
        {
            Key = key;
            Value = value;
            Page = page;
        }
    }
}
