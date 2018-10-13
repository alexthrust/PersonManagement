using System.Collections.Generic;

namespace PersonManagement.Services.Models.Common
{
    public class CustomStoreDataModel<TModel>
    {
        public List<TModel> Items { get; set; }
        public int TotalItems { get; set; }
    }
}