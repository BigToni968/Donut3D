using SimpleLocalization;
using UnityEngine.UI;
using UnityEngine;

public class DynamicText : MonoBehaviour
{
    [SerializeField] private Text someText = null;
    int i = 0;
    private void Start()
    {
        Localizator.Initialize();
    }

    public void SetText()
    {
        i++;
       Localizator.ChangeLanguage(i);
    }
}