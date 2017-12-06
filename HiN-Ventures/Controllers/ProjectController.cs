using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiN_Ventures.Models;
using HiN_Ventures.Models.ProjectViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;

namespace HiN_Ventures.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectRepository _repository;

        public ProjectController(IProjectRepository repository)
        {
            _repository = repository;
        }


        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProjectCreateViewModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(
            [Bind("ProjectTitle, ProjectDescription, Active, Open, Deadline")]
            ProjectCreateViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                Project project = new Project
                {
                    ProjectTitle = viewModel.ProjectTitle,
                    ProjectDescription = viewModel.ProjectDescription,
                    Active = viewModel.Active,
                    Open = viewModel.Open,
                    Deadline = viewModel.Deadline
                };

                await _repository.AddAsync(project, User);
                //TempData["success"] = string.Format("Prosjektet: - {0} - har blitt opprettet", project.ProjectTitle);
                return RedirectToAction("Index", "Home");
            }
            // If we get here, something went wrong
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null)
            {
                return NotFound("Bad parameter");
            }
            bool userIsClient = await _repository.UserIsClientAsync((int)id, User);
            if(!userIsClient)
            {
                return RedirectToAction("Index", "Home");
            } 
            ProjectUpdateViewModel viewModel = await _repository.GetProjectUpdateVMAsync((int)id);
            if(viewModel == null)
            {
                return NotFound("Project was not found");
            }
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(ProjectUpdateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project();
                await _repository.UpdateAsync(project, User);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> GetAll()
        {
            var projects = await _repository.GetAllAsync();
            return View(projects);
        }
    }
}