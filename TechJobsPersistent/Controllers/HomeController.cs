using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            var employerList = context.Employers.ToList();
            var skillList = context.Skills.ToList();
            AddJobViewModel viewModel = new AddJobViewModel(employerList, skillList);
            return View(viewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel viewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Job job = new Job(viewModel.Name)
                {
                    EmployerId = viewModel.EmployerId
                };


                foreach(var selectedSkill in selectedSkills)
                {
                    JobSkill jobSkill = new JobSkill()
                    {
                        Job = job,
                        SkillId = int.Parse(selectedSkill)
                    };
                    context.JobSkills.Add(jobSkill);
                }
                context.Jobs.Add(job);
                context.SaveChanges();
                return Redirect("Index");
            }
            viewModel.populateSelectListItems(context.Employers.ToList(), context.Skills.ToList());
            return View("AddJob", viewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
