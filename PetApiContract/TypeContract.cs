using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validations;

namespace PetApiContract
{
    public class TypeContract : ValidationBase
    {
        public TypeContract()
        {}

        public TypeContract(Guid uniqueId, string name, List<string> errors)
        {
            UniqueId = uniqueId;
            Name = name;
            Errors = errors;
        }

        public Guid UniqueId { get; set; }
        public string Name { get; set; }
    }
}
