using System;
using System.Collections.Generic;

namespace Model
{
    public class OrderSheets
    {
        /// <summary>
        /// 
        /// </summary>
        public long ShipmentBoxId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Orderer Orderer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PaidAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal ShippingPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal RemotePrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool RemoteArea { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParcelPrintMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool SplitShipping { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool AbleSplitShipping { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Receiver Receiver { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderSheetsItem> OrderItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OverseaShippingInfoDto OverseaShippingInfoDto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeliveryCompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string InvoiceNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? InTrasitDateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeliveredDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Refer { get; set; }
    }

    public class Orderer
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SafeNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrdererNumber { get; set; }
    }

    public class Receiver
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SafeNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiverNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Addr1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Addr2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PostCode { get; set; }
    }

    public class ExtraProperties
    {
        public string OnlineSalePriceForBooks { get; set; }
        public string CoupangSalePrice { get; set; }
    }

    public class OrderSheetsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int VendorItemPackageId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VendorItemPackageName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long VendorItemId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VendorItemName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ShippingCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal DiscountPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal InstantCouponDiscount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal DownloadableCouponDiscount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal CoupangDiscount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ExternalVendorSkuCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EtcInfoHeader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EtcInfoValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> EtcInfoValues { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SellerProductId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SellerProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SellerProductItemName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FirstSellerProductItemName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CancelCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int HoldCountForCancel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EstimatedShippingDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PlannedShippingDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? InvoiceNumberUploadDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ExtraProperties ExtraProperties { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool PricingBadge { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool UsedProduct { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ConfirmDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeliveryChargeTypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Canceled { get; set; }
    }

    public class OverseaShippingInfoDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string PersonalCustomsClearanceCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrdererSsn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrdererPhoneNumber { get; set; }
    }
}
