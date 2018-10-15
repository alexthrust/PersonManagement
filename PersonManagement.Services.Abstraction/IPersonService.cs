using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonManagement.Services.Models;
using PersonManagement.Services.Models.Common;

namespace PersonManagement.Services.Abstraction
{
    public interface IPersonService : IBaseApiService
    {
        Task<BaseResponseCustomStoreModel<PersonModel>> GetPersonRecords(CustomStoreRequestModel request);
        Task<BaseResponseDataModel<PersonModel>> GetPerson(int personId);
        Task<BaseResponseModel> CreatePerson(PersonModel person);
        Task<BaseResponseModel> UpdatePerson(int personId, PersonModel person);
        Task<BaseResponseModel> DeletePerson(int personId);

        BaseResponseDataModel<bool> IsPersonalNumberUnique(string personalNumber, int? personId);
    }
}