using UnityEngine;
[System.Serializable]
public class TempUpgrade
{
    [Header("Спрайт для замены обложки.")]
    [SerializeField] private Sprite _substrate = null;
    [Header("Спрайт для замены иконки.")]
    [SerializeField] private Sprite _icon = null;
    [Header("Название апгрейда.")]
    [SerializeField] private string _name = "Повар плюс";
    [Header("Мини описание.")]
    [SerializeField] private string _toolTip = "Короткая информация.";
    [Header("Строковой id того что будет улучшено.")]
    [SerializeField] private string _id = "Povar";
    [Header("Тип улучшения.")]
    [SerializeField] private typeCard _typeCard = typeCard.upItem;
    [Header("Каждая вторая ячейка,это 3 нуля. Цена апгрейда: ")]
    [SerializeField] private Abbreviations _abbreviationsPrice;
    [Range(1, 999)]
    [SerializeField] private int _price = 0;
    [Header("Во сколько раз будет увеличина прибыль.")]
    [SerializeField] private float _percent = 0;
    [Header("Полное описание.")]
    [TextArea(5, 10)]
    [SerializeField] private string _description = "Более полное описание";
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
