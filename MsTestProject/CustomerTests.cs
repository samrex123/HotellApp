using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSite1.Controllers;
using HotelSite1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MsTestProject
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public async Task ReturnIndexView_ShouldWork()
        {
            HotellAppContext _context = new HotellAppContext();
            CustomersController controller = new CustomersController(_context);

            var customers = await _context.Customers.Include(c => c.Customertypes).ToListAsync();
            var result = await controller.Index() as ViewResult;

            Assert.AreEqual(customers.ToString(), result.Model.ToString());
        }
    }
}