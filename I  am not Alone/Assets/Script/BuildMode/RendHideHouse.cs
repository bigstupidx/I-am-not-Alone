using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendHideHouse : MonoBehaviour
{

    public List<TransarentWallChangeMaterial> MainRenderHouse = new List<TransarentWallChangeMaterial>();



    // Use this for initialization
    void Start ()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<TransarentWallChangeMaterial>())
            {
                MainRenderHouse.Add(transform.GetChild(i).GetComponent<TransarentWallChangeMaterial>());



            }
            else
            {

                if (transform.GetChild(i).childCount != 0)
                {
                    for (int l = 0; l < transform.GetChild(i).childCount; l++)
                    {




                        if (transform.GetChild(i).GetChild(l).GetComponent<TransarentWallChangeMaterial>())
                        {
                            MainRenderHouse.Add(transform.GetChild(i).GetChild(l).GetComponent<TransarentWallChangeMaterial>());
                        }


                    }
                }
                else
                {
                    if (transform.GetChild(i).GetComponent<TransarentWallChangeMaterial>())
                    {
                        MainRenderHouse.Add(transform.GetChild(i).GetComponent<TransarentWallChangeMaterial>());



                    }
                }
            }


        }
    }


}
