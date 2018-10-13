namespace PersonManagement.Services.Models.Common
{
    public class BaseResponseDataModel<TModel> : BaseResponseModel
    {
        public TModel Data { get; set; }

        public BaseResponseDataModel()
        {
        }

        public BaseResponseDataModel(bool isSuccess) : base(isSuccess)
        {

        }

        public BaseResponseDataModel(bool isSuccess, TModel data, string message = null) : base(isSuccess, message)
        {
            Data = data;
        }
    }
}