using CustomerCrubOperationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCrubOperationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        AppDBContext _db;
        public CustomerController(AppDBContext db)
        {
            _db = db;

        }


        [HttpGet("{id}")]
        public IActionResult Details(int id)
        { 
            try {
                var custome = _db.Customers.Find(id);
                if (custome == null)
                {
                    return NotFound($"Customer details not found with id{id}");
                }
                return Ok(custome);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }



       }
        [HttpPost]
        public IActionResult Create(Customer model)
        {
            try
            {
                _db.Add(model);
                _db.SaveChanges();
                return Ok("Customer Details created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Update(Customer model)
        {
            try
            {
                if (model == null || model.Id == 0)
                {
                    if (model == null)
                    {
                        return BadRequest("Customer data is invaid");
                    }
                    else if (model.Id == 0)

                    {
                        return BadRequest("Customer id  is invalid");
                    }
                }
                var customer = _db.Customers.Find(model.Id);
                if (customer == null)
                {
                    return NotFound($"Customer Not found with id {model.Id}");
                }
                customer.Name = model.Name;
                customer.Age = model.Age;
                customer.Gender = model.Gender;
                customer.MobileNumber = model.MobileNumber;
                _db.SaveChanges();
                return Ok("Customer details Update successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete] public IActionResult Delete(int id)
        {
            try
            {
                var customer = _db.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound($"Customer not found with id {id}");
                }
                _db.Customers.Remove(customer);
                _db.SaveChanges();

                return Ok("Customer details delete successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    } 


}

