using System.Collections.Generic;
using UnityEngine;

public class AchivmentsFilling : MonoBehaviour
{
    [Header("Класс с AchivmentsManager")]
    [SerializeField] private Achivments _achivment = null;

    private Shell<TempAchivments> _achivments = null;

    private File _myFile = new File();

    private Data _data = Data.GetInstance();

    private void Start()
    {
        //Если не забыли добавить скрипт из AchivmentsManager,то пытаемся получить данные из файла.
        if (_achivment != null)
            _achivments = JsonUtility.FromJson<Shell<TempAchivments>>(_myFile.Read(Application.persistentDataPath + _achivment.Folder, _achivment.File));

        //Если данные из файла были получены.
        if (_achivments != null)
        {
            //Закидываем данные в класс с синглтоном для работы.
            _data.PathAchivments = Application.persistentDataPath + _achivment.Folder + _achivment.File;
            _data.Achivments = _achivments;
            //И заполняем шаблон этими данными.
            FirstFilling(_achivments.cards);
        }

    }

    //Метод для заполнения карты.
    void FirstFilling(List<TempAchivments> listUpgrade)
    {
        if (listUpgrade != null)
            if (listUpgrade.Count > 0)
                foreach (TempAchivments temp in listUpgrade)
                    if (temp != null)
                    {
                        Substrate substrate = Instantiate(_achivment.Substrate, _achivment.Content.transform);
                        substrate.name = _achivment.Substrate.name;
                        substrate.Filling(temp);
                    }
    }
}
