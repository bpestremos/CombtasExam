using CombtasExam.DTOs;
using CombtasExam.Services.Contracts;
using System.Linq;

namespace CombtasExam.Services.Contexts
{
    public class InterfaceApiService : IInterfaceApiService
    {
        public async Task<ValidationError> ValidateDTO(InterfaceModel parameter)
        {
            var result = new ValidationError();
            try
            {
                result.Messages.Add(ValidateExpense(parameter.Expense));
                result.Messages.Add(ValidateOCC(parameter.OCC));
                result.Messages.Add(ValidateAmount(parameter.Amount.Value));
                result.Messages.Add(ValidateDate(parameter.Date.Value));

                result.Decision = (result.Messages.Count(t => t.Equals("Good")) == 4) ? "OK!" : "FAIL!";
                RemoveSuccessValidation(result);
            }
            catch (Exception)
            {
                result.Decision = "FAIL!";
                RemoveSuccessValidation(result);
                result.Messages.Add("Date/Amount is mandatory!");
            }

            return result;
        }

        public string ValidateExpense(string expense)
        {
            return !string.IsNullOrEmpty(expense) ? "Good" : "Expense is Mandatory!";
        }

        public string ValidateAmount(decimal amt)
        {
            return amt > 0M ? "Good" : "Amount must be greater than 0!";
        }

        public string ValidateDate(DateTime date)
        {
            if (date <= DateTime.Now.Date)
                return "Date should be at least ONE year from today!";

            return "Good";
        }

        public string ValidateOCC(string occ)
        {
            string[] allowed = new string[] { "USD", "EUR", "GBP", "PHP" };
            if (string.IsNullOrEmpty(occ))
                return "OCC is mandatory!";

            if (!allowed.Contains(occ.ToUpper()))
                return "Unknown OCC!";

            return "Good";
        }

        public void RemoveSuccessValidation(ValidationError error)
        {
            error.Messages.RemoveAll(t => t.Contains("Good"));
        }
    }
}
