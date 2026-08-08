using System.ComponentModel.DataAnnotations;

namespace EquipmentBorrowingMonitoringSystem.Models
{
    public class EquipmentBorrowing
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Transaction Number")]
        public string TransactionNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Borrower Name")]
        public string BorrowerName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Borrower Type")]
        public string BorrowerType { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Student/Employee ID")]
        public string BorrowerId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Department/Course")]
        public string DepartmentOrCourse { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Equipment Name")]
        public string EquipmentName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Equipment Category")]
        public string EquipmentCategory { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Borrow Date & Time")]
        [DataType(DataType.DateTime)]
        public DateTime BorrowDateTime { get; set; }

        [Required]
        [Display(Name = "Expected Return Date")]
        [DataType(DataType.Date)]
        public DateTime ExpectedReturnDate { get; set; }

        [Display(Name = "Actual Return Date & Time")]
        [DataType(DataType.DateTime)]
        public DateTime? ActualReturnDateTime { get; set; }

        [Required]
        public string Status { get; set; } = "Borrowed";

        [Required]
        public string Purpose { get; set; } = string.Empty;

        public string? Notes { get; set; }
    }
}