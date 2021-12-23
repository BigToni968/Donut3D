using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
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

    [SerializeField] private Shell<TempItem> _items = null;

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

        //����� ��� ������������� � ��������� ���������.
        _items.cards = autoFilling(_items.cards);

        //���� ����� ���,�� ���� ������ �����,�� ����� ��������� ������ �����,������ �����.
        if (!System.IO.File.Exists(Application.persistentDataPath + _folder + _file))
            _myFile.Write(Application.persistentDataPath + _folder, _file, JsonUtility.ToJson(_items));
    }

    //����� � ��������� ���� �����. � ��������� �� �������.
    private List<TempItem> autoFilling(List<TempItem> list)
    {
        TempItem tempItem = null;
        int count = 1;
        if (list != null)
            if (list.Count > 0)
                foreach (TempItem temp in list)
                {
                    temp.AutoPrice = double.Parse(temp.Price + "e" + ((int)temp.AbbreviationsPrice * 3));
                    temp.AutoIncome = double.Parse(temp.Income + "e" + ((int)temp.AbbreviationsIncome * 3));
                    temp.Count = temp.Count = 1;
                    temp.IsBay = false;

                    if (temp.AutoFilling)
                    {
                        if (tempItem == null)
                            tempItem = temp;

                        if (!temp.AutoIgnor)
                        {
                            temp.Name += " " + count;
                            temp.Id += " " + count;

                            temp.AutoPrice = tempItem.AutoPrice * tempItem.AutoUp;
                            temp.AutoIncome = tempItem.AutoIncome * tempItem.AutoUp;
                            temp.PriceUp = tempItem.PriceUp;
                            temp.AutoUp = tempItem.AutoUp;
                            tempItem = temp;

                            count++;
                        }

                    }

                    if (temp.TypeCard == typeCard.non)
                        temp.TypeCard = typeCard.item;
                }

        tempItem = null;

        return list;
    }

    //����� ������� ����������� ��� ���������� �������. � �� ���������� ���. �������� �� ������ ������.
    private void OnDestroy()
    {
        if (_isDeleted)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(Application.persistentDataPath + _folder + _file);
            fileInfo.Delete();
        }
    }
}
