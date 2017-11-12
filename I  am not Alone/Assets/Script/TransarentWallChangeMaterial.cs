using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransarentWallChangeMaterial : MonoBehaviour
{

    public bool newActiveMaterial;
    public bool standartMaterial;
    [Space(10)]

    Material DefaultMaterial;
    public Material Transparentmaterial;

    Renderer rend;
    public float trasparentParams;
    // Use this for initialization
    //void Start ()
    //{
    //    rend = GetComponent<Renderer>();
    //    rend.enabled = true;
    //    DefaultMaterial = rend.material;
    //    trasparentParams = 128;

    //}


    //private void Update ()
    //{

    //    if (!standartMaterial)
    //    {
    //        if (newActiveMaterial)
    //        {

    //            trasparentParams = Mathf.MoveTowards(trasparentParams, 5.0f, Time.deltaTime * 100);
    //            rend.material.SetFloat("_BumpAmt", trasparentParams);

    //        }
    //        else
    //        {
    //            trasparentParams = Mathf.MoveTowards(trasparentParams, 128, Time.deltaTime * 25);
    //            rend.material.SetFloat("_BumpAmt", trasparentParams);
    //            if (trasparentParams == 128)
    //            {
    //                ChangeDefaultMaterial();
    //            }
    //        }
    //    }
    //    else
    //    {
    //   //     rend.material.SetTexture("_MainTex", rend.material.GetTexture(""));
    //    }

    //}

    //public void ChangeDefaultMaterial ()
    //{

    //    rend.sharedMaterial = DefaultMaterial;
    //}
    //public void ChangeTransarentMaterial ()
    //{
    //    trasparentParams = 128;
    //    rend.sharedMaterial = Transparentmaterial;

    //}


}