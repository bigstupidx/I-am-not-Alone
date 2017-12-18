using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInside : MonoBehaviour
{

    public GameObject TwoFloour;
    public GameObject[] Roof;
    public BetweenFloor betweenFloor;
    public bool playerOutSide;
    public TrasnparentWall trasnparentWall;
    // Use this for initialization

    Camera myCamera;
    private void Start ()
    {
        myCamera = Camera.main.GetComponent<Camera>();
        myCamera.cullingMask &= ~(1 << 9);
        myCamera.cullingMask &= ~(1 << 11);
        myCamera.cullingMask |= (1 << 10);
        myCamera.farClipPlane = 75;
    }

    private void OnTriggerEnter (Collider other)
    {
        trasnparentWall.OffTransoarent = true;
        playerOutSide = true;
        if (other.CompareTag("Player"))
        {
            OffRenderAndCollider(true);
            for (int i = 0; i < Roof.Length; i++)
            {
                Roof[i].SetActive(false);
            }
            TwoFloour.SetActive(false);
            //        betweenFloor.RenderObjectToFloor(false, "Ignore Raycast");
            betweenFloor.RenderObjectToFloor(false);
        }
        if (other.CompareTag(Tags.AI))
        {
            other.GetComponent<Rigidbody>().detectCollisions = false;
        }
    }
    private void OnTriggerExit (Collider other)
    {

        playerOutSide = false;
        trasnparentWall.OffTransoarent = false;
        if (other.CompareTag("Player"))
        {
            OffRenderAndCollider(false);
            for (int i = 0; i < Roof.Length; i++)
            {
                Roof[i].SetActive(true);
            }

            TwoFloour.SetActive(true);




        }
        if (other.CompareTag(Tags.AI))
        {
            other.GetComponent<Rigidbody>().detectCollisions = true;
        }
    }

    void OffRenderAndCollider (bool off)
    {
        if (off)
        {
            myCamera.farClipPlane = 50;

            myCamera.cullingMask |= (1 << 9);
            myCamera.cullingMask |= (1 << 11);
        }
        else
        {
            myCamera.farClipPlane = 75;
          
            myCamera.cullingMask &= ~(1 << 11);
            myCamera.cullingMask &= ~(1 << 9);
        }
    }
}
