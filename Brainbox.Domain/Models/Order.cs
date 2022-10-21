using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainbox.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public Customer User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
