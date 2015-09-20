using Microsoft.AspNet.Mvc;
using RallyNow.Models;

namespace RallyNow.Controllers
{
    [Route("api/moxtra")]
    public class MoxtraController : Controller
    {
        [HttpPost]
        public IActionResult Update([FromBody]SaveMoxtraAccessTokenDto dto)
        {
            return new HttpStatusCodeResult(200);
        }
    }
}