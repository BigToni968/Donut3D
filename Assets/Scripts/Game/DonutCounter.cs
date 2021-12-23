using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class DonutCounter : MonoBehaviour
{
    private Text[] _counters = null;
    private TextMeshProUGUI[] _countersPro = null;

    private Formatting _formatting = null;

    private Data _data = Data.GetInstance();
    private DataTemp _json = null;

    private float _second = 0;

    private void Awake()
    {
        _counters = GetComponentsInChildren<Text>();
        if (IsEmptyArray(_counters))
            _countersPro = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _formatting = new Formatting();
        _json = _data.Output();
    }

    private bool IsEmptyArray(Object[] arr)
    {
        if (arr != null)
            if (arr.Length > 0)
                return false;

        return true;
    }

    private void DrawCount(Text[] counterOne, TextMeshProUGUI[] counterTwo, double balance, int index)
    {
        if (!IsEmptyArray(counterOne))
            counterOne[index].text = _formatting.ToText(balance).Trim() + (index > 0 ? "/c" : null);
        else if (!IsEmptyArray(counterTwo))
            counterTwo[index].text = _formatting.ToText(balance).Trim() + (index > 0 ? "/c" : null);
    }

    private void DrawEmerald(Text[] counterOne, TextMeshProUGUI[] counterTwo, double balance, int index)
    {
        if (!IsEmptyArray(counterOne))
            counterOne[index].text = "+" + _formatting.ToText(balance).Trim() + (index > 0 ? "" : null);
        else if (!IsEmptyArray(counterTwo))
            counterTwo[index].text = "+" + _formatting.ToText(balance).Trim() + (index > 0 ? "" : null);
    }

    private void SetPersecond()
    {
        _second += Time.deltaTime;

        if (_second > 1)
        {
            _second = 0;
            _json.Balance += _json.PerSecond;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        DrawCount(_counters, _countersPro, _json.Balance, 0);
        DrawCount(_counters, _countersPro, _json.PerSecond, 1);
        DrawEmerald(_counters, _countersPro, _json.Emerald, 2);
        SetPersecond();
    }
}
