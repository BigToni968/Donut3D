using System.Collections.Generic;
using UnityEngine;

public class AchivmentsFilling : MonoBehaviour
{
    [Header("����� � AchivmentsManager")]
    [SerializeField] private Achivments _achivment = null;

    private Shell<TempAchivments> _achivments = null;

    private File _myFile = new File();

    private Data _data = Data.GetInstance();

    private void Start()
    {
        //���� �� ������ �������� ������ �� AchivmentsManager,�� �������� �������� ������ �� �����.
        if (_achivment != null)
            _achivments = JsonUtility.FromJson<Shell<TempAchivments>>(_myFile.Read(Application.persistentDataPath + _achivment.Folder, _achivment.File));

        //���� ������ �� ����� ���� ��������.
        if (_achivments != null)
        {
            //���������� ������ � ����� � ���������� ��� ������.
            _data.PathAchivments = Application.persistentDataPath + _achivment.Folder + _achivment.File;
            _data.Achivments = _achivments;
            //� ��������� ������ ����� �������.
            FirstFilling(_achivments.cards);
        }

    }

    //����� ��� ���������� �����.
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
