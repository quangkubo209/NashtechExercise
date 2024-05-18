using ASPNet_Day1.BusinessLogic;
using ASPNet_Day1.Models.Repositorys;
using ASPNet_Day1.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ASP_Day1.Models.DTOs;
using AutoMapper;
using ASPNet_Day1.Common;

namespace PersonLogicTest
{
    public class PersonBusinessTest
    {
        Mock<IPersonRepository> mockRepository;
        Mock<IMapper> mockMapper;
        [OneTimeSetUp]
        public void Setup()
        {
            mockRepository = new Mock<IPersonRepository>();
            mockMapper = new Mock<IMapper>();
        }

        [Test]
        public void GetExcelFile_ReturnWorkBook()
        {
            // Arrange
            var mockWorkbook = new Mock<XLWorkbook>();
            List<Person> persons = new List<Person>
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
            mockRepository.Setup(m => m.GetExcelFile()).Returns(mockWorkbook.Object);
            IPersonService personService = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            var wb = personService.GetExcelFile();

            // Assert
            Assert.IsInstanceOf<XLWorkbook>(wb);

        }
        [Test]
        public void GetFullNames_ReturnListFullNames()
        {
            // Arrange
            mockRepository.Setup(m => m.GetFullnameListPersons()).Returns(new List<string> { "Nguyen Tuan", "Nguyen Quang", "Nguyen Thanh" });
            IPersonService personService = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            var fullNames = personService.GetFullnameListPersons();

            // Assert
            Assert.AreEqual(3, fullNames.Count());
        }

        [Test]
        public void GetTheOldest_ReturnPersonModel()
        {
            // Arrange
            mockRepository.Setup(m => m.GetOldestPerson())
                .Returns(new Person
                {
                    Id = 1,
                    FirstName = "Quang",
                    LastName = "Nguyen",
                    Gender = GenderEnum.Male,
                    DateOfBirth = new DateTime(2002, 09, 08),
                    PhoneNumber = "090909090",
                    BirthPlace = "Phu Tho",
                    IsGraduated = false
                });
            IPersonService personService = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            var oldest = personService.GetOldestPerson();

            // Assert
            Assert.NotNull(oldest);
        }
        [Test]
        public void Add_Person_ValidRequest()
        {
            //Arrange
            var person = new Person
            {
                Id = 10,
                FirstName = "Quang",
                LastName = "Nguyen",
                Gender = GenderEnum.Male,
                DateOfBirth = new DateTime(2002, 09, 08),
                PhoneNumber = "090909090",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            };

            var expectedPerson = new PersonViewModel()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth,
                PhoneNumber = person.PhoneNumber,
                BirthPlace = person.BirthPlace,
                IsGraduated = person.IsGraduated
            };
            mockMapper.Setup(m => m.Map<PersonViewModel>(person)).Returns(expectedPerson);

            mockRepository.Setup(m => m.AddPerson(person));

            IPersonService personService = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            int status = personService.AddPerson(expectedPerson);


            // Assert
            Assert.That(status, Is.EqualTo(ConstantsStatus.Success));
        }
        [Test]
        public void Add_Person_MissFiledRequest()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
                FirstName = "Quang",
                LastName = "Nguyen",
                Gender = GenderEnum.Male,
                DateOfBirth = new DateTime(2002, 09, 08),
                PhoneNumber = "090909090",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            };
            var expectedPerson = new PersonViewModel()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth,
                PhoneNumber = person.PhoneNumber,
                BirthPlace = person.BirthPlace,
                IsGraduated = person.IsGraduated
            };
            mockMapper.Setup(m => m.Map<PersonViewModel>(person)).Returns(expectedPerson);

            mockRepository.Setup(m => m.AddPerson(person));
            IPersonService personService = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            var status = personService.AddPerson(expectedPerson);


            // Assert
            Assert.That(status, Is.EqualTo(ConstantsStatus.Success));
        }

        [Test]
        public void UpdatePersonFullyFiled()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
                FirstName = "Quang",
                LastName = "Nguyen",
                Gender = GenderEnum.Male,
                DateOfBirth = new DateTime(2002, 09, 08),
                PhoneNumber = "090909090",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            };
            var expectedPerson = new PersonViewModel()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth,
                PhoneNumber = person.PhoneNumber,
                BirthPlace = person.BirthPlace,
                IsGraduated = person.IsGraduated
            };
            mockMapper.Setup(m => m.Map<PersonViewModel>(person)).Returns(expectedPerson);

            mockRepository.Setup(m => m.UpdatePerson( person))
                          .Returns(ConstantsStatus.Success);
            IPersonService personBusinessLogic = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            var status = personBusinessLogic.UpdatePerson( expectedPerson);


            // Assert
            Assert.That(status, Is.EqualTo(ConstantsStatus.Success));
        }
        [Test]
        public void UpdatePersonMissingField()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
                FirstName = "Quang",
                LastName = "Nguyen",
                Gender = GenderEnum.Male,
                DateOfBirth = new DateTime(2002, 09, 08),
                PhoneNumber = "090909090",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            };

            var expectedPerson = new PersonViewModel()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth,
                PhoneNumber = person.PhoneNumber,
                BirthPlace = person.BirthPlace,
                IsGraduated = person.IsGraduated
            };
            mockMapper.Setup(m => m.Map<PersonViewModel>(person)).Returns(expectedPerson);

            mockRepository.Setup(m => m.UpdatePerson( person))
                          .Returns(ConstantsStatus.Failed);
            IPersonService personBusinessLogic = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            var status = personBusinessLogic.UpdatePerson( expectedPerson);


            // Assert
            Assert.AreEqual(ConstantsStatus.Failed, status);
        }
        [Test]
        public void DeleteById_invalid_request()
        {
            //Arrange
            int id = 1;
            mockRepository.Setup(m => m.DeleteConfirmed(id))
                          .Returns(ConstantsStatus.Success);
            IPersonService personBusinessLogic = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            var status = personBusinessLogic.DeleteConfirmed(id);


            // Assert
            Assert.That(status, Is.EqualTo(ConstantsStatus.Success));
        }


        [Test]
        public void GetPersonById_ReturnNull()
        {
            //Arrange
            int id = 2;

            mockRepository.Setup(m => m.GetPersonById(id))
                          .Returns(new Person { Id = id });
            IPersonService personService = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            var person = personService.GetPersonById(id);


            // Assert
            Assert.IsInstanceOf<Person?>(person);
        }

        [Test]
        public void GetListPerson_ReturnList()
        {
            //Arrange

            mockRepository.Setup(m => m.GetPersons())
                       .Returns(new List<Person>
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
                       });
            IPersonService personService = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            var people = personService.GetListPersons();


            // Assert
            Assert.That(people.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetPersonBaseOnYear_ReturnListPerson_YearGreater2000()
        {
            //Arrange
            mockRepository.Setup(m => m.GetPersonBaseOnYear(It.IsAny<Func<Person, bool>>()))
           .Returns(new List<Person>
           {
                            new Person
                            {
                                Id = 1,
                                FirstName = "Nguyen",
                                LastName = "Quang",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(2002, 08, 09),
                                PhoneNumber = "0983327119",
                                BirthPlace = "PHu Tho",
                                IsGraduated = false
                            },
                            new Person
                            {
                                Id = 2,
                                FirstName = "Anh",
                                LastName = "Nguyen",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(2003, 11, 11),
                                PhoneNumber = "0983327119",
                                BirthPlace = "Ha Noi",
                                IsGraduated = true
                            }
           });
            IPersonService personService = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            Func<Person, bool> choice = s => s.DateOfBirth.Year > 2000;
            var people = personService.GetPersonBaseOnYear(choice);


            // Assert
            Assert.AreEqual(2, people.Count());
        }

        [Test]
        public void GetPersonBaseOnYear_ReturnListPerson_YearEqual2000()
        {
            //Arrange
            mockRepository.Setup(m => m.GetPersonBaseOnYear(It.IsAny<Func<Person, bool>>()))
           .Returns(new List<Person>
           {
                            new Person
                            {
                                Id = 1,
                                FirstName = "Nguyen",
                                LastName = "Quang",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(2000, 08, 09),
                                PhoneNumber = "0983327119",
                                BirthPlace = "PHu Tho",
                                IsGraduated = false
                            },
                            new Person
                            {
                                Id = 2,
                                FirstName = "Anh",
                                LastName = "Nguyen",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(2000, 11, 11),
                                PhoneNumber = "0983327119",
                                BirthPlace = "Ha Noi",
                                IsGraduated = true
                            }
           });
            IPersonService personService = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            Func<Person, bool> choice = s => s.DateOfBirth.Year > 2000;
            var people = personService.GetPersonBaseOnYear(choice);


            // Assert
            Assert.AreEqual(2, people.Count());
        }

        [Test]
        public void GetPersonBaseOnYear_ReturnListPerson_YearLess2000()
        {
            //Arrange
            mockRepository.Setup(m => m.GetPersonBaseOnYear(It.IsAny<Func<Person, bool>>()))
           .Returns(new List<Person>
           {
                            new Person
                            {
                                Id = 1,
                                FirstName = "Nguyen",
                                LastName = "Quang",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(1999, 08, 09),
                                PhoneNumber = "0983327119",
                                BirthPlace = "PHu Tho",
                                IsGraduated = false
                            },
                            new Person
                            {
                                Id = 2,
                                FirstName = "Anh",
                                LastName = "Nguyen",
                                Gender = GenderEnum.Male,
                                DateOfBirth = new DateTime(1998, 11, 11),
                                PhoneNumber = "0983327119",
                                BirthPlace = "Ha Noi",
                                IsGraduated = true
                            }
           });
            IPersonService personService = new PersonService(mockRepository.Object, mockMapper.Object);

            // Act
            Func<Person, bool> choice = s => s.DateOfBirth.Year > 2000;
            var people = personService.GetPersonBaseOnYear(choice);


            // Assert
            Assert.AreEqual(2, people.Count());
        }

    }
}
