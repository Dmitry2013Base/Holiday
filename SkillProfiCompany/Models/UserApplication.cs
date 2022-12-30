using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SkillProfiCompany.DatabaseContext;
using SkillProfiCompany.Interfaces;
using SkillProfiCompany.Users;
using static System.Net.Mime.MediaTypeNames;

namespace SkillProfiCompany.Models
{
    public class UserApplication
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserMessage { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public User User { get; set; }


        public UserApplication()
        {

        }

        [JsonConstructor]
        public UserApplication(string userFullName, string userEmail, string userMessage)
        {
            Date = DateTime.Now;
            UserFullName = userFullName;
            UserEmail = userEmail;
            UserMessage = userMessage;
            User = User.GetUser(userFullName);
        }
    }
}

