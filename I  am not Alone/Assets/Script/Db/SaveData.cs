using BayatGames.SaveGameFree;

using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree.Serializers;
public class SaveData : MonoBehaviour
{
    [System.Serializable]
    public class SnecePArams
    {

        public List<string> scene = new List<string>();
    }
    [System.Serializable]
    public class PlayerParams
    {

        public string Money;
        public string Language;
    }
    [System.Serializable]
    public class PlayerItemParams
    {

        public List<string> NameWeapon = new List<string>();
        public List<int> LevelWeapon = new List<int>();
        public List<int> Category = new List<int>();
    }
    public class PlayerCraftItem
    {

        public List<string> NameCraft = new List<string>();
        public List<int> LevelItem = new List<int>();

    }
    public class CraftItemInventory
    {

        public List<string> NameCraft = new List<string>();
        public List<int> LevelItem = new List<int>();

    }
    public class ItemWeaponInventory
    {

        public List<string> NameWeapon = new List<string>();
        public List<int> LevelWeapon = new List<int>();

    }
    public bool clearAll;

    public PlayerItemParams _playerItemParams;
    public SnecePArams snecePArams;
    public PlayerParams _playerParams;
    public PlayerCraftItem _playerCraftItem;
    public CraftItemInventory _craftItemInventory;
    public ItemWeaponInventory _itemWeaponInventory;
    CheckInScene checkIn;
    public static string identifier_snecePArams = "snecePArams";
    public static string identifier_playerParams = "_playerParams";
    public static string identifier_playerItemParams = "_playerItemParams";
    public static string identifier_playerCraftItem = "_playerCraftItem";
    public static string identifier_craftItemInventory = "_craftItemInventory";
    public static string identifier_itemWeaponInventory = "_itemWeaponInventory";
    MyMainMenu mainmenu;
    CheckInWeaponAndCraft checkInWeaponAndCraft;


    private void Awake ()
    {
        if (clearAll)
        {
            SaveGame.Clear();
        }

    }
    public void GetSceneBought ()
    {
        checkIn = GetComponent<CheckInScene>();

        snecePArams = SaveGame.Load<SnecePArams>(
    identifier_snecePArams,
    new SnecePArams(),
   new SaveGameJsonSerializer());


        for (int i = 0; i < snecePArams.scene.Count; i++)
        {
            checkIn.sceneBought.Add(snecePArams.scene[i]);
        }


    }

    public void InsertDBSceneName (string sceneName)
    {

        snecePArams.scene.Add(sceneName);

        SaveGame.Save<SnecePArams>(identifier_snecePArams, snecePArams, new SaveGameJsonSerializer());


    }
    public void GetMoney ()
    {
        _playerParams = SaveGame.Load<PlayerParams>(
identifier_playerParams,
new PlayerParams(),
new SaveGameJsonSerializer());


        if (_playerParams.Money == null)
        {
            UpdateMoney("1500");
        }
        if (mainmenu = GetComponent<MyMainMenu>())
        {
            mainmenu = GetComponent<MyMainMenu>();
            mainmenu.myMoney.text = _playerParams.Money;

        }
        else if (checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>())
        {
            checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>();
            checkInWeaponAndCraft.MyMoney.text = _playerParams.Money;
        }








    }
    public void UpdateMoney (string money)
    {

        _playerParams.Money = money;
        SaveGame.Save<PlayerParams>(identifier_playerParams, _playerParams, new SaveGameJsonSerializer());

    }
    public void GetWeaponBought ()
    {

        checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>();

        _playerItemParams = SaveGame.Load<PlayerItemParams>(
identifier_playerItemParams,
new PlayerItemParams(),
new SaveGameJsonSerializer());

        if (_playerItemParams.NameWeapon.Count == 0)
        {
            InsertDBWeapon("Drobovik", 1, 1);
            SaveGame.Save<PlayerItemParams>(identifier_playerItemParams, _playerItemParams, new SaveGameJsonSerializer());
            _playerItemParams = SaveGame.Load<PlayerItemParams>(
identifier_playerItemParams,
new PlayerItemParams(),
new SaveGameJsonSerializer());
        }



        for (int i = 0; i < _playerItemParams.NameWeapon.Count; i++)
        {
            checkInWeaponAndCraft.WeaponBought.Add(new ParamsDbBoughtWeaponAndCraftItem(_playerItemParams.NameWeapon[i], _playerItemParams.LevelWeapon[i], _playerItemParams.Category[i]));
        }




    }
    public void GetCraftItemBought ()
    {


        checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>();



        _playerCraftItem = SaveGame.Load<PlayerCraftItem>(
identifier_playerCraftItem,
new PlayerCraftItem(),
new SaveGameJsonSerializer());
        for (int i = 0; i < _playerCraftItem.NameCraft.Count; i++)
        {

            checkInWeaponAndCraft.CraftItemBought.Add(new ParamsDbBoughtWeaponAndCraftItem(_playerCraftItem.NameCraft[i], _playerCraftItem.LevelItem[i], 0));
        }




    }
    public string GetLanguage (string idLanguage)
    {
        _playerParams = SaveGame.Load<PlayerParams>(
identifier_playerParams,
new PlayerParams(),
new SaveGameJsonSerializer());


        idLanguage = _playerParams.Language;
        return idLanguage;

    }

    public void UpdateLanguage (string idLanguage)
    {


        _playerParams.Language = idLanguage;
        SaveGame.Save<PlayerParams>(identifier_playerParams, _playerParams, new SaveGameJsonSerializer());


    }
    public void InsertDBWeapon (string WeaponName, int level, int category)
    {
        _playerItemParams.NameWeapon.Add(WeaponName);
        _playerItemParams.LevelWeapon.Add(level);
        _playerItemParams.Category.Add(category);

        SaveGame.Save<PlayerItemParams>(identifier_playerItemParams, _playerItemParams, new SaveGameJsonSerializer());







    }
    public void UpdateDBWeapon (string WeaponName, int level)
    {

        int l = _playerItemParams.NameWeapon.FindIndex(x => x.Equals(WeaponName));

        _playerItemParams.NameWeapon.RemoveAt(l);
        _playerItemParams.LevelWeapon.RemoveAt(l);
        _playerItemParams.Category.RemoveAt(l);


        _playerItemParams.NameWeapon.Add(WeaponName);
        _playerItemParams.LevelWeapon.Add(level);
        _playerItemParams.Category.Add(0);
        SaveGame.Save<PlayerItemParams>(identifier_playerItemParams, _playerItemParams, new SaveGameJsonSerializer());

    }

    public void GetInventoryForMenu ()
    {
        _itemWeaponInventory = SaveGame.Load<ItemWeaponInventory>(
identifier_itemWeaponInventory,
new ItemWeaponInventory(),
new SaveGameJsonSerializer());


        _craftItemInventory = SaveGame.Load<CraftItemInventory>(
identifier_craftItemInventory,
new CraftItemInventory(),
new SaveGameJsonSerializer());
        if (mainmenu = GetComponent<MyMainMenu>())
        {
            if (mainmenu.PlayButton)
            {
                if (_itemWeaponInventory.NameWeapon.Count != 0)
                {
                    mainmenu.PlayButton.interactable = true;
                }
                else
                {
                    mainmenu.PlayButton.interactable = false;
                }
            }

        }
    }

    public void GetInventory ()
    {
        _itemWeaponInventory = SaveGame.Load<ItemWeaponInventory>(
identifier_itemWeaponInventory,
new ItemWeaponInventory(),
new SaveGameJsonSerializer());


        _craftItemInventory = SaveGame.Load<CraftItemInventory>(
identifier_craftItemInventory,
new CraftItemInventory(),
new SaveGameJsonSerializer());

        if (checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>())
        {
            for (int i = 0; i < _itemWeaponInventory.NameWeapon.Count; i++)
            {
                checkInWeaponAndCraft.addItemCraftWeapon.Add(_itemWeaponInventory.NameWeapon[i]);
            }
            for (int i = 0; i < _craftItemInventory.NameCraft.Count; i++)
            {
                checkInWeaponAndCraft.addItemCraft.Add(_craftItemInventory.NameCraft[i]);
            }
        }

    }

    public void InsertInventoryWeapon (string WeaponName, int level)
    {
        _itemWeaponInventory.NameWeapon.Add(WeaponName);
        _itemWeaponInventory.LevelWeapon.Add(level);

        SaveGame.Save<ItemWeaponInventory>(identifier_itemWeaponInventory, _itemWeaponInventory, new SaveGameJsonSerializer());
    }

    public void DeleteInventoryWeapon (string WeaponName)
    {
        _itemWeaponInventory = SaveGame.Load<ItemWeaponInventory>(
identifier_itemWeaponInventory,
new ItemWeaponInventory(),
new SaveGameJsonSerializer());
        int l = _itemWeaponInventory.NameWeapon.FindIndex(x => x.Equals(WeaponName));
        _itemWeaponInventory.NameWeapon.RemoveAt(l);
        _itemWeaponInventory.LevelWeapon.RemoveAt(l);
        SaveGame.Save<ItemWeaponInventory>(identifier_itemWeaponInventory, _itemWeaponInventory, new SaveGameJsonSerializer());
    }
    public void InsertInventoryItemCraft (string WeaponName, int level)
    {
        _craftItemInventory.NameCraft.Add(WeaponName);
        _craftItemInventory.LevelItem.Add(level);

        SaveGame.Save<CraftItemInventory>(identifier_craftItemInventory, _craftItemInventory, new SaveGameJsonSerializer());
    }
    public void DeleteInventoryItemCraft (string itemCraft)
    {
        _craftItemInventory = SaveGame.Load<CraftItemInventory>(
identifier_craftItemInventory,
new CraftItemInventory(),
new SaveGameJsonSerializer());
        int l = _craftItemInventory.NameCraft.FindIndex(x => x.Equals(itemCraft));
        _craftItemInventory.NameCraft.RemoveAt(l);
        _craftItemInventory.LevelItem.RemoveAt(l);
        SaveGame.Save<CraftItemInventory>(identifier_craftItemInventory, _craftItemInventory, new SaveGameJsonSerializer());
    }
    public void InsertDBCraft (string NameCraft, int level)
    {

        _playerCraftItem.NameCraft.Add(NameCraft);
        _playerCraftItem.LevelItem.Add(level);
        SaveGame.Save<PlayerCraftItem>(identifier_playerCraftItem, _playerCraftItem, new SaveGameJsonSerializer());
    }

    public void UpdateDBCraft (string NameCraft, int level)
    {


        int l = _playerCraftItem.NameCraft.FindIndex(x => x.Equals(NameCraft));

        _playerCraftItem.NameCraft.RemoveAt(l);
        _playerCraftItem.LevelItem.RemoveAt(l);


        _playerCraftItem.NameCraft.Add(NameCraft);
        _playerCraftItem.LevelItem.Add(level);
        SaveGame.Save<PlayerCraftItem>(identifier_playerCraftItem, _playerCraftItem, new SaveGameJsonSerializer());



    }
}
