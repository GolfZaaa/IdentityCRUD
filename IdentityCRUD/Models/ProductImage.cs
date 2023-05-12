using System.Text.Json.Serialization;

namespace IdentityCRUD.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }


    }
}
