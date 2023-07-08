namespace Aula2.Models.Entity
{
    public class OrderEntity
    {
        public long Id { get; private set; }
        public string Order_number { get; private set; }
        public DateTime Purchase_date { get; private set; }
        public DateTime Download_date { get; private set; }
        public decimal Price { get; private set; }
        public short First_message { get; private set; }
    }
}
