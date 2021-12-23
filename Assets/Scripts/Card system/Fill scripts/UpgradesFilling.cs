using System.Collections.Generic;
using UnityEngine;

public class UpgradesFilling : MonoBehaviour
{
    [Header("Класс с UpgradeManager")]
    [SerializeField] private Upgrade _upgrade = null;

    private Shell<TempUpgrade> _upgrades = null;

    private File _myFile = new File();

    private Data _data = Data.GetInstance();

    private void Start()
    {
        //Если не забыли добавить скрипт из upgradeManager,то пытаемся получить данные из файла.
        if (_upgrade != null)
            _upgrades = JsonUtility.FromJson<Shell<TempUpgrade>>(_myFile.Read(Application.persistentDataPath + _upgrade.Folder, _upgrade.File));

        //Если данные из файла были получены.
        if (_upgrades != null)
        {
            //Закидываем данные в класс с синглтоном для работы.
            _data.PathUp = Application.persistentDataPath + _upgrade.Folder + _upgrade.File;
            _data.Upgrades = _upgrades;
            //И заполняем шаблон этими данными.
            FirstFilling(_upgrades.cards);
        }

    }

    //Метод для заполнения карты.
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
