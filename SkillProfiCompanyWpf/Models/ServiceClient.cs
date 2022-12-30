using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiCompanyWpf
{
    public class ServiceClient
    {
        public string Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }


        [JsonConstructor]
        public ServiceClient(string id, string header, string description)
        {
            Id = id;
            Header = header;
            Description = description;
        }
    }
}
