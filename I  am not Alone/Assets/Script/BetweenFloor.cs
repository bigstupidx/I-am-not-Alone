using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenFloor : MonoBehaviour
{
    public GameObject Floor;

    public bool FloorCraft;
    public SwitchMode switchMode;
    public Transform FurnitureFirst;
    public Transform FurnitureSecond;
    public GameObject FloorHowTrue;

    private void Start ()
    {


    }
    public void RenderObjectToFloor (bool b, string newLayer)
    {
        for (int i = 0; i < FurnitureSecond.childCount; i++)
        {

            if (FurnitureSecond.GetChild(i).CompareTag("CraftFromMenu") || FurnitureSecond.GetChild(i).CompareTag("Things"))
            {
                FurnitureSecond.GetChild(i).gameObject.layer = LayerMask.NameToLayer(newLayer);
            }
            if (FurnitureSecond.GetChild(i).childCount == 0)
            {
                FurnitureSecond.GetChild(i).GetComponent<Renderer>().enabled = b;
            }
            else
            {
                if (FurnitureSecond.GetChild(i).CompareTag("CraftFromMenu") || FurnitureSecond.GetChild(i).CompareTag("CraftMode"))
                {
                    for (int l = 0; l < FurnitureSecond.GetChild(i).GetChild(0).childCount; l++)
                    {
                        if (FurnitureSecond.GetChild(i).GetChild(0).GetChild(l).GetComponent<Renderer>())
                        {

                            FurnitureSecond.GetChild(i).GetChild(0).GetChild(l).GetComponent<Renderer>().enabled = b;
                        }
                    }
                }
                else
                {

                    if (FurnitureSecond.GetChild(i).GetComponent<Renderer>())
                    {
                        FurnitureSecond.GetChild(i).GetComponent<Renderer>().enabled = b;
                    }
                    for (int l = 0; l < FurnitureSecond.GetChild(i).childCount; l++)
                    {
                        if (FurnitureSecond.GetChild(i).GetChild(l).GetComponent<Renderer>())
                        {

                            FurnitureSecond.GetChild(i).GetChild(l).GetComponent<Renderer>().enabled = b;
                        }
                    }
                }
            }




        }
    }


    // Use this for initialization
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FloorCraft = true;
            switchMode.CheckInBuiltWalls(true);
            Floor.SetActive(true);
            FloorHowTrue.SetActive(true);
            for (int i = 0; i < FurnitureSecond.childCount; i++)
                RenderObjectToFloor(true, "Default");



        }
        if (other.CompareTag("Things"))
        {

            other.transform.SetParent(FurnitureSecond);
        }
        if (other.CompareTag("CraftFromMenu"))
        {

            other.transform.SetParent(FurnitureSecond);
        }
        if (other.CompareTag("CraftMode"))
        {
            Debug.Log(true);
            other.transform.SetParent(FurnitureSecond);
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FloorCraft = false;
            switchMode.CheckInBuiltWalls(true);
            Floor.SetActive(false);

            RenderObjectToFloor(false, "Ignore Raycast");
            FloorHowTrue.SetActive(false);
        }
        if (other.CompareTag("Things"))
        {

            other.transform.SetParent(FurnitureFirst);
        }
        if (other.CompareTag("CraftFromMenu"))
        {

            other.transform.SetParent(FurnitureFirst);
        }
        if (other.CompareTag("CraftMode"))
        {

            other.transform.SetParent(FurnitureFirst);
        }
    }
}
