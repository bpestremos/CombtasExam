using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CombtasExam.DTOs
{
    public class InterfaceModel
    {
        public DateTime? Date { get; set; }

        [MaxLength(100)]
        public string? Expense { get; set; }
        
        public decimal? Amount { get; set; }

        [MaxLength(3)]
        public string? OCC { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }
    }

    public class ValidationError
    {
        public string Decision { get; set; }
        public List<string> Messages { get; set; }

        public ValidationError() 
        {
            Messages = new List<string>();
        }
    }
}