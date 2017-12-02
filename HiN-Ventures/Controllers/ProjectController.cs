using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiN_Ventures.Models;
using HiN_Ventures.Models.ProjectViewModels;

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
        // Husk authorize (legges til senere pga. testing)
        public async Task<IActionResult> Create(ProjectCreateViewModel vm)
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var projects = await _repository.GetAllAsync();
            return View(projects);
        }
    }
}