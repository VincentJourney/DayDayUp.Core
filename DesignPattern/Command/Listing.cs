using System;

namespace DesignPattern.Command
{
    public abstract class Listing
    {

        public void Execute()
        {
            CreateBaseInfo();
            CreateSku();
            AddSku();
            CalculatePrice();
            CreateListingQueueData();
            Enqueue();
        }
        public abstract void CreateBaseInfo();
        public abstract void CreateSku();
        public abstract void AddSku();
        public abstract void CalculatePrice();
        public abstract void CreateListingQueueData();
        public abstract void Enqueue();
    }
    public class AliexpressListing : Listing
    {
        public override void CreateBaseInfo()
        {
            Console.WriteLine(nameof(CreateBaseInfo));
        }

        public override void CreateSku()
        {
            new AliexpressCreateSkuCommand().Execute();
        }

        public override void AddSku()
        {
            Console.WriteLine(nameof(AddSku));
        }

        public override void CalculatePrice()
        {
            Console.WriteLine(nameof(CalculatePrice));
        }

        public override void CreateListingQueueData()
        {
            Console.WriteLine(nameof(CreateListingQueueData));
        }

        public override void Enqueue()
        {
            Console.WriteLine(nameof(Enqueue));
        }
    }
}
