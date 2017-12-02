using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using HiN_Ventures.Models;
using HiN_Ventures.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections;
using HiN_Ventures.Models.ProjectViewModels;

namespace HiN_Ventures_UnitTests
{
    [TestClass]
    public class ProjectControllerTest
    {
        Mock<IProjectRepository> _repository;
        IEnumerable<Project> _fakeProjects;

        [TestInitialize]
        public void Setup()
        {
            _repository = new Mock<IProjectRepository>();
            _fakeProjects = new List<Project>
            {
                new Project { ProjectTitle = "Tittel1", ProjectDescription = "Beskrivelse1" },
                new Project { ProjectTitle = "Tittel2", ProjectDescription = "Beskrivelse2" },
                new Project { ProjectTitle = "Tittel3", ProjectDescription = "Beskrivelse3" },
            };
            
        }


        //[TestMethod]
        public void Create()
        {

        }

        [TestMethod]
        public async Task GetAll_ReturnsAllProjects()
        {
            // Arrange
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(_fakeProjects);
            var controller = new ProjectController(_repository.Object);

            // Act
            var result = await controller.GetAll() as ViewResult;

            // Assert
            Assert.IsNotNull(result, "ViewResult is null");
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Project));
            var projects = result.ViewData.Model as List<Project>;
            Assert.AreEqual(3, projects.Count, "Got wrong number of projects");
            Assert.AreEqual(_fakeProjects, projects);
        }

        public async Task Index_ReturnsAllOpenProjects()
        {

        }

        public async Task GetAllActive_ReturnsAllActiveProjects()
        {

        }

        [TestMethod]
        public void CreateHttpGet_ReturnsProjectCreateViewModel()
        {
            // Arrange
            //var projectCreateVM = new ProjectCreateViewModel();
            var controller = new ProjectController(_repository.Object);
           
            // Act
            var result = (ViewResult)controller.Create();

            // Assert
            Assert.IsNotNull(result, "ViewResult is null");
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            
        }
    }
}
