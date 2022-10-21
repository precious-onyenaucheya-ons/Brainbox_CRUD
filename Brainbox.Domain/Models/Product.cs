using Brainbox.Domain.enums;
using Brainbox.Domain.Interface;

namespace Brainbox.Domain.Models
{
    public class Product: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
