using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasnparentWall : MonoBehaviour
{

    RaycastHit hit;
    Ray ray;
    GameObject player;
    private Camera camera;

    List<Transform> walls = new List<Transform>();
    public LayerMask layerMask;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update ()
    {

        //Find the direction from the camera to the player
        Vector3 direction = player.transform.position - camera.transform.position;

        //The magnitude of the direction is the distance of the ray
        float distance = direction.magnitude;

        //Raycast and store all hit objects in an array. Also include the layermaks so we only hit the layers we have specified
        RaycastHit[] hits = Physics.RaycastAll(camera.transform.position, direction, distance, layerMask);

        //Go through the objects
        for (int i = 0; i < hits.Length; i++)
        {
            Transform currentHit = hits[i].transform;

            //Only do something if the object is not already in the list
            if (!walls.Contains(currentHit))
            {
                //Add to list and disable renderer


                // currentHit.GetComponent<Renderer>().material.SetFloat("_BodyAlpha", 0.1f);

                if (!currentHit.CompareTag("Floor"))
                {
                    if (currentHit.GetComponent<Renderer>())
                    {
                        walls.Add(currentHit);
                        //currentHit.GetComponent<Renderer>().enabled = false;
                        if (currentHit.GetComponent<TransarentWallChangeMaterial>())
                        {
                            currentHit.GetComponent<TransarentWallChangeMaterial>().ChangeTransarentMaterial();
                            currentHit.GetComponent<TransarentWallChangeMaterial>().newActiveMaterial = true;
                        }
                        else
                        {
                            if (!currentHit.CompareTag("Things"))
                            {
                                currentHit.GetComponent<Renderer>().enabled = false; 
                            }
                        }
                    }
                }

            }
        }

        //clean the list of objects that are in the list but not currently hit.
        for (int i = 0; i < walls.Count; i++)
        {
            bool isHit = false;
            //Check every object in the list against every hit
            for (int j = 0; j < hits.Length; j++)
            {
                if (hits[j].transform == walls[i])
                {
                    isHit = true;
                    break;
                }
            }

            //If it is not among the hits
            if (!isHit)
            {
                //Enable renderer, remove from list, and decrement the counter because the list is one smaller now
                Transform wasHidden = walls[i];

                //  wasHidden.GetComponent<Renderer>().material.SetFloat("_BodyAlpha", 1f);
                if (wasHidden)
                {
                    if (wasHidden.GetComponent<Renderer>())
                    {
                        //   wasHidden.GetComponent<Renderer>().enabled = true;
                        if (wasHidden.GetComponent<TransarentWallChangeMaterial>())
                        {

                            wasHidden.GetComponent<TransarentWallChangeMaterial>().newActiveMaterial = false;
                        }
                        else
                        {
                            if (!wasHidden.CompareTag("Things"))
                            {
                                wasHidden.GetComponent<Renderer>().enabled = true; 
                            }
                        }
                        walls.RemoveAt(i);
                    }

                    i--;
                }

            }
        }
    }
}
