using EquipmentBorrowingMonitoringSystem.Models;
using EquipmentBorrowingMonitoringSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentBorrowingMonitoringSystem.Controllers
{
    [Authorize]
    public class EquipmentBorrowingController : Controller
    {
        private readonly EquipmentBorrowingRepository _repository;

        public EquipmentBorrowingController(
            EquipmentBorrowingRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(string? search)
        {
            var borrowings = _repository.Search(search ?? "");

            ViewBag.Search = search;

            return View(borrowings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EquipmentBorrowing borrowing)
        {
            if (!ModelState.IsValid)
            {
                return View(borrowing);
            }

            borrowing.Status = "Borrowed";

            _repository.Add(borrowing);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var borrowing = _repository.GetById(id);

            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            EquipmentBorrowing borrowing)
        {
            if (id != borrowing.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(borrowing);
            }

            _repository.Update(borrowing);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var borrowing = _repository.GetById(id);

            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        [HttpGet]
        public IActionResult Return(int id)
        {
            var borrowing = _repository.GetById(id);

            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Return(
            int id,
            EquipmentBorrowing borrowing)
        {
            var existing = _repository.GetById(id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Status = "Returned";
            existing.ActualReturnDateTime = DateTime.Now;

            return RedirectToAction(nameof(Index));
        }
    }
}