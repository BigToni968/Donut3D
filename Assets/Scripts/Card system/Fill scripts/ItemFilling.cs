using System.Collections.Generic;
using UnityEngine;

public class ItemFilling : MonoBehaviour
{
    [Header("����� � ItemManager")]
    [SerializeField] private Item _item = null;

    private Shell<TempItem> _items = null;

    private File _myFile = new File();

    private Data _data = Data.GetInstance();

    private void Awake()
    {
        //���� �� ������ �������� ������ �� itemManager,�� �������� �������� ������ �� �����.
        if (_item != null)
            _items = JsonUtility.FromJson<Shell<TempItem>>(_myFile.Read(Application.persistentDataPath + _item.Folder, _item.File));

        //���� ������ �� ����� ���� ��������.
        if (_items != null)
        {
            //���������� ������ � ����� � ���������� ��� ������.
            _data.PathItem = Application.persistentDataPath + _item.Folder + _item.File;
            _data.Items = _items;
            //� ��������� ������ ����� �������.
            FirstFilling(_items.cards);
        }

    }

    //����� ��� ���������� �����.
    private void FirstFilling(List<TempItem> listItem)
    {
        if (listItem != null)
            if (listItem.Count > 0)
                foreach (TempItem temp in listItem)
                    if (temp != null)
                    {
                        Substrate substrate = Instantiate(_item.Substrate, _item.Content.transform);
                        substrate.name = _item.Substrate.name;
                        substrate.Filling(temp);
                    }
    }
}
