using Microsoft.AspNetCore.Mvc;
using RandomWordService;
using RandomWordWeb.Models;

namespace RandomWordWeb.Controllers
{
    public class TextListController : Controller
    {


        private readonly TextContext _context;
        

        public TextListController( TextContext context)
        {
           
            _context= context;
           
        }


        [HttpGet]
        public  IActionResult Get(TextViewModel model)
        {


            model.Texts = _context.Texts.ToList();
            return View(model);

          
        }
    }
}
