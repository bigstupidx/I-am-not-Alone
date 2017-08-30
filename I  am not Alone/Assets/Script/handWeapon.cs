using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handWeapon : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {

            anim.SetBool("attack", false);
        }
        if (Input.GetMouseButtonDown(0))

        {
            anim.SetBool("attack", true);



            //  Debug.Log("play");



        }
    }
}
