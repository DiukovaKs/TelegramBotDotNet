using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBotDotNet.Data;

[Table("chat_to_city")]
public class ChatToCityEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    
    [Required]
    [Column("city_id")]
    public long CityId { get; set; }

    [Required]
    [Column("chat_id")]
    public long ChatId { get; set; }
}