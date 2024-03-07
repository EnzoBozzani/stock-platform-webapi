namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        //ao criar o Comment, criará o CreatedOn no momento da criação
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        //isso é uma NavigationProperty, a qual permite navegar entre os Models, 
        //ou seja, acessar por exemplo o dado de um Stock por meio de um Comment
        public Stock? Stock { get; set; }
    }
}