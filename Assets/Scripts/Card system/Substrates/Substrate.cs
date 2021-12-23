using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Substrate : MonoBehaviour
{
    //Массивы
    private Image[] _componentsImage;
    private TextMeshProUGUI[] _componentsText;
    //Класс для форматирования больших чисел. 
    private Formatting _formatting;

    //Переменные для обработки нажатия.
    private float _second = 0;
    private bool _visibleDescription = false;
    private bool _isClick = false;
    private bool _isOver = false;

    //Синглтон с данными
    private Data _data = Data.GetInstance();
    private DataTemp _json = null;

    //Класс отвечающий за обработку выбранной карты.
    private SelectedCard _selectedCard;

    //Классы для перегрузки
    private TempItem _item = null;
    private TempUpgrade _upgrade = null;
    private TempAchivments _achivment = null;

    //Каждая карта будет знать себе цену.
    //Так проще понять что её можно скрыть,или показать.
    private double _price = 0;

    private void Awake()
    {
        _componentsImage = GetComponentsInChildren<Image>();
        _componentsText = GetComponentsInChildren<TextMeshProUGUI>();
        _formatting = new Formatting();
        _json = _data.Output();
        _selectedCard = new SelectedCard(this, _json);
    }

    //Замена спрайта с карты на выбранный спрайт. По именам поиск.
    private void SetSprite(string findSpriteName, string replaceable)
    {
        foreach (Image image in _componentsImage)
            if (image.name.Equals(findSpriteName))
            {
                if (replaceable != null && replaceable != "")
                    image.sprite = ResourcesExtension.Load(replaceable);

                return;
            }

        Debug.Log("Что-то пошло не так!\n" +
            "На префабе искали спрайт  = " + findSpriteName + "\n" +
            "Но не нашли.");
    }

    //Замена Текста с карты на выбранный текст. По именам поиск.
    private void SetText(string findTextName, string txt)
    {
        foreach (TextMeshProUGUI text in _componentsText)
            if (text.name.Equals(findTextName))
            {
                text.text = txt;
                return;
            }

        Debug.Log("Что-то пошло не так!\n" +
    "На префабе искали TextMeshPro  = " + findTextName + "\n" +
    "Но не нашли.");
    }

    private void SetCard(Sprite substrate, Sprite icon, (string name, string toolTip, string description, double autoPrice) text, TempAchivments achivment = null)
    {
        //Добавляем спрайт обложки.
        SetSprite("Substrate", substrate != null ? substrate.name : null);
        //Добавляем спрайт иконки.
        SetSprite("Icon", icon != null ? icon.name : null);
        //Добавляем имя.
        SetText("Name", text.name);
        //Добавляем подсказку.
        SetText("Tooltip", text.toolTip);
        //Добавляем полное описание.
        SetText("Description", text.description);
        //Добавляем цену.
        if (achivment == null)
            SetText("Price", _formatting.ToText(text.autoPrice));
        else
            SetText("Price", achivment.IsOpen ? achivment.IsReceived ? "Забрано!" : "Доступно!" : "Не доступно!");
    }

    //Показывает полное описание,скрывая всё кроме описания. И наоборот.
    public void Description(bool visible)
    {
        foreach (TextMeshProUGUI text in _componentsText)
            if (text.name.Equals("Description"))
                text.color = new Color(text.color.r, text.color.g, text.color.b, visible ? 1 : 0);
            else
                text.color = new Color(text.color.r, text.color.g, text.color.b, visible ? 0 : 1);
    }

    public void Filling(TempItem item)
    {
        if (item != null)
        {
            //Каждая карта сама знает всю нужную ей информацию о себе.
            _item = item;
            //Установка цены.
            _price = item.AutoPrice;
            //Настройка отображаемой части.
            SetCard(item.Substrate, item.Icon, (item.Name, item.ToolTip, item.Description, item.AutoPrice));
        }
    }

    public void Filling(TempUpgrade upgrade)
    {
        if (upgrade != null)
        {
            //Каждая карта сама знает всю нужную ей информацию о себе.
            _upgrade = upgrade;
            //Установка цены.
            _price = upgrade.AutoPrice;
            //Настройка отображаемой части.
            SetCard(upgrade.Substrate, upgrade.Icon, (upgrade.Name, upgrade.ToolTip, upgrade.Description, upgrade.AutoPrice));
        }
    }

    public void Filling(TempAchivments achivment)
    {
        if (achivment != null)
        {
            //Каждая карта сама знает всю нужную ей информацию о себе.
            _achivment = achivment;
            //Установка цены.
            _price = 0;
            //Настройка отображаемой части.
            SetCard(achivment.Substrate, achivment.Icon, (achivment.Name, achivment.ToolTip, achivment.Description, 0), achivment);
        }
    }

    //Всё что находиться ниже отвечает за выбор карты,её реакцию и прочее.
    public void OnFingerOver() => _isOver = true;

    public void OnFingerExit()
    {
        _isOver = false;
        _isClick = false;
        _second = 0;
    }


    public void OnFingerDown()
    {
        if (_isOver)
            _isClick = true;
    }

    public void OnFingerUp()
    {
        if (_second < 1 && _isClick)
        {
            if (_price <= _json.Balance)
                if (_item != null)
                    _selectedCard.CardItem(_item);
                else if (_upgrade != null)
                    _selectedCard.CardUpgrade(_upgrade);
                else if (_achivment != null)
                    if (_achivment.IsOpen)
                        _selectedCard.CardAchivment(_achivment);
        }
        _second = 0;
        _isClick = false;
    }

    public void Destroy() => Destroy(gameObject);

    private void Update()
    {
        if (_achivment != null)
        {
            if (_achivment.IsOpen == false)
            {
                switch (_achivment.Condition)
                {
                    case typeAchivments.balance:
                        if (_json.Balance >= _achivment.Unit)
                            _achivment.IsOpen = true;
                        break;
                    case typeAchivments.click:
                        if (_json.Click >= _achivment.Unit)
                            _achivment.IsOpen = true;
                        break;
                    case typeAchivments.perSecond:
                        if (_json.PerSecond >= _achivment.Unit)
                            _achivment.IsOpen = true;
                        break;
                    case typeAchivments.emerald:
                        if (_json.Emerald >= _achivment.Unit)
                            _achivment.IsOpen = true;
                        break;
                }
                Filling(_achivment);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isClick)
        {
            _second += Time.deltaTime;
            if (_second > 1)
            {
                Description(_visibleDescription = !_visibleDescription);
                _isClick = false;
            }
        }
    }
}
