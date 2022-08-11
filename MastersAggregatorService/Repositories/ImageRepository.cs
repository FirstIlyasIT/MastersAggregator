using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class ImageRepository : BaseRepository<Image>
{
    public static List<Image> Images { get; }

    static ImageRepository()
    {
        Images = new List<Image>
        {
            new Image { ImageUrl = "https://my-domen.com/conten/images/21324.ipg", ImageDescription = "описание работы: необходимо починить дверной замок на фото показана поломка - сломалс¤ ключ" },
            new Image { ImageUrl = "https://my-domen.com/conten/images/21325.ipg", ImageDescription = "описание работы: у мен¤ не закрываетьс¤ окно на фото видно проблему" },
            new Image { ImageUrl = "https://my-domen.com/conten/images/21326.ipg", ImageDescription = "описание работы: перекос окна вид с другой стороны" }
        };
    }
    public new Image? GetById(int id)
    {
        if (Images[id] is null)
            return null;

        return Images[id];
    }
}