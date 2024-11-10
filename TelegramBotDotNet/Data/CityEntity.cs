using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBotDotNet.Entities;

[Table("cities")]
public class CityEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    
    [Required]
    [Column("city")]
    public string City { get; set; }

    [Required]
    [Column("latitude")]
    public string Latitude { get; set; }
    
    [Required]
    [Column("longitude")]
    public string Longitude { get; set; }
   
}