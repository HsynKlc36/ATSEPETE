using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.CategoryVMs
{
    public class AdminCategoryUpdateVM
    {
        [DisplayName("Kategori Id")]
        public Guid Id { get; set; }
        [DisplayName("Kategori Adı")]
        public string Name { get; set; }
        [DisplayName("Kategori Açıklaması")]
        public string Description { get; set; }
 
    }
}
