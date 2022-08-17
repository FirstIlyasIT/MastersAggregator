using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class ImageRepository : BaseRepository<Image>
{
    static List<Image> Images { get; set; }

    static ImageRepository()
    {
        Images = new List<Image>
        {
            new Image { Id = 0, ImageUrl = "https://my-domen.com/conten/images/21324.ipg", ImageDescription = "описание работы: необходимо починить дверной замок на фото показана поломка - сломалс¤ ключ" },
            new Image { Id = 1, ImageUrl = "https://my-domen.com/conten/images/21325.ipg", ImageDescription = "описание работы: у мен¤ не закрываетьс¤ окно на фото видно проблему" },
            new Image { Id = 2, ImageUrl = "https://my-domen.com/conten/images/21326.ipg", ImageDescription = "описание работы: перекос окна вид с другой стороны" }
        };
    }
   

    public override IEnumerable<Image> GetAll()
    {
        return Images;
    }

    public override Image? GetById(int id)
    {
        var index = Images.FindIndex(p => p.Id == id);
        if (index == -1)
            return null;

        return Images[id];
    }

    public override Image Save(Image model)
    {
        Images.Add(model);
        return model;
    }

    public override void Delete(Image model)
    {
        var index = Images.FindIndex(p => p.Id == model.Id);
        if (index == -1)
            return;

        Images.Remove(GetById(model.Id));
    }
}