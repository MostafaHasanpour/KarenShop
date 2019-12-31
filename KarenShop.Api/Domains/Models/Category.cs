using System.Collections.Generic;

namespace KarenShop.Api.Domains.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
    }
}
