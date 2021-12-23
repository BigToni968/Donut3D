using UnityEngine;

public class Data
{
    private string _DBname = "Donut3D";
    private static Data _instance = null;
    private DataTemp _json = null;
    private bool _openMenu = false;
    private string _pathItem = null;
    private string _pathUp = null;
    private string _pathAchivments = null;
    private Shell<TempItem> _items = null;
    private Shell<TempUpgrade> _upgrades = null;
    private Shell<TempAchivments> _achivments = null;

    public bool IsOpenMenu { get => _openMenu; set => _openMenu = value; }
    public string PathItem { get => _pathItem; set => _pathItem = value; }
    public string PathUp { get => _pathUp; set => _pathUp = value; }
    public string PathAchivments { get => _pathAchivments; set => _pathAchivments = value; }
    public Shell<TempItem> Items { get => _items; set => _items = value; }
    public Shell<TempUpgrade> Upgrades { get => _upgrades; set => _upgrades = value; }
    public Shell<TempAchivments> Achivments { get => _achivments; set => _achivments = value; }

    private Data() { }

    public static Data GetInstance() => _instance ??= new Data();

    public void Input(DataTemp dataTemp)
    {
        PlayerPrefs.SetString(_DBname, JsonUtility.ToJson(dataTemp));
        PlayerPrefs.Save();
    }

    public DataTemp Output()
    {
        if (_json == null)
        {
            _json = new DataTemp();
            if (PlayerPrefs.HasKey(_DBname))
                _json = JsonUtility.FromJson<DataTemp>(PlayerPrefs.GetString(_DBname));
        }

        return _json;
    }

    public void Clear()
    {
        if (PlayerPrefs.HasKey(_DBname))
            PlayerPrefs.DeleteKey(_DBname);
    }
}
