using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Formatting
{
    private TextAsset _fileAbbreviations = null;
    private List<string> _abbreviations = null;
    public Formatting()
    {
        if (_fileAbbreviations == null)
            _fileAbbreviations = Resources.Load<TextAsset>("TextFiles/Abbreviations") as TextAsset;

        _abbreviations = new List<string>(_fileAbbreviations.ToString().Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries));
    }

    public string ToText(double tmp)
    {
        int n = 0;
        string typeformat = tmp >= 1e3 ? "{0:0.000}{1}" : "{0:0}{1}";

        while (n + 1 < _abbreviations.Count && tmp >= 1e3)
        {
            tmp /= 1e3;
            n++;
        }

        typeformat = string.Format(typeformat, tmp, _abbreviations[n]);

        return typeformat.Contains(",") ? typeformat.Replace(",", " ") : typeformat;
    }

    public string ToCellSave(BigInteger tmp)
    {
        string strTemp = tmp.ToString();

        if (strTemp.Length >= 4)
            strTemp = (strTemp.Substring(0, 3)) + "e" + (strTemp.Length - 3).ToString();

        return strTemp;
    }
}
