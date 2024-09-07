namespace RestfullServiceDemo.Model
{
    public class Book
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int isDelete { get; set; }
    }
}
