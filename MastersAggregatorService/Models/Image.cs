namespace MastersAggregatorService.Models;

public class Image : BaseModel
{
    public string ImageUrl { get; set; } //храним фото на котором указан обьект с которым необходимо произвести ремонт
    public string ImageDescription { get; set; } = "не указано описание задания"; //описание задания на фото
}