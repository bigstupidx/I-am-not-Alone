using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenFloor : MonoBehaviour
{
    public GameObject Floor;

    public bool FloorCraft;
    public GameObject WallTransparent;
    public SwitchMode switchMode;
    public Transform FurnitureFirst;
    public Transform FurnitureSecond;
    public GameObject FloorHowTrue;
    public Camera myCamera;
    private void Start ()
    {

        myCamera = Camera.main.GetComponent<Camera>();
        myCamera.cullingMask &= ~(1 << 9);
    }
    public void RenderObjectToFloor (bool b)
    {

        if (b)
        {
            myCamera.cullingMask |= (1 << 9);
        }
        else
        {
            myCamera.cullingMask &= ~(1 << 9);
        }

        for (int i = 0; i < FurnitureSecond.childCount; i++)
        {
            if (FurnitureSecond.GetChild(0).GetComponent<CraftItem>())
            {
                FurnitureSecond.GetChild(0).GetComponent<CraftItem>().ChangeActiveParams(b);
            }
        }


    }


    // Use this for initialization
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FloorCraft = true;
            WallTransparent.SetActive(false);
            switchMode.CheckInBuiltWalls(true);
            Floor.SetActive(true);
            FloorHowTrue.SetActive(true);

            RenderObjectToFloor(true);



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
            WallTransparent.SetActive(true);
            RenderObjectToFloor(false);
            //  RenderObjectToFloor(false, "Ignore Raycast");
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
