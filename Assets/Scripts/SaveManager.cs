using System.Collections;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("�������� ����-����������?")]
    [SerializeField] private bool _isAutoSave = false;
    [Header("����� ����� ����-������������ � ��������.")]
    [Range(1, 600)]
    [SerializeField] private float _autoSaveTime = 10;
    [Header("������� ������� ���������� ����� ������ �� PLay?")]
    [Tooltip("��������,�������� ����������� ����� ����� ������ �� ����������,��� ��� � ������ ������ ��������� ���� �� false")]
    [SerializeField] private bool _isDeleted = true;

    //������ � �������.
    private Data _data = Data.GetInstance();
    private DataTemp _json = null;


    private File _myFile = new File();

    private void Awake() => _json = _data.Output();

    void Start()
    {
        //���� �������� ����-����������.
        if (_isAutoSave)
            StartCoroutine(AutoSave());
    }

    //����� ��� ����������.
    public void Save()
    {
        _data.Input(_json);
        WriteData(_data.PathUp, _data.Upgrades);
        WriteData(_data.PathItem, _data.Items);
        WriteData(_data.PathAchivments, _data.Achivments);
    }

    //������ ������.
    private void WriteData<U>(string path, U shell)
    {
        if (path != null)
            if (shell != null)
                _myFile.Write(path, JsonUtility.ToJson(shell));
    }

    //�������� ����������� ���� ����-����������.
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
