using System.ComponentModel.DataAnnotations;

namespace ChuckItApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Icon { get; set; }
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
    }
}
