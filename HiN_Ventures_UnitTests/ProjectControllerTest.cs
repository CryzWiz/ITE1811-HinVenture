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
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HiN_Ventures_UnitTests
{
    [TestClass]
    public class ProjectControllerTest
    {
        Mock<IProjectRepository> _repository;
        List<Project> _fakeProjects;
        ProjectCreateViewModel _fakeProjectCreateVM;
        ProjectUpdateViewModel _fakeProjectUpdateVM;
        ProjectReadViewModel _fakeProjectReadVM;


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

            _fakeProjectCreateVM = new ProjectCreateViewModel()
            {
                ProjectTitle = "Title",
                ProjectDescription = "Description",
                Active = true,
                Open = true,
                Deadline = DateTime.Now
            };



            var fakeFreelancers = new List<FreelancerInfo>
            {
                new FreelancerInfo { FirstName = "Ola" },
                new FreelancerInfo { FirstName = "Normann" }

            };

            

            _fakeProjectUpdateVM = new ProjectUpdateViewModel
            {
                ProjectId = 1,
                ProjectTitle = "Title",
                ProjectDescription = "Description",
                Active = true,
                Open = true,
                Complete = false, 
                Deadline = DateTime.Now,
                //Freelancers = fakeFreelancers
            };

            _fakeProjectReadVM = new ProjectReadViewModel()
            {
                ProjectTitle = "Title",
                ProjectDescription = "Description",
                Active = true,
                Open = true,
                Complete = false,
                Deadline = DateTime.Now,
                DateCreated = DateTime.Now,
                Freelancer = fakeFreelancers[0],
                Client = new KlientInfo { CompanyName = "UiT Norges Arktiske Universitet" }
            };

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

        public async Task Index_ReturnsAllOpenAndActiveProjects()
        {

        }

        public async Task GetAllActive_ReturnsAllActiveProjects()
        {

        }

        [TestMethod]
        public async Task CreatePost_AddAsyncIsCalled()
        {
            // Arrange
            var controller = new ProjectController(_repository.Object);

            // Act
            await controller.Create(_fakeProjectCreateVM);

            // Assert
            _repository.Verify(x => x.AddAsync(It.IsAny<Project>(), It.IsAny<IPrincipal>()), Times.Exactly(1));
        }

        [TestMethod]
        public void CreateGet_ReturnsView()
        {
            // Arrange
            var controller = new ProjectController(_repository.Object);
           
            // Act
            var result = (ViewResult)controller.Create();

            // Assert
            Assert.IsNotNull(result, "ViewResult is null");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task UpdateGet_ReturnsNotFoundWhenIdIsNull()
        {
            // Arrange
            var controller = new ProjectController(_repository.Object);

            // Act
            int? id = null;
            var result = await controller.Update(id);

            // Assert
            Assert.IsNotNull(result, "View Result is null");
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task UpdateGet_ReturnsCorrectViewModel()
        {
            // Arrange
            _repository.Setup(x => x.UserIsClientAsync(1, It.IsAny<IPrincipal>())).Returns(Task.FromResult<bool>(true));
            _repository.Setup(x => x.GetProjectUpdateVMAsync(1)).Returns(Task.FromResult<ProjectUpdateViewModel>(new ProjectUpdateViewModel()));
            var controller = new ProjectController(_repository.Object);

            // Act
            var result = await controller.Update(1) as ViewResult;
            
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var projectViewModel = result.ViewData.Model as ProjectUpdateViewModel;
            Assert.IsInstanceOfType(projectViewModel, typeof(ProjectUpdateViewModel));
            _repository.Verify(x => x.GetProjectUpdateVMAsync(1), Times.Exactly(1));
            _repository.VerifyAll();
        }

        [TestMethod]
        public async Task UpdateGet_RedirectsIfUserIsNotClient()
        {
            // Arrange
            _repository.Setup(x => x.UserIsClientAsync(1, It.IsAny<IPrincipal>())).Returns(Task.FromResult<bool>(false));
            var controller = new ProjectController(_repository.Object);

            // Act
            var result = await controller.Update(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "Redirect result is null");
            Assert.AreEqual("Index", result.ActionName as String);
            Assert.AreEqual("Home", result.ControllerName as String);
        }

        [TestMethod]
        public async Task UpdatePost_UpdateAsyncIsCalled()
        {
            // Arrange
            _repository.Setup(x => x.UserIsClientAsync(_fakeProjectUpdateVM.ProjectId, It.IsAny<IPrincipal>())).Returns(Task.FromResult<bool>(true));
            var controller = new ProjectController(_repository.Object);

            // Act
            await controller.Update(_fakeProjectUpdateVM);

            // Assert
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Project>(), It.IsAny<IPrincipal>()), Times.Exactly(1));
        }

        /*[TestMethod]
        public async Task UpdatePost_RedirectsToReadIfSuccesful()
        {
            // Arrange
            _repository.Setup(x => x.UserIsClientAsync(_fakeProjectUpdateVM.ProjectId, It.IsAny<IPrincipal>())).Returns(Task.FromResult<bool>(true));
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Project>(), It.IsAny<IPrincipal>()));
            var controller = new ProjectController(_repository.Object);

            // Act
            var result = await controller.Update(_fakeProjectUpdateVM);
            RedirectToActionResult redirect = result as RedirectToActionResult;
            string resultActionName = redirect.ActionName as String;

            // Assert
            Assert.IsNotNull(result, "Redirect result is null");
           // Assert.AreEqual("Read", result.ActionName as String);
            //Assert.AreEqual("Project", result.ControllerName as String);
           
        }*/

        [TestMethod]
        public async Task UpdatePost_RedirectsIfUserIsNotClient()
        {
            // Arrange
            _repository.Setup(x => x.UserIsClientAsync(_fakeProjectUpdateVM.ProjectId, It.IsAny<IPrincipal>())).Returns(Task.FromResult<bool>(false));
            var controller = new ProjectController(_repository.Object);

            // Act
            var result = await controller.Update(_fakeProjectUpdateVM) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "Redirect result is null");
            Assert.AreEqual("Index", result.ActionName as String);
            Assert.AreEqual("Home", result.ControllerName as String);

        }

        [TestMethod]
        public async Task UpdatePost_ReturnsNotFoundIfViewModelIsNull()
        {
            // Arrange
            _repository.Setup(x => x.UserIsClientAsync(1, It.IsAny<IPrincipal>())).Returns(Task.FromResult<bool>(true));
            _repository.Setup(x => x.GetProjectUpdateVMAsync(1)).Returns(Task.FromResult<ProjectUpdateViewModel>(null));
            var controller = new ProjectController(_repository.Object);

            // Act
            var result = await controller.Update(1);

            // Assert
            Assert.IsNotNull(result, "View Result is null");
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task ReadGet_ReturnsCorrectProject()
        {
            // Arrange
            _repository.Setup(x => x.GetProjectReadVMAsync(1)).Returns(Task.FromResult<ProjectReadViewModel>(_fakeProjectReadVM));
            var controller = new ProjectController(_repository.Object);

            // Act
            var result = await controller.Read(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "View Result is null");
            var project = result.ViewData.Model as ProjectReadViewModel;
            Assert.IsInstanceOfType(project, typeof(ProjectReadViewModel));
            Assert.AreEqual(project, _fakeProjectReadVM);
        }

        [TestMethod]
        public async Task ReadGet_RedirectsIfProjectWasNotFound()
        {
            // Arrange
            _repository.Setup(x => x.GetProjectReadVMAsync(1)).Returns(Task.FromResult<ProjectReadViewModel>(null));
            var controller = new ProjectController(_repository.Object);

            // Act
            var result = await controller.Read(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "Redirect result is null");
            _repository.Verify(x => x.GetProjectReadVMAsync(1), Times.Exactly(1));
            Assert.AreEqual("Index", result.ActionName as String);
            Assert.AreEqual("Project", result.ControllerName as String);
        }

        [TestMethod]
        public async Task ReadGet_RedirectsIfRepositoryThrowsException()
        {
            // Arrange
            _repository.Setup(x => x.GetProjectReadVMAsync(1)).Throws(new Exception());
            var controller = new ProjectController(_repository.Object);

            // Act
            var result = await controller.Read(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "Redirect result is null");
            _repository.Verify(x => x.GetProjectReadVMAsync(1), Times.Exactly(1));
            Assert.AreEqual("Index", result.ActionName as String);
            Assert.AreEqual("Project", result.ControllerName as String);
        }

        [TestMethod]
        public async Task ReadGet_ReturnsNotFoundIfIdIsNull()
        {
            // Arrange
            var controller = new ProjectController(_repository.Object);

            // Act
            int? id = null;
            var result = await controller.Read(id);

            // Assert
            Assert.IsNotNull(result, "View Result is null");
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }





        // TODO: TEST TEMPDATA!!!

        /*[TestMethod]
        public async Task CreatePost_SetsTempDataSuccessBeforeRedirect()
        {
            // Arrange
            var controller = new ProjectController(_repository.Object);
            var tempData = new Mock<TempDataDictionary>();
            controller.TempData = tempData.Object;

            // Act
            await controller.Create(_fakeProjectCreateVM);

            // Assert
            Assert.IsNotNull(controller.TempData);
        }*/


    }
}
