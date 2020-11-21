using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Diameter { get; set; }
        public double Weight { get; set; }
        public int Stock { get; set; }
        public int Seen { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
