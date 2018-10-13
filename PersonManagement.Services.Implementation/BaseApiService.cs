using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonManagement.Services.Abstraction;
using PersonManagement.Services.Models.Common;

namespace PersonManagement.Services.Implementation
{
    public class BaseApiService : IBaseApiService
    {
        protected readonly ILogger<BaseApiService> Logger;
        protected readonly IMapper Mapper;

        public BaseApiService(IMapper mapper,
                              ILogger<BaseApiService> logger)
        {
            Logger = logger;
            Mapper = mapper;
        }

        protected TResp ProcessRequest<TResp>(Func<TResp> func) where TResp : BaseResponseModel, new()
        {
            try
            {
                var response = func();
                return response;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, exception.Message);

                var response = new TResp
                {
                    IsSuccess = false,
                    ErrorMessage = exception.Message
                };
                return response;
            }
        }

        protected async Task<TResp> ProcessRequestAsync<TResp>(Func<Task<TResp>> func) where TResp : BaseResponseModel, new()
        {
            try
            {
                var response = await func();
                return response;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, exception.Message);

                var response = new TResp
                {
                    IsSuccess = false,
                    ErrorMessage = exception.Message
                };
                return response;
            }
        }
    }
}