using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        [Range(0, double.MaxValue)]
        public double TotalAmount { get; set; }
    }
}
