using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validations;

namespace PetApiContract
{
    public class BreedContract : ValidationBase
    {
        public BreedContract()
        {}

        public BreedContract(Guid uniqueId, string name, Guid typeId, List<string> errors)
        {
            UniqueId = uniqueId;
            Name = name;
            TypeId = typeId;
            Errors = errors;
        }

        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public Guid TypeId { get; set; }
    }
}
