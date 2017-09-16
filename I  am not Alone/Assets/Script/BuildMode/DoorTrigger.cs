using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DoorTrigger : MonoBehaviour
{
    private SwitchMode buildMode;
    Animator animator;
    public NavMeshObstacle obstacle;
    public CraftItem craftItem;
    // Use this for initialization
    void Start ()
    {
        buildMode = GameObject.Find("BuildController").GetComponent<SwitchMode>();
        animator = transform.parent.GetComponent<Animator>();
        obstacle =transform.parent.GetComponent<NavMeshObstacle>();

    }

    private void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (!craftItem.Built)
            {
                buildMode.buttonAction.gameObject.SetActive(true);
                buildMode.Door = animator;
            }
            else
            {
                buildMode.buttonAction.gameObject.SetActive(false);
            }


        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {


            buildMode.buttonAction.gameObject.SetActive(false);
            buildMode.Door = null;



        }
    }
}
