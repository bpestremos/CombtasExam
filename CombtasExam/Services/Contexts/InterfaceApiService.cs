using CombtasExam.DTOs;
using CombtasExam.Services.Contracts;

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
                result.Messages.Add(ValidateDate(parameter.Date));
                result.Messages.Add(ValidateAmount(parameter.Amount));

                result.Decision = (result.Messages.Count(t => t.Contains("Good")) == 4) ? "OK!" : "FAIL!";
                RemoveSuccessValidation(result);
            }
            catch (InvalidOperationException ex)
            {
                result.Decision = "FAIL!";
                RemoveSuccessValidation(result);
                result.Messages.Add("Date/Amount is mandatory!");
            }

            return result;
        }

        public string ValidateExpense(string expense)
        {
            return !string.IsNullOrEmpty(expense) ? "Expense Validation : Good" : "Expense Validation : Expense is Mandatory!";
        }

        public string ValidateAmount(decimal? amt)
        {
            if (amt == null)
                return "Amount Validation : Amount is mandatory!";

            return amt.HasValue && amt > 0M ? "Amount Validation : Good" : "Amount Validation : Amount must be greater than 0!";
        }

        public string ValidateDate(DateTime? date)
        {
            if (date == null)
                return "Date Validation : Date is mandatory!";

            if (date.HasValue && date <= DateTime.Now.Date)
                return "Date Validation : Date should be at least ONE year from today!";

            return "Date Validation : Good";

        }

        public string ValidateOCC(string occ)
        {
            string[] allowed = new string[] { "USD", "EUR", "GBP", "PHP" };
            if (string.IsNullOrEmpty(occ))
                return "OCC Validation : OCC is mandatory!";

            if (!allowed.Contains(occ.ToUpper()))
                return "OCC Validation : Unknown OCC!";

            return "OCC Validation : Good";
        }

        public void RemoveSuccessValidation(ValidationError error)
        {
            error.Messages.RemoveAll(t => t.Contains("Good"));
        }
    }
}
