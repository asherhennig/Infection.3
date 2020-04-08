using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class multiLanguage : MonoBehaviour
{

    public Dropdown dropdown;
    public Toggle toggle;

    // Use this for initialization
    void Start()
    {
        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
        list.Add(new Dropdown.OptionData("English", null));
        list.Add(new Dropdown.OptionData("Français", null));
        list.Add(new Dropdown.OptionData("German", null));
        dropdown.AddOptions(list);
        dropdown.onValueChanged.AddListener(OnValueChange);
        toggle.onValueChanged.AddListener(OnAutoLanguage);

        toggle.isOn = true;
    }

    void OnAutoLanguage(bool v)
    {
        Jun_MultiLanguage.AutoLanguage(v);
    }

    void OnValueChange(int v)
    {
        switch (v)
        {
            case 0:
                Jun_MultiLanguage.SetSystemLanguage(SystemLanguage.English);
                break;

            case 1:
                Jun_MultiLanguage.SetSystemLanguage(SystemLanguage.French);
                break;

            case 2:
                Jun_MultiLanguage.SetSystemLanguage(SystemLanguage.German);
                break;
        }
        toggle.isOn = false;
    }
}
