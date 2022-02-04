using System.Collections.Generic;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Examples
{
    internal class Car : DomainBase
    {
        public string Model { get; set; }

        public List<CarDriver> Drivers { get; set; }
    }
}
