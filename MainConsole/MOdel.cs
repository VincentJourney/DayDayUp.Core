using System;
using System.Collections.Generic;
using System.Text;

namespace MainConsole
{
    /// <summary>
    /// InventoryReviseSetting实体模型 DTO
    /// </summary>
    public class InventoryReviseSettingModel
    {

        /// <summary>
        /// 平台
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string SellerAccount { get; set; }

        /// <summary>
        /// 站点
        /// </summary>
        public string SaleSite { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// 其他条件
        /// </summary>
        public string ExtendedCondition { get; set; }
    }
}
