namespace MastersAggregatorService.Models;

public class Image : BaseModel
{
    public int ImageId { get; set; }
    public string ImageUrl { get; set; } //������ ���� �� ������� ������ ������ � ������� ���������� ���������� ������
    public string ImageDescription { get; set; } = "�� ������� �������� �������"; //�������� ������� �� ����
}