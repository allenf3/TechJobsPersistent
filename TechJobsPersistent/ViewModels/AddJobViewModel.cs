using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        public AddJobViewModel() { }
        public AddJobViewModel(List<Employer> employers, List<Skill> skills)
        {
            populateSelectListItems(employers, skills);
        }

        public void populateSelectListItems(List<Employer> employers, List<Skill> skills)
        {
            Employers = new List<SelectListItem>();
            foreach (var employer in employers)
            {
                Employers.Add(new SelectListItem
                {
                    Value = employer.Id.ToString(),
                    Text = employer.Name.ToString()
                });
            }
            Skills = skills;
        }

        [Required]
        [StringLength(120, MinimumLength = 3)]
        public string Name { get; set; }
        public int EmployerId { get; set; }
        public List<SelectListItem> Employers { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
