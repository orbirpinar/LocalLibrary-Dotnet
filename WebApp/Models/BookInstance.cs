using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class BookInstance
    {
        public Guid Id { get; set; }
        
        [Column(TypeName = "Date")]
        public DateTime? DueBack { get; set; }
        public LoanStatus LoanStatus { get; set; }
        public Book? Book { get; set; }
        public string? Imprint { get; set; }
        
        public string? BorrowerId { get; set; }
        
        public User? Borrower { get; set; }
    }

    public enum LoanStatus
    {
        Maintenance,
        OnLoan,
        Available,
        Reserved
    }
}
