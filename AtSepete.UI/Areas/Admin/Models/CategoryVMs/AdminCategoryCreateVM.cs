using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.CategoryVMs
{
    public class AdminCategoryCreateVM
    {
        [DisplayName("Kategori Adı")]
        public string Name { get; set; }
        [DisplayName("Kategori Açıklaması")]
        public string Description { get; set; }
    }
}
