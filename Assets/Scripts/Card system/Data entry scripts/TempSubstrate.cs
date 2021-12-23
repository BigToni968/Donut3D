using UnityEngine;

[System.Serializable]
public class TempSubstrate
{
    [Header("������ ��� ������ �������.")]
    [SerializeField] private Sprite _substrate = null;
    [Header("������ ��� ������ ������.")]
    [SerializeField] private Sprite _icon = null;
    [Header("�������� ��������.")]
    [SerializeField] private string _name = "�����";
    [Header("���� ��������.")]
    [SerializeField] private string _toolTip = "�������� ����������.";
    [Header("��������� id ��������. �������� �� ���������!")]
    [SerializeField] private string _id = "Povar";
    [Header("������������. ������ ������,��� 3 ����,����� ������. ����: ")]
    [SerializeField] private Abbreviations _abbreviationsPrice;
    [Range(1, 999)]
    [SerializeField] private int _price = 0;
    [Header("������������. ������ ������,��� 3 ����,����� ������. �����: ")]
    [SerializeField] private Abbreviations _abbreviationsIncome;
    [Range(1, 999)]
    [SerializeField] private int _income = 0;
    [Header("������ ��������.")]
    [TextArea(5, 10)]
    [SerializeField] private string _description = "����� ������ ��������";
}

