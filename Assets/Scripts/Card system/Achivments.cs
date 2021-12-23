using System.Collections.Generic;
using UnityEngine;

public class Achivments : MonoBehaviour
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

    [SerializeField] private Shell<TempAchivments> _achivments = null;

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
        _achivments.cards = AutoFilling(_achivments.cards);

        //���� ����� ���,�� ���� �������� �����,�� ����� ��������� ������ �����,������ �����.
        if (!System.IO.File.Exists(Application.persistentDataPath + _folder + _file))
            _myFile.Write(Application.persistentDataPath + _folder, _file, JsonUtility.ToJson(_achivments));

    }

    //����� � ��������� ���� �����. � ��������� �� �������.
    private List<TempAchivments> AutoFilling(List<TempAchivments> list)
    {
        if (list != null)
            if (list.Count > 0)
                foreach (TempAchivments temp in list)
                {
                    temp.IsOpen = false;
                    temp.IsReceived = false;
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
