using HrApp.Controllers;
using HrApp.Models;
using HrApp.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace HrApp.Tests.Controllers
{
    public class EmployeeControllerTest
    {
        [Fact]
        public async void Index_ReturnsAllEmployees()
        {
            //arrange
            int numberOfEmployees = 3;
            var mockPieRepository = RepositoryMocks.GetEmployeeRepository(numberOfEmployees);

            var employeeController = new EmployeeController(mockPieRepository.Object);

            //act
            var result = await employeeController.Index();

            //assert
            var viewResult = Assert.IsType<ViewResult>(result); //The returning type is a ViewResult
            var listOfEmployees = Assert.IsAssignableFrom<IEnumerable<Employee>>(viewResult.ViewData.Model); //The model returned is a list of employees
            Assert.Equal(numberOfEmployees, listOfEmployees.Count()); //All employees are returned 
        }
    }
}
