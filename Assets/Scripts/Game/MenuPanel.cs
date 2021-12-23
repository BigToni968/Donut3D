using UnityEngine.UI;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    [Header("UI панели.")]
    [Tooltip("Имена панелей должны быть идентичными именам кнопок.")]
    [SerializeField] private GameObject[] _panels = null;

    private Button[] _buttons = null;
    private Data data = Data.GetInstance();

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();

        foreach (Button tmp in _buttons)
            tmp.onClick.AddListener(() => ClickListener(tmp));
    }

    private void SwitchPanel(string namePanel)
    {
        if (_panels != null && _panels.Length > 0)
            foreach (GameObject tmp in _panels)
                if (!tmp.name.ToLower().Equals(namePanel.ToLower()))
                    tmp.SetActive(false);
                else
                    tmp.SetActive(!tmp.activeSelf);
    }

    private bool iIsAllInvisible()
    {
        if (_panels != null && _panels.Length > 0)
            foreach (GameObject tmp in _panels)
                if (tmp.activeSelf)
                    return true;

        return false;
    }



    private void ClickListener(Button but)
    {
        SwitchPanel(but.name.ToLower());
        data.IsOpenMenu = iIsAllInvisible();
    }
}
