namespace PersonManagement.Services.Models.Common
{
    public class BaseResponseModel
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }

        public BaseResponseModel()
        {
        }

        public BaseResponseModel(bool isSuccess, string message = null)
        {
            this.IsSuccess = isSuccess;
            this.ErrorMessage = message;
        }
    }
}