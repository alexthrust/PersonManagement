using System.Collections.Generic;

namespace PersonManagement.Services.Models.Common
{
    public class BaseResponseCustomStoreModel<TModel> : BaseResponseModel
    {
        public CustomStoreDataModel<TModel> Data { get; set; }

        public BaseResponseCustomStoreModel() { }

        public BaseResponseCustomStoreModel(bool isSuccess) : base(isSuccess)
        {
        }

        public BaseResponseCustomStoreModel(List<TModel> data, int totalItems) : base(true)
        {
            Data = new CustomStoreDataModel<TModel>
            {
                Items = data,
                TotalItems = totalItems
            };
        }
    }
}