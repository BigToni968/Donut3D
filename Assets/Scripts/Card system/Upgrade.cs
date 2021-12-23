using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [Header("������ Content ������� ��������� � scroll view.")]
    [SerializeField] private GameObject _content = null;

    [Header("������ ������� ����� ��� ���������� ��������.")]
    [SerializeField] private Substrate _substrate = null;

    [Header("�������� ����� ���� ��������� ������.")]
    [Tooltip("���� �� ��������� ��� ������,�� ��� ������ �� �������.")]
    [SerializeField] private string _folder = null;

    [Header("��� ����� � ����������. ������ file.txt")]
    [Tooltip("��������,���� ������ ����� ���������� ���,����� ���������� �� ���������!")]
    [SerializeField] private string _file = null;

    [Header("������� ������� ���� ��� ������ �� play?")]
    [Tooltip("��������,�������� ����������� ����� ����� ������ �� ����������,��� ��� � ������ ������ ��������� ���� �� false")]
    [SerializeField] private bool _isDeleted = true;

    [SerializeField] private Shell<TempUpgrade> upgrades = null;

    public GameObject Content => _content;
    public Substrate Substrate => _substrate;
    public string Folder => _folder;
    public string File => _file;



    //��� ������ � �������.
    private File _myFile = new File();

    private void Awake()
    {
        //�������� �� ����������.
        _file = _file == null || _file == "" ? GetType() + ".json" : _file;

        //�������� �� ����������.
        if (_folder != null || _folder != "")
            if (!_folder.Contains("/"))
                _folder += "/";

        //����� ����� ��� �������� ���� �����,�� ������ ���� �� ������ ���,��� ������ �� ���������.
        upgrades.cards = AutoFilling(upgrades.cards);

        //���� ����� ���,�� ���� ������ �����,�� ����� ��������� ������ �����,������ �����.
        if (!System.IO.File.Exists(Application.persistentDataPath + _folder + _file))
            _myFile.Write(Application.persistentDataPath + _folder, _file, JsonUtility.ToJson(upgrades));

    }

    //����� � ��������� ���� �����. � ��������� �� �������.
    private List<TempUpgrade> AutoFilling(List<TempUpgrade> list)
    {
        if (list != null)
            if (list.Count > 0)
                foreach (TempUpgrade temp in list)
                {
                    temp.AutoPrice = double.Parse(temp.Price + "e" + (int)temp.AbbreviationsPrice);

                    if (temp.TypeCard == typeCard.non || temp.TypeCard == typeCard.item)
                        temp.TypeCard = typeCard.upItem;
                }
        return list;
    }


    //����� ��� ������� ����������� ��� ������ �� ����. � �� ���������� ���. ���������� � �������� �� ������ ������.

    private void OnDestroy()
    {
        if (_isDeleted)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(Application.persistentDataPath + _folder + _file);
            fileInfo.Delete();
        }
    }
}
