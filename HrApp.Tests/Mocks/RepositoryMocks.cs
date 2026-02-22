using Bogus;
using HrApp.Models;
using HrApp.Repositories;
using Moq;

namespace HrApp.Tests.Mocks
{
    public static class RepositoryMocks
    {
        public static Mock<IEmployeeRepository> GetEmployeeRepository(int numberOfEmployees)
        {
            int fakeId = 0;
            var faker = new Faker<Employee>()
                .StrictMode(true)
                .RuleFor(e => e.EmployeeId, f => fakeId++)
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName());


            var employees = new List<Employee>();
            for(int i = 0; i<numberOfEmployees; i++)
            {
                employees.Add(faker.Generate());
            }

            var mock = new Mock<IEmployeeRepository>();

            mock.Setup(repo => repo.GetAll()).ReturnsAsync(employees);

            return mock;
        }
    }
}
