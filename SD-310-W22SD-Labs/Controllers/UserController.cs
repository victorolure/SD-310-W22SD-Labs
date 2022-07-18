using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_310_W22SD_Labs.Models;
using SD_310_W22SD_Labs.Models.ViewModels;

namespace SD_310_W22SD_Labs.Controllers
{
    public class UserController : Controller
    {
        private ZenithRentalsContext _db;
        public UserController(ZenithRentalsContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            CustomerEquipmentSelectViewModel vm = new CustomerEquipmentSelectViewModel(_db.Customers.ToList(), _db.Equipment.ToList());

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateRental(int?customerId, int?equipmentId, int rentalHrs)
        {
            Customer customer = _db.Customers.First(c=> c.Id== customerId);
            Equipment equipment = _db.Equipment.First(c=> c.Id== equipmentId);
            Rental rental = new Rental();
            if(customer.RentalHrs >= rentalHrs)
            {
                if(equipment.Quantity > 1)
                {
                    rental.Customer = customer;
                    rental.Equipment = equipment;
                    rental.IsCurrent = true;
                    rental.RentalDate = DateTime.Now;
                    rental.RentalHrs = rentalHrs;
                    customer.RentalHrs -= rentalHrs;
                    customer.Rentals.Add(rental);
                    equipment.Rentals.Add(rental);
                    equipment.Quantity -= 1;
                    _db.Rentals.Add(rental);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("EquipmentQuantityError");
                }
               
            }
            else
            {
                return RedirectToAction("CustomerHoursError");
            }
            
        }
        public IActionResult Rentals()
        {
            List<Rental> rentals = _db.Rentals.OrderBy(r=> r.Customer.UserName).Include(r=> r.Customer).Include(r=> r.Equipment).ToList();
            return View(rentals);
        }
        public IActionResult Customers()
        {
            List<Customer> customers = _db.Customers.OrderBy(c => c.UserName).ToList();
            return View(customers);
        }

        public IActionResult CustomerHoursError()
        {
            ViewBag.Message = "Sorry, You do not have sufficient rental hours";
            return View();
        }
        public IActionResult EquipmentQuantityError()
        {
            ViewBag.Message = "Sorry, Equipment is not available";
            return View();
        }

        [HttpPost]
        public IActionResult EndRental(int? rentalId)
        {
            Rental rental = _db.Rentals.Include(r=> r.Equipment).First(r => r.Id == rentalId);
            if(rental.IsCurrent == true)
            {
                rental.IsCurrent = false;
                Equipment equipment = rental.Equipment;
                equipment.Quantity += 1;
                _db.SaveChanges();
                return RedirectToAction("Rentals");
            }
            else
            {
                return RedirectToAction("Rentals");
            }
            
        }
    }
    
}
