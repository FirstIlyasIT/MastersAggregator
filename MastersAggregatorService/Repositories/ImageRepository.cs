using MastersAggregatorService.Data;
using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class ImageRepository : BaseRepository<Image>
{
    static List<Image> Images { get; set; }
    //Все тестовые данные в классе TestData для получения тестовых данных прописал их в конструкторе и получай через DI
    public ImageRepository()
    { 
        Images = TestData.Images; 
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

        return Images[index];
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

        Images.RemoveAt(index);
    }
}