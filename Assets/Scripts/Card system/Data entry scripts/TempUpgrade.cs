using UnityEngine;
[System.Serializable]
public class TempUpgrade
{
    [Header("������ ��� ������ �������.")]
    [SerializeField] private Sprite _substrate = null;
    [Header("������ ��� ������ ������.")]
    [SerializeField] private Sprite _icon = null;
    [Header("�������� ��������.")]
    [SerializeField] private string _name = "����� ����";
    [Header("���� ��������.")]
    [SerializeField] private string _toolTip = "�������� ����������.";
    [Header("��������� id ���� ��� ����� ��������.")]
    [SerializeField] private string _id = "Povar";
    [Header("��� ���������.")]
    [SerializeField] private typeCard _typeCard = typeCard.upItem;
    [Header("������ ������ ������,��� 3 ����. ���� ��������: ")]
    [SerializeField] private Abbreviations _abbreviationsPrice;
    [Range(1, 999)]
    [SerializeField] private int _price = 0;
    [Header("�� ������� ��� ����� ��������� �������.")]
    [SerializeField] private float _percent = 0;
    [Header("������ ��������.")]
    [TextArea(5, 10)]
    [SerializeField] private string _description = "����� ������ ��������";
    [HideInInspector]
    [SerializeField] private double _autoPrice = 0;

    public Sprite Substrate => _substrate;
    public Sprite Icon => _icon;
    public string Name { get => _name; set => _name = value; }
    public string ToolTip => _toolTip;
    public string Id { get => _id; set => _id = value; }
    public typeCard TypeCard { get => _typeCard; set => _typeCard = value; }
    public Abbreviations AbbreviationsPrice => _abbreviationsPrice;
    public int Price => _price;
    public float Percent => _percent;
    public string Description => _description;
    public double AutoPrice { get => _autoPrice; set => _autoPrice = value; }
}
