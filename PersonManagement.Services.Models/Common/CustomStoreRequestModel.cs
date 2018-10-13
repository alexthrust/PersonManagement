namespace PersonManagement.Services.Models.Common
{
    public class CustomStoreRequestModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string FilterValue { get; set; }

        public CustomStoreSortingModel Sort { get; set; }
    }
}