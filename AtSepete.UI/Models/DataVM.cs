using AtSepete.Dtos.Dto.Users;

namespace AtSepete.UI.Models
{
    public class DataVM<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
