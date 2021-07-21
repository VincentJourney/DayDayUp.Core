using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DesignPattern.Command
{
    public abstract class ListingCommand
    {
        //调用命令
        public abstract ListingResponse Execute();
    }

    public abstract class CreateSkuCommand : ListingCommand
    {

    }
    public class AliexpressCreateSkuCommand : CreateSkuCommand
    {
        public AliexpressListingCreateSkuResponse Execute(AliexpressListingCreateSkuRequest aliexpressListingCreateSkuRequest)
        {
            Console.WriteLine(nameof(AliexpressCreateSkuCommand));
            return new AliexpressListingCreateSkuResponse();
        }

        public override ListingResponse Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class ListingResponse
    {
        public int SkipCount { get; set; }
    }

    public class AliexpressListingCreateSkuResponse : ListingResponse
    {

    }

    public class ListingRequest
    {
        public string BatchCode { get; set; }
        public ListingOperateEnum LazadaListingOperate { get; set; }
    }
    public class AliexpressListingCreateSkuRequest
    {
    }

    public enum ListingOperateEnum
    {
        [Description("生成基础信息")]
        CreateBaseInfo = 0,
        [Description("创建SKU")]
        CreateSKU = 1,
        [Description("加入调价列表")]
        AddIntoPriceReviseList = 2,
        [Description("计算初始价格")]
        ComputeSkuInitPrice = 3,
        [Description("创建队列数据")]
        BuildQueue = 4,
        [Description("加入队列")]
        EnterQueue = 5
    }
}
