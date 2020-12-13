using System;
using System.Collections.Generic;
using System.Text;

namespace MainConsole
{
    public abstract class AbstractInventoryReviseSettingExportModel
    {
        /// <summary>
        /// 平台
        /// </summary>
        public string 平台 { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string 账号 { get; set; }

        /// <summary>
        /// 投射器
        /// </summary>
        public static Func<InventoryReviseSettingModel, AbstractInventoryReviseSettingExportModel> Selector;
    }

    /// <summary>
    /// 上库存SKU导出模型
    /// </summary>
    public class InventoryReviseSettingForSKUExportModel : AbstractInventoryReviseSettingExportModel
    {
        /// <summary>
        /// SKU
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// 投射器
        /// </summary>
        public new static Func<InventoryReviseSettingModel, AbstractInventoryReviseSettingExportModel> Selector = model
           => new InventoryReviseSettingForSKUExportModel
           {
               SKU = model.Sku,
               平台 = model.Platform,
               账号 = model.SellerAccount
           };
    }

    /// <summary>
    /// 上库存站点导出模型
    /// </summary>
    public class InventoryReviseSettingForSiteExportModel : AbstractInventoryReviseSettingExportModel
    {

        /// <summary>
        /// 站点
        /// </summary>
        public string 站点 { get; set; }

        /// <summary>
        /// 其他条件
        /// </summary>
        public string 其他条件 { get; set; }

        /// <summary>
        /// 投射器
        /// </summary>
        public new static Func<InventoryReviseSettingModel, AbstractInventoryReviseSettingExportModel> Selector = model
            => new InventoryReviseSettingForSiteExportModel
            {
                平台 = model.Platform,
                账号 = model.SellerAccount,
                其他条件 = model.ExtendedCondition,
                站点 = model.SaleSite
            };
    }

    /// <summary>
    /// 上库存账号导出模型
    /// </summary>
    public class InventoryReviseSettingForSellerAccountExportModel : AbstractInventoryReviseSettingExportModel
    {
        /// <summary>
        /// 其他条件
        /// </summary>
        public string 其他条件 { get; set; }

        /// <summary>
        /// 投射器
        /// </summary>
        public new static Func<InventoryReviseSettingModel, AbstractInventoryReviseSettingExportModel> Selector = model
            => new InventoryReviseSettingForSellerAccountExportModel
            {
                平台 = model.Platform,
                账号 = model.SellerAccount,
                其他条件 = model.ExtendedCondition
            };
    }
}
