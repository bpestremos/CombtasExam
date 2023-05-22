using CombtasExam.DTOs;

namespace CombtasExam.Services.Contracts
{
    public interface IInterfaceApiService
    {
        Task<ValidationError> ValidateDTO(InterfaceModel parameter);
    }
}
