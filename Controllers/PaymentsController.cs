using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments([FromQuery] int pageNumber, int pageSize)
        {
            var results = _mapper.Map<List<CourseDto>>(await _paymentRepository.Search().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(Guid id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return _mapper.Map<PaymentDto>(payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(Guid id, PaymentDto request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var payment = await _paymentRepository.GetByIdAsync(id);

            payment.TotalAmount = request.TotalAmount;

            if (payment == null)
            {
                return NotFound();
            }

            try
            {
                await _paymentRepository.UpdateAsync(payment);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment(PaymentDto request)
        {
            var payment = _mapper.Map<Payment>(request);

            await _paymentRepository.AddAsync(payment);

            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, _mapper.Map<PaymentDto>(payment));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            await _paymentRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
