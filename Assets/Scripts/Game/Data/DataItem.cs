[System.Serializable]
public class DataItem
{
    private string _id = "";
    private double _price = 100;
    private double _income = 10;
    private int _countBuilds = 1;
    private int _levelUpdate = 0;
    private int _maxLevelUpdate = 0;
    private float _precent = 1f;

    public string Id { get => _id; set => _id = value; }
    public double Price { get => _price; set => _price = value; }
    public double Income { get => _income; set => _income = value; }
    public int CountBuilds { get => _countBuilds; set => _countBuilds = value; }
    public int LevelUpdate { get => _levelUpdate; set => _levelUpdate = value; }
    public int MaxLevelUpdate { get => _maxLevelUpdate; set => _maxLevelUpdate = value; }
    public float Percent { get => _precent; set => _precent = value; }
}
