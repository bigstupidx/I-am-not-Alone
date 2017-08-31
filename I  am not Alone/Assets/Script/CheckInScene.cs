using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInScene : MonoBehaviour
{

 
    public Transform grid;
    [HideInInspector]
    public List<string> sceneBought = new List<string>();

    DbGame db;
    void Start ()
    {
        db = GetComponent<DbGame>();
        db.OpenDB("DBGame.db");
        db.GetSceneBought();
        CheckIn();
    }

    public void CheckIn ()
    {

        for (int i = 0; i < sceneBought.Count; i++)
        {
            if (sceneBought[i] != null)
            {
                grid.Find(sceneBought[i]).Find("ImageLocked").gameObject.SetActive(false);
            }
        }

    }
}
