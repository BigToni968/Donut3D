using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [Header("Объект Content который находится в scroll view.")]
    [SerializeField] private GameObject _content = null;

    [Header("Префаб который нужен для заполнения контента.")]
    [SerializeField] private Substrate _substrate = null;

    [Header("Название папки куда сохранить данные.")]
    [Tooltip("Если не заполнять эти данные,то они пойдут по дефолту.")]
    [SerializeField] private string _folder = null;

    [Header("Имя файла с раширением. Пример file.txt")]
    [Tooltip("Внимание,файл должен иметь уникальное имя,иначе сохранение не произойдёт!")]
    [SerializeField] private string _file = null;

    [Header("Удалить готовый файл при выходе из play?")]
    [Tooltip("Внимание,подобное реализовано через метод выхода из приложения,так что в случае сборки установть флаг на false")]
    [SerializeField] private bool _isDeleted = true;

    [SerializeField] private Shell<TempUpgrade> upgrades = null;

    public GameObject Content => _content;
    public Substrate Substrate => _substrate;
    public string Folder => _folder;
    public string File => _file;



    //Для работы с данными.
    private File _myFile = new File();

    private void Awake()
    {
        //Проверка на содержимое.
        _file = _file == null || _file == "" ? GetType() + ".json" : _file;

        //Проверка на содержимое.
        if (_folder != null || _folder != "")
            if (!_folder.Contains("/"))
                _folder += "/";

        //Метод чисто для проверки типа карты,на случай если не выбран тип,или выбран не правильно.
        upgrades.cards = AutoFilling(upgrades.cards);

        //Если файла нет,то есть готово файла,то будет проведена запись этого,самого файла.
        if (!System.IO.File.Exists(Application.persistentDataPath + _folder + _file))
            _myFile.Write(Application.persistentDataPath + _folder, _file, JsonUtility.ToJson(upgrades));

    }

    //Метод с проверкой типа карты. И настройки по дефолту.
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


    //Метод для который срабатывает при выходе из игры. В нём реализация доп. сохранение и удаление на случай тестов.

    private void OnDestroy()
    {
        if (_isDeleted)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(Application.persistentDataPath + _folder + _file);
            fileInfo.Delete();
        }
    }
}
