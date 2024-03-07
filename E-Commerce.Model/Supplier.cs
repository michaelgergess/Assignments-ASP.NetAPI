namespace E_Commerce.Model
{
    public class Supplier :BaseClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Supplier()
        {
            Products = new List<Product>();
        }

    }
}
