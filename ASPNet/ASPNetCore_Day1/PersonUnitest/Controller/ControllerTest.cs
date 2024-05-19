using ASPNet_Day1.BusinessLogic;
using ASPNet_Day1.Common;
using ASPNet_Day1.Controllers;
using ASPNet_Day1.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControllerTest
{
    public class ControllerTest
    {
        Mock<IPersonService> mockService;
        Mock<ILogger<ASPNet_Day1.Controllers.PersonController>> mockLogger;

        [OneTimeSetUp]
        public void ClassInit()
        {
            mockService = new Mock<IPersonService>();
            mockLogger = new Mock<ILogger<PersonController>>();
        }

        [Test]
        public void Detail_InvalidId_ReturnsRedirectToIndex()
        {
            // Arrange
            PersonViewModel? person = null;
            mockService.Setup(m => m.GetPersonById(1)).Returns(person);
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = personController.Detail(1) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);
        }

        [Test]
        public void CreatePerson_ValidPerson_ReturnsRedirectToIndex()
        {
            // Arrange
            var person = new PersonViewModel
            {
                Id = 1,
                FirstName = "Nguyen",
                LastName = "Quang",
                Gender = GenderEnum.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0909090909",
                BirthPlace = "Ha Noi"
            };
            mockService.Setup(m => m.AddPerson(person)).Returns(ConstantsStatus.Success);
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = personController.CreatePerson(person) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);
        }

        [Test]
        public void Detail_ValidId_ReturnsViewWithPerson()
        {
            // Arrange
            var person = new PersonViewModel
            {
                Id = 1,
                FirstName = "Quang",
                LastName = "Nguyen",
                Gender = GenderEnum.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0909090909",
                BirthPlace = "Ha noi"
            };
            mockService.Setup(m => m.GetPersonById(1)).Returns(person);
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = personController.Detail(1) as ViewResult;

            // Assert
            Assert.That(viewResult.Model, Is.EqualTo(person));
        }

        [Test]
        public void CreatePerson_InvalidPerson_ReturnsViewWithError()
        {
            // Arrange
            var person = new PersonViewModel
            {
                Id = 1,
                LastName = "Quang",
                Gender = GenderEnum.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0909090909",
                BirthPlace = "Ha Noi"
            };
            mockService.Setup(m => m.AddPerson(person)).Returns(ConstantsStatus.Failed);
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = personController.CreatePerson(person) as ViewResult;
            var errorMessage = viewResult.ViewData["errorDetail"] as string;

            // Assert
            Assert.AreEqual("FirstName or LastName is required!", errorMessage);
        }

        [Test]
        public void CreatePerson_ModelStateInvalid_ReturnsViewWithPerson()
        {
            // Arrange
            var person = new PersonViewModel
            {
                Id = 1,
                FirstName = "Nguyen",
                LastName = "Quang",
                Gender = GenderEnum.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0909090909",
                BirthPlace = "Ha Noi"
            };
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = personController.CreatePerson(person) as ViewResult;

            // Assert
            Assert.AreEqual(person, viewResult.Model);
        }

        [Test]
        public void Delete_ValidId_ReturnsRedirectToIndex()
        {
            // Arrange
            int id = 1;
            mockService.Setup(m => m.DeleteConfirmed(id)).Returns(ConstantsStatus.Success);
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = personController.Delete(id) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);
        }

        [Test]
        public void Update_InvalidPerson_ReturnsViewWithPerson()
        {
            // Arrange
            int id = 1;
            PersonViewModel? person = null;
            mockService.Setup(m => m.GetPersonById(id)).Returns(person);
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = personController.UpdatePerson(id, person) as ViewResult;

            // Assert
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }

        [Test]
        public void Update_ValidId_ReturnsViewWithPerson()
        {
            // Arrange
            int id = 1;
            var person = new PersonViewModel
            {
                Id = 1,
                FirstName = "Nguyen",
                LastName = "Quang",
                Gender = GenderEnum.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0909090909",
                BirthPlace = "Ha Noi"
            };
            mockService.Setup(m => m.GetPersonById(id)).Returns(person);
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = personController.Update(id) as ViewResult;

            // Assert
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }

        [Test]
        public void GetMalePersons_ReturnsViewWithListOfPersons()
        {
            // Arrange
            var persons = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    FirstName = "Quang",
                    LastName = "Nguyen",
                    Gender = GenderEnum.Male,
                    DateOfBirth = new DateTime(2002, 09, 08),
                    PhoneNumber = "090909090",
                    BirthPlace = "Phu Tho",
                    IsGraduated = false
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Tuan",
                    LastName = "Nguyen",
                    Gender = GenderEnum.Male,
                    DateOfBirth = new DateTime(2000, 10, 12),
                    PhoneNumber = "0808080808",
                    BirthPlace = "Ha Noi",
                    IsGraduated = true
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Thanh",
                    LastName = "Nguyen",
                    Gender = GenderEnum.Male,
                    DateOfBirth = new DateTime(2002, 01, 04),
                    PhoneNumber = "08808080807",
                    BirthPlace = "Ha Noi",
                    IsGraduated = true
                }
            };
            mockService.Setup(m => m.GetMalePersons()).Returns(persons);
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = personController.GetMalePerson() as ViewResult;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOf<ViewResult>(viewResult);
            Assert.AreEqual(persons, viewResult.Model);
        }

        [Test]
        public void GetOldestPerson_ReturnsViewWithPersonModel()
        {
            // Arrange
            mockService.Setup(m => m.GetOldestPerson())
                .Returns(new Person
                {
                    Id = 1,
                    FirstName = "Nguyen",
                    LastName = "Tuan",
                    Gender = GenderEnum.Male,
                    DateOfBirth = DateTime.Now,
                    PhoneNumber = "0983311009",
                    BirthPlace = "Ha noi",
                });
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = personController.GetOldestPerson() as ViewResult;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }

        [Test]
        public void GetFullnameListPersons_ReturnsViewWithListFullName()
        {
            // Arrange
            mockService.Setup(m => m.GetFullnameListPersons())
                .Returns(new List<string> { "Nguyen Quang", "Nguyen Tuan" });
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = personController.GetListFullname() as ViewResult;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.IsNotNull(viewResult.Model);
            var modelList = viewResult.Model as List<string>;
            Assert.IsNotNull(modelList);
            Assert.AreEqual(2, modelList.Count);
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }

        [Test]
        public void GetPersonBasedOnAge_GreaterThan2000_ReturnOkWithListPerson()
        {
            mockService.Setup(m => m.GetPersonBaseOnYear(It.IsAny<Func<Person, bool>>()))
           .Returns(new List<Person>
           {
                            new Person
                            {
                                Id = 1,
                                FirstName = "Nguyen",
                                LastName = "Quang",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(2002, 11, 09),
                                PhoneNumber = "0909090909",
                                BirthPlace = "Phu Tho",
                                IsGraduated = false
                            },
                            new Person
                            {
                                Id = 2,
                                FirstName = "Nguyen",
                                LastName = "Tuan",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(2004, 08, 08),
                                PhoneNumber = "0909090909",
                                BirthPlace = "Ha Noi",
                                IsGraduated = true
                            }
           });
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = personController.GetPersonBaseOnYear(1) as OkObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(200, actionResult.StatusCode);
            var listPerson = actionResult.Value as List<Person>;
            Assert.IsNotNull(listPerson);
            Assert.AreEqual(2, listPerson.Count);

        }

        [Test]
        public void GetPersonBasedOnAge_Equal2000_ReturnOkWithListPerson()
        {
            mockService.Setup(m => m.GetPersonBaseOnYear(It.IsAny<Func<Person, bool>>()))
           .Returns(new List<Person>
           {                            new Person
                            {
                                Id = 1,
                                FirstName = "Nguyen",
                                LastName = "Quang",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(2000, 11, 09),
                                PhoneNumber = "0909090909",
                                BirthPlace = "Phu Tho",
                                IsGraduated = false
                            },
                            new Person
                            {
                                Id = 2,
                                FirstName = "Nguyen",
                                LastName = "Tuan",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(2000, 08, 08),
                                PhoneNumber = "0909090909",
                                BirthPlace = "Ha Noi",
                                IsGraduated = true
                            }
           });
            var personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = personController.GetPersonBaseOnYear(0) as OkObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(200, actionResult.StatusCode);
            var listPerson = actionResult.Value as List<Person>;
            Assert.IsNotNull(listPerson);
            Assert.AreEqual(2, listPerson.Count);
        }

        [Test]
        public void GetPersonBasedOnAge_LessThan2000_ReturnOkWithListPerson()
        {
            mockService.Setup(m => m.GetPersonBaseOnYear(It.IsAny<Func<Person, bool>>()))
      .Returns(new List<Person>
      {                            new Person
                            {
                                Id = 1,
                                FirstName = "Nguyen",
                                LastName = "Quang",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(1999, 11, 09),
                                PhoneNumber = "0909090909",
                                BirthPlace = "Phu Tho",
                                IsGraduated = false
                            },
                            new Person
                            {
                                Id = 2,
                                FirstName = "Nguyen",
                                LastName = "Tuan",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(1998, 08, 08),
                                PhoneNumber = "0909090909",
                                BirthPlace = "Ha Noi",
                                IsGraduated = true
                            }
      });
            var personController = new ASPNet_Day1.Controllers.PersonController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = personController.GetPersonBaseOnYear(-1) as OkObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(200, actionResult.StatusCode);
            var listPerson = actionResult.Value as List<Person>;
            Assert.IsNotNull(listPerson);
            Assert.AreEqual(2, listPerson.Count);
        }

        [Test]
        public void ExportExcel_ReturnsFileResult()
        {
            // Arrange
            var wb = new XLWorkbook();
            wb.Worksheets.Add("Sheet1");
            wb.Worksheets.Add("Sheet2");
            mockService.Setup(b => b.GetExcelFile()).Returns(wb);
            ASPNet_Day1.Controllers.PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);


            // Act
            var result = PersonController.ExportExcel() as FileResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.ContentType);
        }
    }
}
