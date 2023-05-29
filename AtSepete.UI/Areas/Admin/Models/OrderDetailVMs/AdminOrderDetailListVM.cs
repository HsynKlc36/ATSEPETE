namespace AtSepete.UI.Areas.Admin.Models.OrderDetailVMs
{
    public class AdminOrderDetailListVM
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        //public DateTime? DeletedDate { get; set; }
        //public bool IsActive { get; set; }
    }
}
