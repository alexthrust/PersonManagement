using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonManagement.Constants;
using PersonManagement.Data;
using PersonManagement.Data.DbModels;
using PersonManagement.Extensions;
using PersonManagement.Services.Abstraction;
using PersonManagement.Services.Models;
using PersonManagement.Services.Models.Common;
using PersonManagement.Utils;

namespace PersonManagement.Services.Implementation
{
    public class PersonService : BaseApiService, IPersonService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PersonService(IMapper mapper,
                             ILogger<BaseApiService> logger,
                             ApplicationDbContext applicationDbContext) : base(mapper, logger)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<BaseResponseCustomStoreModel<PersonModel>> GetPersonRecords(CustomStoreRequestModel request)
        {
            var response = await ProcessRequestAsync(async () =>
            {
                var count = await _applicationDbContext.Persons.CountAsync();
                var dbPersons = GetFilteredPersonRecords(request);
                var dbItems = await dbPersons.Skip(request.Skip).Take(request.Take).ToListAsync();
                var items = Mapper.Map<List<PersonModel>>(dbItems);

                return new BaseResponseCustomStoreModel<PersonModel>(items, count);
            });

            return response;
        }

        public async Task<BaseResponseDataModel<PersonModel>> GetPerson(int personId)
        {
            var response = await ProcessRequestAsync(async () =>
            {
                var dbPerson = await _applicationDbContext.Persons.FirstOrDefaultAsync(per => per.Id == personId);

                if (dbPerson == null)
                    throw new Exception($"Can't find person with Id: {personId}");

                var person = Mapper.Map<PersonModel>(dbPerson);
                return new BaseResponseDataModel<PersonModel>(true, person);
            });

            return response;
        }

        public async Task<BaseResponseModel> CreatePerson(PersonModel person)
        {
            var response = await ProcessRequestAsync(async () =>
            {
                var dbPerson = Mapper.Map<Person>(person);
                _applicationDbContext.Persons.Add(dbPerson);
                await _applicationDbContext.SaveChangesAsync();

                return new BaseResponseModel(true);
            });

            return response;
        }

        public async Task<BaseResponseModel> UpdatePerson(int personId, PersonModel person)
        {
            var response = await ProcessRequestAsync(async () =>
            {
                var dbPerson = await _applicationDbContext.Persons.AsNoTracking().FirstOrDefaultAsync(per => per.Id == personId);

                if (dbPerson == null)
                    throw new Exception($"Can't find person with Id: {personId}");

                var dbPersonMapped = Mapper.Map<Person>(person);
                _applicationDbContext.Entry(dbPersonMapped).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();

                return new BaseResponseModel(true);
            });

            return response;
        }

        public async Task<BaseResponseModel> DeletePerson(int personId)
        {
            var response = await ProcessRequestAsync(async () =>
            {
                var dbPerson = await _applicationDbContext.Persons.FirstOrDefaultAsync(per => per.Id == personId);

                if (dbPerson == null)
                    throw new Exception($"Can't find person with Id: {personId}");

                _applicationDbContext.Persons.Remove(dbPerson);
                await _applicationDbContext.SaveChangesAsync();

                return new BaseResponseModel(true);
            });

            return response;
        }

        public BaseResponseDataModel<bool> IsPersonalNumberUnique(string personalNumber)
        {
            var response = ProcessRequest(() =>
            {
                var dbPerson = _applicationDbContext.Persons.FirstOrDefault(per => per.PersonalNumber == personalNumber);

                if (dbPerson == null)
                    return new BaseResponseDataModel<bool>(true, true);

                return new BaseResponseDataModel<bool>(true, false);
            });

            return response;
        }

        private IQueryable<Person> GetFilteredPersonRecords(CustomStoreRequestModel request)
        {
            var dbPersons = _applicationDbContext.Persons.AsQueryable();
            if (!dbPersons.Any()) return dbPersons;

            if (!string.IsNullOrEmpty(request.FilterValue))
            {
                var filterProperties = typeof(Person).GetProperties().Where(property => !property.Name.Equals("Id"));
                var genderFilterProperties = typeof(Person).GetProperties().Where(property => property.Name.Equals("Gender"));

                dbPersons = dbPersons.Where(person => filterProperties.Any(prop => (GetFormattedStringFromObject(prop.GetValue(person, null)).ToLowerInvariant()).Contains(request.FilterValue.ToLowerInvariant())) ||
                                                      genderFilterProperties.Any(prop => GetFormattedStringFromObject(prop.GetValue(person, null)).ToLowerInvariant().Contains(request.FilterValue.ToLowerInvariant())));
            }

            if (request.Sort != null)
            {
                dbPersons = request.Sort.Desc ? dbPersons.OrderByDescending(request.Sort.Field) : dbPersons.OrderBy(request.Sort.Field);
            }
            else
            {
                dbPersons = dbPersons.OrderByDescending(per => per.LastName);
            }

            return dbPersons;
        }

        private string GetFormattedStringFromObject(object value)
        {
            if (value == null)
                return string.Empty;

            if (value is Enum)
            {
                var enumHelper = new EnumHelper();
                return enumHelper.GetPersonGenderName(value);
            }

            if (value is DateTime dateTime)
                return dateTime.ToString("MM/dd/yyyy");

            return value.ToString();
        }
    }
}