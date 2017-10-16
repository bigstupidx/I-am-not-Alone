using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectScaled : MonoBehaviour
{
    public bool scale;
    public bool particleCircle;
    [Space(10)]
    public GameObject ParentObject;
    public BoxCollider boxcollider;
    public ParticleSystem system;
    CraftItem craft;
    public float DestroyObjecToFinish = 30;
    public float scaleX;
    public float scaleY;
    ParticleSystem.ShapeModule sh;
    public GameObject prefabEffect;

    private void Start ()
    {

        sh = system.shape;
        sh.radius = scaleX;
    }
    // Use this for initialization
    private void OnEnable ()
    {
        if (ParentObject)
        {
            craft = ParentObject.GetComponent<CraftItem>();
        }
        scaleX = 1;
        scaleY = 1;
        if (boxcollider)
        {
            boxcollider.size = new Vector3(1, 1, 1);
        }
        if (particleCircle)
        {
            sh = system.shape;

            sh.rotation = new Vector3(-90, 0, 0);
            sh.radius = scaleX;
        }

    }

    // Update is called once per frame
    void Update ()
    {
        if (scale)
        {
            scaleX = Mathf.MoveTowards(scaleX, 5.291637f, Time.deltaTime / DestroyObjecToFinish);
            scaleY = Mathf.MoveTowards(scaleY, 5.497761f, Time.deltaTime / DestroyObjecToFinish);
            transform.localScale = new Vector3(scaleX, scaleY, 0.01f);
            boxcollider.size = new Vector3(scaleX + 3, 1, scaleY + 3);
            if (scaleX == 5.291637f)
            {
                if (prefabEffect)
                {
                    prefabEffect.DestroyAPS();

                }
                craft.DefaultOptions();
                craft.DefaultForParticle();
                ParentObject.DestroyAPS();



            }
        }
        else if (particleCircle)
        {

            sh.radius = Mathf.MoveTowards(system.shape.radius, 5.497761f, Time.deltaTime / DestroyObjecToFinish);

            if (sh.radius == 5.497761f)
            {


                gameObject.DestroyAPS();



            }

        }



    }
}
