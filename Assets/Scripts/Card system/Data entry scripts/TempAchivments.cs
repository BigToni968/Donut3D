using UnityEngine;
[System.Serializable]
public class TempAchivments
{
    [Header("������ ��� ������ �������.")]
    [SerializeField] private Sprite _substrate = null;
    [Header("������ ��� ������ ������.")]
    [SerializeField] private Sprite _icon = null;
    [Header("�������� �����.")]
    [SerializeField] private string _name = "��������!";
    [Header("���� ��������.")]
    [SerializeField] private string _toolTip = "������� ����������.";
    [Header("��������� id �����.")]
    [SerializeField] private string _id = "Nice_game";
    [Header("��� ��������� ����������")]
    [SerializeField] private typeAchivments _condition = typeAchivments.non;
    [Header("��������� ������� ����������")]
    [SerializeField] private float _unit = 0;
    [Header("��� ������� �� ����������")]
    [SerializeField] typeAchivments _income = typeAchivments.non;
    [Header("������� ���������� ������")]
    [SerializeField] private float _incomeUnit = 0;
    [SerializeField]
    [HideInInspector] private bool _isReceived = false;
    [SerializeField]
    [HideInInspector] private bool _isOpen = false;
    [TextArea(5, 10)]
    [SerializeField] private string _description = "����� ������ ��������";

    public Sprite Substrate => _substrate;
    public Sprite Icon => _icon;
    public string Name { get => _name; set => _name = value; }
    public string ToolTip => _toolTip;
    public string Id { get => _id; set => _id = value; }
    public typeAchivments Condition => _condition;
    public float Unit => _unit;
    public typeAchivments Income => _income;
    public float IncomeUnit => _incomeUnit;
    public bool IsReceived { get => _isReceived; set => _isReceived = value; }
    public bool IsOpen { get => _isOpen; set => _isOpen = value; }
    public string Description => _description;
}
