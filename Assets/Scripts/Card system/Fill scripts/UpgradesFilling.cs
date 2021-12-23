using System.Collections.Generic;
using UnityEngine;

public class UpgradesFilling : MonoBehaviour
{
    [Header("����� � UpgradeManager")]
    [SerializeField] private Upgrade _upgrade = null;

    private Shell<TempUpgrade> _upgrades = null;

    private File _myFile = new File();

    private Data _data = Data.GetInstance();

    private void Start()
    {
        //���� �� ������ �������� ������ �� upgradeManager,�� �������� �������� ������ �� �����.
        if (_upgrade != null)
            _upgrades = JsonUtility.FromJson<Shell<TempUpgrade>>(_myFile.Read(Application.persistentDataPath + _upgrade.Folder, _upgrade.File));

        //���� ������ �� ����� ���� ��������.
        if (_upgrades != null)
        {
            //���������� ������ � ����� � ���������� ��� ������.
            _data.PathUp = Application.persistentDataPath + _upgrade.Folder + _upgrade.File;
            _data.Upgrades = _upgrades;
            //� ��������� ������ ����� �������.
            FirstFilling(_upgrades.cards);
        }

    }

    //����� ��� ���������� �����.
    private void FirstFilling(List<TempUpgrade> listUpgrade)
    {
        if (listUpgrade != null)
            if (listUpgrade.Count > 0)
                foreach (TempUpgrade temp in listUpgrade)
                    if (temp != null)
                    {
                        Substrate substrate = Instantiate(_upgrade.Substrate, _upgrade.Content.transform);
                        substrate.name = _upgrade.Substrate.name;
                        substrate.Filling(temp);
                    }
    }
}
