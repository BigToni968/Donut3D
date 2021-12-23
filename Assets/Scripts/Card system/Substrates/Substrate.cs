using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Substrate : MonoBehaviour
{
    //�������
    private Image[] _componentsImage;
    private TextMeshProUGUI[] _componentsText;
    //����� ��� �������������� ������� �����. 
    private Formatting _formatting;

    //���������� ��� ��������� �������.
    private float _second = 0;
    private bool _visibleDescription = false;
    private bool _isClick = false;
    private bool _isOver = false;

    //�������� � �������
    private Data _data = Data.GetInstance();
    private DataTemp _json = null;

    //����� ���������� �� ��������� ��������� �����.
    private SelectedCard _selectedCard;

    //������ ��� ����������
    private TempItem _item = null;
    private TempUpgrade _upgrade = null;
    private TempAchivments _achivment = null;

    //������ ����� ����� ����� ���� ����.
    //��� ����� ������ ��� � ����� ������,��� ��������.
    private double _price = 0;

    private void Awake()
    {
        _componentsImage = GetComponentsInChildren<Image>();
        _componentsText = GetComponentsInChildren<TextMeshProUGUI>();
        _formatting = new Formatting();
        _json = _data.Output();
        _selectedCard = new SelectedCard(this, _json);
    }

    //������ ������� � ����� �� ��������� ������. �� ������ �����.
    private void SetSprite(string findSpriteName, string replaceable)
    {
        foreach (Image image in _componentsImage)
            if (image.name.Equals(findSpriteName))
            {
                if (replaceable != null && replaceable != "")
                    image.sprite = ResourcesExtension.Load(replaceable);

                return;
            }

        Debug.Log("���-�� ����� �� ���!\n" +
            "�� ������� ������ ������  = " + findSpriteName + "\n" +
            "�� �� �����.");
    }

    //������ ������ � ����� �� ��������� �����. �� ������ �����.
    private void SetText(string findTextName, string txt)
    {
        foreach (TextMeshProUGUI text in _componentsText)
            if (text.name.Equals(findTextName))
            {
                text.text = txt;
                return;
            }

        Debug.Log("���-�� ����� �� ���!\n" +
    "�� ������� ������ TextMeshPro  = " + findTextName + "\n" +
    "�� �� �����.");
    }

    private void SetCard(Sprite substrate, Sprite icon, (string name, string toolTip, string description, double autoPrice) text, TempAchivments achivment = null)
    {
        //��������� ������ �������.
        SetSprite("Substrate", substrate != null ? substrate.name : null);
        //��������� ������ ������.
        SetSprite("Icon", icon != null ? icon.name : null);
        //��������� ���.
        SetText("Name", text.name);
        //��������� ���������.
        SetText("Tooltip", text.toolTip);
        //��������� ������ ��������.
        SetText("Description", text.description);
        //��������� ����.
        if (achivment == null)
            SetText("Price", _formatting.ToText(text.autoPrice));
        else
            SetText("Price", achivment.IsOpen ? achivment.IsReceived ? "�������!" : "��������!" : "�� ��������!");
    }

    //���������� ������ ��������,������� �� ����� ��������. � ��������.
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
            //������ ����� ���� ����� ��� ������ �� ���������� � ����.
            _item = item;
            //��������� ����.
            _price = item.AutoPrice;
            //��������� ������������ �����.
            SetCard(item.Substrate, item.Icon, (item.Name, item.ToolTip, item.Description, item.AutoPrice));
        }
    }

    public void Filling(TempUpgrade upgrade)
    {
        if (upgrade != null)
        {
            //������ ����� ���� ����� ��� ������ �� ���������� � ����.
            _upgrade = upgrade;
            //��������� ����.
            _price = upgrade.AutoPrice;
            //��������� ������������ �����.
            SetCard(upgrade.Substrate, upgrade.Icon, (upgrade.Name, upgrade.ToolTip, upgrade.Description, upgrade.AutoPrice));
        }
    }

    public void Filling(TempAchivments achivment)
    {
        if (achivment != null)
        {
            //������ ����� ���� ����� ��� ������ �� ���������� � ����.
            _achivment = achivment;
            //��������� ����.
            _price = 0;
            //��������� ������������ �����.
            SetCard(achivment.Substrate, achivment.Icon, (achivment.Name, achivment.ToolTip, achivment.Description, 0), achivment);
        }
    }

    //�� ��� ���������� ���� �������� �� ����� �����,� ������� � ������.
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
