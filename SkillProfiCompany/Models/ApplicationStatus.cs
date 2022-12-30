using Microsoft.EntityFrameworkCore;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SkillProfiCompany.Models
{
    public class ApplicationStatus
    {
        public int Id { get; set; }
        public string StatusName { get; set; }


        public ApplicationStatus()
        {

        }
    }
}
