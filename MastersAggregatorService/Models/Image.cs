namespace MastersAggregatorService.Models;

public class Image : BaseModel
{
    public string ImageUrl { get; set; } //������ ���� �� ������� ������ ������ � ������� ���������� ���������� ������
    public string ImageDescription { get; set; } = "�� ������� �������� �������"; //�������� ������� �� ����
}