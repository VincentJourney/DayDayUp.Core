using System;

namespace Model
{
    public class OrderSheetsRequest
    {
        public DateTime CreatedAtFrom { get; set; }
        public DateTime CreatedAtTo { get; set; }
        public string Status { get; set; }
        public int MaxPerPage { get; set; }
    }
}
