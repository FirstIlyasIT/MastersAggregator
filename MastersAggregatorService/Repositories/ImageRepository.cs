using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class ImageRepository : BaseRepository<Image>
{
    public static List<Image> Images { get; }

    static ImageRepository()
    {
        Images = new List<Image>
        {
            new Image { ImageUrl = "https://my-domen.com/conten/images/21324.ipg", ImageDescription = "�������� ������: ���������� �������� ������� ����� �� ���� �������� ������� - ������� ����" },
            new Image { ImageUrl = "https://my-domen.com/conten/images/21325.ipg", ImageDescription = "�������� ������: � ��� �� ����������� ���� �� ���� ����� ��������" },
            new Image { ImageUrl = "https://my-domen.com/conten/images/21326.ipg", ImageDescription = "�������� ������: ������� ���� ��� � ������ �������" }
        };
    }
    public new Image? GetById(int id)
    {
        if (Images[id] is null)
            return null;

        return Images[id];
    }
}