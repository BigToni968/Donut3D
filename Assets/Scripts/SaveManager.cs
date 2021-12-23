using System.Collections;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("Включить авто-сохранение?")]
    [SerializeField] private bool _isAutoSave = false;
    [Header("Время между авто-сохранениями в секундах.")]
    [Range(1, 600)]
    [SerializeField] private float _autoSaveTime = 10;
    [Header("Удалить готовое сохранение после выхода из PLay?")]
    [Tooltip("Внимание,подобное реализовано через метод выхода из приложения,так что в случае сборки установть флаг на false")]
    [SerializeField] private bool _isDeleted = true;

    //Работа с данными.
    private Data _data = Data.GetInstance();
    private DataTemp _json = null;


    private File _myFile = new File();

    private void Awake() => _json = _data.Output();

    void Start()
    {
        //Если включено авто-сохранение.
        if (_isAutoSave)
            StartCoroutine(AutoSave());
    }

    //Метод для сохранения.
    public void Save()
    {
        _data.Input(_json);
        WriteData(_data.PathUp, _data.Upgrades);
        WriteData(_data.PathItem, _data.Items);
        WriteData(_data.PathAchivments, _data.Achivments);
    }

    //Запись данных.
    private void WriteData<U>(string path, U shell)
    {
        if (path != null)
            if (shell != null)
                _myFile.Write(path, JsonUtility.ToJson(shell));
    }

    //Коротина выполянющая роль авто-сохранения.
    private IEnumerator AutoSave()
    {
        while (_isAutoSave)
        {
            yield return new WaitForSeconds(_autoSaveTime);
            Save();
        }
    }

    private void OnDestroy()
    {
        if (_isDeleted)
            _data.Clear();
        else
            Save();
    }

    private void OnApplicationQuit() => Save();

}
