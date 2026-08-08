using EquipmentBorrowingMonitoringSystem.Models;

namespace EquipmentBorrowingMonitoringSystem.Repositories
{
    public class EquipmentBorrowingRepository
    {
        private static readonly List<EquipmentBorrowing> borrowings = new();

        private static int nextId = 1;

        public List<EquipmentBorrowing> GetAll()
        {
            return borrowings;
        }

        public EquipmentBorrowing? GetById(int id)
        {
            return borrowings.FirstOrDefault(x => x.Id == id);
        }

        public void Add(EquipmentBorrowing borrowing)
        {
            borrowing.Id = nextId++;
            borrowings.Add(borrowing);
        }

        public void Update(EquipmentBorrowing borrowing)
        {
            var existing = GetById(borrowing.Id);

            if (existing != null)
            {
                existing.TransactionNumber = borrowing.TransactionNumber;
                existing.BorrowerName = borrowing.BorrowerName;
                existing.BorrowerType = borrowing.BorrowerType;
                existing.BorrowerId = borrowing.BorrowerId;
                existing.DepartmentOrCourse = borrowing.DepartmentOrCourse;
                existing.EquipmentName = borrowing.EquipmentName;
                existing.EquipmentCategory = borrowing.EquipmentCategory;
                existing.Quantity = borrowing.Quantity;
                existing.BorrowDateTime = borrowing.BorrowDateTime;
                existing.ExpectedReturnDate = borrowing.ExpectedReturnDate;
                existing.Purpose = borrowing.Purpose;
                existing.Notes = borrowing.Notes;
                existing.Status = borrowing.Status;
                existing.ActualReturnDateTime = borrowing.ActualReturnDateTime;
            }
        }

        public void Delete(int id)
        {
            var borrowing = GetById(id);

            if (borrowing != null)
            {
                borrowings.Remove(borrowing);
            }
        }

        public List<EquipmentBorrowing> Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return GetAll();
            }

            return borrowings.Where(x =>
                x.TransactionNumber.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                x.BorrowerName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                x.EquipmentName.Contains(search, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}