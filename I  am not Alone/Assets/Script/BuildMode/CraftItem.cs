using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountForCreare
{
    public string nameMaterial;
    public int Count;

    public CountForCreare (string name, int _count)
    {
        nameMaterial = name;
        Count = _count;
    }


}


public class CraftItem : MonoBehaviour
{
    public string whatMaterial;
    public List<CountForCreare> CountWoodForCreate;
    public bool Built;
    public Material[] materials;


    SwitchMode buildMode;
    Renderer rend;
    // Use this for initialization
    void Start ()
    {
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        DefaultOptions();
    }


    public void DefaultOptions ()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            rend = transform.GetChild(0).GetChild(i).GetComponent<Renderer>();
            rend.enabled = true;

            rend.sharedMaterial = materials[0];
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            buildMode.craft.Add(this.gameObject);
       
        }
        gameObject.SetActive(false);
        Built = false;
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player")
        {

            if (!Built)
            {
                for (int i = 0; i < transform.GetChild(0).childCount; i++)
                {
                    rend = transform.GetChild(0).GetChild(i).GetComponent<Renderer>();


                    rend.sharedMaterial = materials[1];
                    transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                    Built = true;
                    buildMode.craft.Remove(buildMode.craft.Find(obj => obj.name == gameObject.name));


                } 
            }

        }
    }
}
