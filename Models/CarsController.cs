using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalApiHNV.Data;

namespace CarRentalApiHNV.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarRentalApiHNVContext _context;

        public CarsController(CarRentalApiHNVContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            return await _context.Car.ToListAsync();
        }

        // GET: api/Cars?rented
        [HttpGet("Rental")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar(bool rented)
        {
            //return await _context.Car.ToListAsync();
            var CarList = await _context.Car.ToListAsync();
            var CarsRentedOrNot = new List<Car>();

            foreach (Car theCar in CarList)
            {
                if (rented)
                {
                    if (theCar.Active)
                    {
                        CarsRentedOrNot.Add(theCar);
                    }
                }
                else
                {
                    if (!theCar.Active)
                    {
                        CarsRentedOrNot.Add(theCar);
                    }
                }
            }
            return CarsRentedOrNot;
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //PUT: api/Cars/5/Rental
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/Rental")]
        public async Task<IActionResult> PutCarRental(int id, Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            var CarList = await _context.Car.ToListAsync();
            Car moddedCar = CarList.Find(x => x.CarId == id);

            if (moddedCar.Active == true)
            {
                return BadRequest("The car is already rented you twit!");
            }

            moddedCar.Active = true;
            car = moddedCar;

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //PUT: api/Cars/5/Returnal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/Returnal")]
        public async Task<IActionResult> PutCarReturnal(int id, Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            var CarList = await _context.Car.ToListAsync();
            Car moddedCar = CarList.Find(x => x.CarId == id);

            if (moddedCar.Active == false)
            {
                return BadRequest("The car cannot be returned since it ain't rented you clown!");
            }

            moddedCar.Active = false;
            car = moddedCar;

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/Cars
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.CarId }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.CarId == id);
        }
    }
}
