using UnityEngine;

[System.Serializable]
public class TempItem
{
    [Header("Спрайт для замены обложки.")]
    [SerializeField] private Sprite _substrate = null;
    [Header("Спрайт для замены иконки.")]
    [SerializeField] private Sprite _icon = null;
    [Header("Название карты.")]
    [SerializeField] private string _name = "Повар";
    [Header("Мини описание.")]
    [SerializeField] private string _toolTip = "Краткая информация.";
    [Header("Строковой id карты.")]
    [SerializeField] private string _id = "Povar";
    [Header("Каждая вторая ячейка,это 3 нуля. Цена: ")]
    [SerializeField] private Abbreviations _abbreviationsPrice;
    [Range(1, 999)]
    [SerializeField] private int _price = 0;
    [Header("Каждая вторая ячейка,это 3 нуля. Доход: ")]
    [SerializeField] private Abbreviations _abbreviationsIncome;
    [Range(1, 999)]
    [SerializeField] private int _income = 0;
    [Header("Во сколько раз обновиться цена.")]
    [SerializeField] private float _priceUp = 0;
    [Header("Авто-заполнение.")]
    [Tooltip("Сделано для ленивого гд,чисто для тестов,надеюсь он не будет использовать это в разработке.")]
    [SerializeField] private bool _autoFilling = false;
    [Header("На сколько раз умножить цену следующей карты.")]
    [Tooltip("Умножает цену и доход передедущей карты на это значение чтобы получить своё значение.")]
    [SerializeField] private float _autoUp = 0;
    [Header("Игнорировать авто-заполнение.")]
    [Tooltip("Сделано на случай если гд таки воспользуется авто-заполнением в разработке.")]
    [SerializeField] private bool _autoIgnor = true;
    [Header("Полное описание.")]
    [TextArea(5, 10)]
    [SerializeField] private string _description = "Более полное описание";

    [HideInInspector]
    [SerializeField]
    typeCard _typeCard = typeCard.item;
    [HideInInspector]
    [SerializeField]
    double _autoIncome = 0;
    [HideInInspector]
    [SerializeField]
    double _autoPrice = 0;
    [HideInInspector]
    [SerializeField]
    double _count = 1;
    [HideInInspector]
    [SerializeField]
    bool _isBay = false;
    [HideInInspector]
    [SerializeField]
    float _bonus = 1;

    public Sprite Substrate => _substrate;
    public Sprite Icon => _icon;
    public string Name { get => _name; set => _name = value; }
    public string ToolTip => _toolTip;
    public string Id { get => _id; set => _id = value; }
    public Abbreviations AbbreviationsPrice => _abbreviationsPrice;
    public int Price => _price;
    public Abbreviations AbbreviationsIncome => _abbreviationsIncome;
    public int Income => _income;
    public float PriceUp { get => _priceUp; set => _priceUp = value; }
    public bool AutoFilling => _autoFilling;
    public float AutoUp { get => _autoUp; set => _autoUp = value; }
    public bool AutoIgnor => _autoIgnor;
    public string Description => _description;
    public typeCard TypeCard { get => _typeCard; set => _typeCard = value; }
    public double AutoIncome { get => _autoIncome; set => _autoIncome = value; }
    public double AutoPrice { get => _autoPrice; set => _autoPrice = value; }
    public double Count { get => _count; set => _count = value; }
    public bool IsBay { get => _isBay; set => _isBay = value; }
    public float Bonus { get => _bonus; set => _bonus = value; }
}


