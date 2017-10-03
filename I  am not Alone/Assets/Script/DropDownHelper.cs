using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownHelper : MonoBehaviour
{

    public bool WithOutDroDown;

    public Dropdown dropdown;
    public Text labelDropDown;
    public Sprite[] flags;
    int dropdownValue;
    DbGame db;
    string Language;
    // Use this for initialization
    void Start ()
    {
        db = GetComponent<DbGame>();
        db.OpenDB("DBGame.db");

        if (!WithOutDroDown)
        {

            dropdown.ClearOptions();
            string l = db.GetLanguage(labelDropDown.text);
            List<Dropdown.OptionData> flagsItem = new List<Dropdown.OptionData>();
            foreach (var flag in flags)
            {
                var flagOption = new Dropdown.OptionData(flag.name, flag);
                flagsItem.Add(flagOption);

            }
            dropdown.AddOptions(flagsItem);
            labelDropDown.text = db.GetLanguage(labelDropDown.text);

            dropdown.value = flagsItem.FindIndex(x => x.text == db.GetLanguage(labelDropDown.text));


        }
        Lean.Localization.LeanLocalization.CurrentLanguage = db.GetLanguage(Language);


        //   LocalizationService.Instance.Localization = labelDropDown.text;

    }


    public void ChangeLanguage (Dropdown drop)
    {
        db.UpdateLanguage(labelDropDown.text);






    }
}
