using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillProfiCompany.DatabaseContext;

namespace SkillProfiCompany.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Value { get; set; }
        public TagContact Tag { get; set; }


        public enum TagContact
        {
            Address,
            Telephone,
            Email,
            Social
        }


        public Contact()
        {

        }

        public Contact(string header, string value, TagContact tag)
        {
            Header = header;
            Value = value;
            Tag = tag;
        }
    }
}
