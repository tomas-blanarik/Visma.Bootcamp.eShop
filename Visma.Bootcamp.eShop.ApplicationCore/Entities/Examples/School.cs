using System.Collections.Generic;

namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Examples
{
    internal class School : DomainBase
    {
        public List<Student> Students { get; set; }
    }
}
