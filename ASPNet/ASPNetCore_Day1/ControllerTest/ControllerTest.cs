using ASPNet_Day1.BusinessLogic;
using ASPNet_Day1.Common;
using ASPNet_Day1.Controllers;
using ASPNet_Day1.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
    public class ControllerTest
    {
        Mock<IPersonService> mockService;
        Mock<ILogger<PersonController>> mockLogger;
        [OneTimeSetUp]
        public void ClassInit()
        {
            mockService = new Mock<IPersonService>();
            mockLogger = new Mock<ILogger<PersonController>>();
        }
        [Test]
        public void GetDetail_ById_RetrieveNull()
        {
            // Arrange
            PersonViewModel? person = null;
            mockService.Setup(m => m.GetPersonById(1)).Returns(person);
            PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            RedirectToActionResult actionResult = (RedirectToActionResult)PersonController.Detail(1);

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);
        }
        [Test]
        public void Add_PersonModel_ReturnIndexAction()
        {
            // Arrange
            PersonViewModel? person = new PersonViewModel
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
            PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = PersonController.CreatePerson(person) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);
        }

        [Test]
        public void GetDetailById_ValidPerson()
        {
            // Arrange
            PersonViewModel? person = new PersonViewModel
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
            PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            ViewResult viewResult = (ViewResult)PersonController.Detail(1);

            // Assert
            Assert.That(viewResult.Model, Is.EqualTo(person));
        }
     
     
        [Test]
        public void Add_PersonModel_ReturnViewWithError()
        {
            // Arrange
            PersonViewModel? person = new PersonViewModel
            {
                Id = 1,
                LastName = "Quang",
                Gender = GenderEnum.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0909090909",
                BirthPlace = "Ha Noi"
            };
            mockService.Setup(m => m.AddPerson(person)).Returns(ConstantsStatus.Failed);
            PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = PersonController.CreatePerson(person) as ViewResult;
            var errorMessage = viewResult.ViewData["errorDetail"] as string;

            // Assert
            Assert.AreEqual("FirstName or LastName is required!", errorMessage);
        }
        [Test]
        public void Add_PersonModel_ReturnViewWithInvalidModel()
        {
            // Arrange
            PersonViewModel? person = new PersonViewModel
            {
                Id = 1,
                FirstName = "Nguyen",
                LastName = "Quang",
                Gender = GenderEnum.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0909090909",
                BirthPlace = "Ha Noi"
            };
            PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = PersonController.CreatePerson(person) as ViewResult;

            // Assert
            Assert.AreEqual(person, viewResult.Model);
        }

        [Test]
        public void Delete_ById_ReturnIndexAction()
        {
            // Arrange
            int id = 1;
            mockService.Setup(m => m.DeleteConfirmed(id)).Returns(ConstantsStatus.Success);
            PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = PersonController.Delete(id) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);
        }
        [Test]
        public void Update_ById_ReturnViewWithPerson()
        {
            // Arrange
            int id = 1;
            PersonViewModel? person = null;
            mockService.Setup(m => m.GetPersonById(id)).Returns(person);
            PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = PersonController.UpdatePerson(id, person) as ViewResult;

            // Assert
            Assert.IsInstanceOf<ViewResult>(viewResult);

        }
        [Test]
        public void Update_ById_ReturnView()
        {
            // Arrange
            int id = 1;
            PersonViewModel? person = new PersonViewModel
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
            PersonController personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = personController.Update(id) as ViewResult;

            // Assert
            Assert.IsInstanceOf<ViewResult>(viewResult);

        }
    

        [Test]
        public void GetMalePerson_ReturnViewWithListPerson()
        {
            // Arrange
            IEnumerable<Person> persons = new List<Person>
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
            mockService.Setup(m => m.GetMalePersons())
            .Returns(persons);
            PersonController personController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = personController.GetMalePerson() as ViewResult;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOf<ViewResult>(viewResult);
            Assert.AreEqual(persons, viewResult.Model);
        }


        [Test]
        public void GetTheOldest_ReturnViewWithPersonModel()
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
            PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = PersonController.GetOldestPerson() as ViewResult;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }

        [Test]
        public void GetFullNameList_ReturnViewWithListFullName()
        {
            // Arrange
            int id = 1;
            mockService.Setup(m => m.GetFullnameListPersons())
                .Returns(new List<string> { "Nguyen Quang", "Nguyen Tuan" });
            PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = PersonController.GetListFullname() as ViewResult;
            // Assert
            Assert.IsNotNull(viewResult);
            Assert.IsNotNull(viewResult.Model);
            var modelList = viewResult.Model as List<string>;
            Assert.IsNotNull(modelList);
            Assert.AreEqual(2, modelList.Count);
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }

        [Test]
        public void GetPeopleBasedOnAge_GreaterThan2000_ReturnOkWithListPerson()
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
        public void GetPeopleBasedOnAge_Equal2000_ReturnOkWithListPerson()
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
        public void GetPeopleBasedOnAge_LessThan2000_ReturnOkWithListPerson()
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
            var personController = new PersonController(mockService.Object, mockLogger.Object);

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
            PersonController PersonController = new PersonController(mockService.Object, mockLogger.Object);


            // Act
            var result = PersonController.ExportExcel() as FileResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.ContentType);
        }
    }
}
