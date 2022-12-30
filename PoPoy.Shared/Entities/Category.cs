using PoPoy.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoPoy.Shared.Dto
{
    public class Category
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public Status Status { get; set; } = Status.Active;
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public List<ProductInCategory> ProductInCategories { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
