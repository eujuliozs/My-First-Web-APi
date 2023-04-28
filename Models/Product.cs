using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FirstWebApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string? Name { get; set; }

        [Required]
        [StringLength(300)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public float? Stock { get; set; }

        [DataType(DataType.Date)]
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DateTime? Moment { get; set; }
        public int CategoryId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Category? Category { get; set; }
    }
}
