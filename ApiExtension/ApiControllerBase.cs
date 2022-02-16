using Microsoft.AspNetCore.Mvc;
using Validations;

namespace ApiExtension
{
    public class ApiControllerBase : ControllerBase
    {
        public IActionResult Ok<T>(T value) where T : ValidationBase
        {
            if (value == null)
            {
                return NotFound();
            }

            if (value.IsValid)
            {
                return base.Ok(value);
            }
            else
            {
                return BadRequest(value);
            }
        }

        public IActionResult Ok<T>(IEnumerable<T> value)
        {
            if (value == null)
            {
                return NotFound();
            }

            return base.Ok(value);
        }
    }
}