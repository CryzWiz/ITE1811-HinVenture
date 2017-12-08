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


        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Save(
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("Bad parameter");
            }
            bool userIsClient = await _repository.UserIsClientAsync((int)id, User);
            if (!userIsClient)
            {
                //TempData["error"] = "Du har ikke tillatelse til å endre dette prosjektet.";
                return RedirectToAction("Read", "Project", new { id = id });
            }
            ProjectUpdateViewModel viewModel = await _repository.GetProjectUpdateVMAsync((int)id);
            if (viewModel == null)
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
                    //TempData["error"] = "Du har ikke tillatelse til å endre dette prosjektet.";
                    return RedirectToAction("Index", "Project");
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
                return RedirectToAction("Read", "Project", new { id = viewModel.ProjectId });
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
                    //TempData["error"] = "Kunne ikke finne valgt prosjekt";
                    return RedirectToAction("Index", "Project");
                }
                return View(project);
            } catch(Exception ex)
            {
                //TempData["error"] = string.Format("Oops, noe gikk galt under lasting av prosjekt");
                return RedirectToAction("Index", "Project");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound("Bad parameter");
            }
            bool userIsClient = await _repository.UserIsClientAsync((int)id, User);
            if(userIsClient)
            {
                try
                {
                    await _repository.RemoveAsync((int)id, User);
                    //TempData["success"] = string.Format("Prosjekt med id: - {0} - ble slettet", id);
                    return RedirectToAction("Index", "Project");
                }
                catch (Exception ex)
                {
                    //TempData["error"] = string.Format("Oops, noe gikk galt under sletting av prosjektet");
                    return RedirectToAction("Index", "Project");
                }
            }
            //TempData["error"] = "Du har ikke tillatelse til å slette dette prosjektet.";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetAll()
        {
            var projects = await _repository.GetAllAsync();
            return View(projects);
        }
    }
}