using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validations
{
    public class ValidationBase
    {
        public List<string> Errors { get; set; }
        public bool IsValid => Errors.Any();

        public ValidationBase()
        {
            Errors = new List<string>();
        }
    }
}
