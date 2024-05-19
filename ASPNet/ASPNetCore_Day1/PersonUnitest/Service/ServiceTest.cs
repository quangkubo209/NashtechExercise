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
using ASPNet_Day1.Models.DTOs;

namespace PersonLogicTest
{
    public class ServiceTest
    {
        Mock<IPersonRepository> mockRepository;
        IMapper mapper;

        [OneTimeSetUp]
        public void Setup()
        {
            mockRepository = new Mock<IPersonRepository>();
            var profile = new AutoMapperPerson();
            var mapperConfig = new MapperConfiguration(c => c.AddProfile(profile));
            mapper = new Mapper(mapperConfig);
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
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

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
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            var fullNames = personService.GetFullnameListPersons();

            // Assert
            Assert.AreEqual(3, fullNames.Count());
            Assert.Contains("Nguyen Tuan", fullNames.ToList(), "The list should contain 'Nguyen Tuan'.");
            Assert.Contains("Nguyen Quang", fullNames.ToList(), "The list should contain 'Nguyen Quang'.");
            Assert.Contains("Nguyen Thanh", fullNames.ToList(), "The list should contain 'Nguyen Thanh'.");
        }

        [Test]
        public void GetTheOldest_ReturnPersonModel()
        {
            // Arrange
            mockRepository.Setup(m => m.GetOldestPerson())
                .Returns(new Person
                {
                    Id = 15,
                    FirstName = "Quang",
                    LastName = "Nguyen",
                    Gender = GenderEnum.Male,
                    DateOfBirth = new DateTime(2002, 09, 08),
                    PhoneNumber = "090909090",
                    BirthPlace = "Phu Tho",
                    IsGraduated = false
                });
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            var oldest = personService.GetOldestPerson();

            // Assert
            Assert.NotNull(oldest);
            Assert.IsInstanceOf<Person>(oldest);
        }

        [Test]
        public void AddPerson_ValidRequest_ReturnSuccess()
        {
            //Arrange
            var person = new Person
            {
                Id = 15,
                FirstName = "Quang",
                LastName = "Nguyen",
                Gender = GenderEnum.Male,
                DateOfBirth = new DateTime(2002, 09, 08),
                PhoneNumber = "090909090",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            };

            var personViewModel = mapper.Map<PersonViewModel>(person);

            mockRepository.Setup(m => m.AddPerson(It.IsAny<Person>())).Returns(ConstantsStatus.Success);
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            int status = personService.AddPerson(personViewModel);

            // Assert
            Assert.That(status, Is.EqualTo(ConstantsStatus.Success));
        }
        [Test]
        public void AddPerson_InvalidRequest_ReturnFailed()
        {
            //Arrange
            var person = new Person
            {
                Id = 10,
                LastName = "Nguyen",
                Gender = GenderEnum.Male,
                DateOfBirth = new DateTime(2002, 09, 08),
                PhoneNumber = "090909090",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            };

            var personViewModel = mapper.Map<PersonViewModel>(person);

            mockRepository.Setup(m => m.AddPerson(person));
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            var status = personService.AddPerson(personViewModel);


            // Assert
            Assert.That(status, Is.EqualTo(ConstantsStatus.Failed));
        }

        [Test]
        public void UpdatePerson_ValidRequest_ReturnSuccess()
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

            var personViewModel = mapper.Map<PersonViewModel>(person);

            mockRepository.Setup(m => m.UpdatePerson(It.IsAny<Person>()))
                          .Returns(ConstantsStatus.Success);
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            var status = personService.UpdatePerson( personViewModel);


            // Assert
            Assert.That(status, Is.EqualTo(ConstantsStatus.Success));
        }
        [Test]
        public void UpdatePeson_InValidRequest_ReturnFailed()
        {
            //Arrange
            var person = new Person
            {
                Id = 10,
                FirstName = "Quang",
                Gender = GenderEnum.Male,
                DateOfBirth = new DateTime(2002, 09, 08),
                PhoneNumber = "090909090",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            };

            var personViewModel = mapper.Map<PersonViewModel>(person);

            mockRepository.Setup(m => m.UpdatePerson(It.IsAny<Person>()))
                          .Returns(ConstantsStatus.Success);
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            var status = personService.UpdatePerson( personViewModel);


            // Assert
            Assert.AreEqual(ConstantsStatus.Failed, status);
        }
        [Test]
        public void DeleteById_Invalid_returnSuccess()
        {
            //Arrange
            int id = 1;
            mockRepository.Setup(m => m.DeleteConfirmed(id))
                          .Returns(ConstantsStatus.Success);
            IPersonService personBusinessLogic = new PersonService(mockRepository.Object, mapper);

            // Act
            var status = personBusinessLogic.DeleteConfirmed(id);


            // Assert
            Assert.That(status, Is.EqualTo(ConstantsStatus.Success));
        }


        [Test]
        public void GetPersonById_Invalid_ReturnNull()
        {
            //Arrange
            int id = 2;

            mockRepository.Setup(m => m.GetPersonById(id))
                          .Returns(new Person { Id = id });
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            var person = personService.GetPersonById(id);

            // Assert
            Assert.IsInstanceOf<PersonViewModel?>(person);
        }

        [Test]
        public void GetListPerson_Whencall_ReturnListPerson()
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
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            var people = personService.GetListPersons();

            // Assert
            Assert.That(people.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetPersonBaseOnYear_CallFilter_ReturnListYearGreater2000()
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
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            Func<Person, bool> choice = s => s.DateOfBirth.Year > 2000;
            var people = personService.GetPersonBaseOnYear(choice);


            // Assert
            Assert.AreEqual(2, people.Count());
            foreach (var person in people)
            {
                Assert.IsTrue(person.DateOfBirth.Year > 2000);
            }
        }

        [Test]
        public void GetPersonBaseOnYear_CallFilter_ReturnListYearEqual2000()
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
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            Func<Person, bool> choice = s => s.DateOfBirth.Year > 2000;
            var people = personService.GetPersonBaseOnYear(choice);


            // Assert
            Assert.AreEqual(2, people.Count());
            foreach (var person in people)
            {
                Assert.IsTrue(person.DateOfBirth.Year == 2000);
            }
        }

        [Test]
        public void GetPersonBaseOnYear_CallFilter_ReturnListYearLess2000()
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
            IPersonService personService = new PersonService(mockRepository.Object, mapper);

            // Act
            Func<Person, bool> choice = s => s.DateOfBirth.Year > 2000;
            var people = personService.GetPersonBaseOnYear(choice);


            // Assert
            Assert.AreEqual(2, people.Count());
            foreach (var person in people)
            {
                Assert.IsTrue(person.DateOfBirth.Year < 2000);
            }

        }

    }
}
