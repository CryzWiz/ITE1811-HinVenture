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
                // TODO: Finn ut hvor det skal redirectes til
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
                bool userIsClient = await _repository.UserIsClientAsync(viewModel.ProjectId, User);
                if (!userIsClient)
                {
                    // TODO: Finn ut hvor det skal redirectes til
                    return RedirectToAction("Index", "Home");
                }
                Project project = new Project
                {
                    ProjectId = viewModel.ProjectId,
                    ProjectTitle = viewModel.ProjectTitle,
                    ProjectDescription = viewModel.ProjectDescription,
                    Active = viewModel.Active,
                    Open = viewModel.Open,
                    Complete = viewModel.Complete,
                    Deadline = viewModel.Deadline
                }; 
                await _repository.UpdateAsync(project, User);
                return RedirectToAction("Read", "Project");
            }
            // If we get here, something was wrong with the model
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Read(int? id)
        {
            if (id == null)
            {
                return NotFound("Bad parameter");
            }
            try
            {
                ProjectReadViewModel project = await _repository.GetProjectReadVMAsync((int)id);
                if (project == null)
                {
                    TempData["error"] = "Kunne ikke finne valgt prosjekt";
                    return RedirectToAction("Index", "Project");
                }
                return View(project);
            } catch(Exception ex)
            {
                //TempData["error"] = string.Format("Oops, noe gikk galt under lasting av prosjekt");
                return RedirectToAction("Index", "Project");
            }


        }

        public async Task<IActionResult> GetAll()
        {
            var projects = await _repository.GetAllAsync();
            return View(projects);
        }
    }
}