using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {

        private readonly IProducersService _service;

        public ProducersController(IProducersService service)
        {
            _service = service;
        }


        public async Task<IActionResult> Index()
        {

            var data = await _service.GetAllAsync();
            return View(data);
        }


        public IActionResult Create()
        {

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.AddAsync(producer);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {

            var ProducerDetails = await _service.GetByIdAsync(id);

            if (ProducerDetails == null)
            {

                return View("View");

            }


            return View(ProducerDetails);



        }


        public async Task<IActionResult> Edit(int id)
        {

            var ProducerDetails = await _service.GetByIdAsync(id);

            if (ProducerDetails == null)
            {

                return View("Not found");

            }


            return View(ProducerDetails);

        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Delete(int id)
        {

            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
            {

                return View("Not found");

            }


            return View(producerDetails);

        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
            {

                return View("Not found");

            }
            else
            {

                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));

            }



        }


    }
}
