using BankTransaction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankTransaction.Controllers.Api
{
    [Route("api/Transactions")]
    [ApiController]
    public class TransactionApiController : ControllerBase
    {
        private readonly TransactionDbContext _context;

        public TransactionApiController(TransactionDbContext context)
        {
            
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Transactions);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        // POST: api/Transactions

        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return CreatedAtAction("Get", new { id = transaction.TransactionId }, transaction);
        }

        // PUT: api/Transactions/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }
            _context.Entry(transaction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return NoContent();
        }
       
    }
}
