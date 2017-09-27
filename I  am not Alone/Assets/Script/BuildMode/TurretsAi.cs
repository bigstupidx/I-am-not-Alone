using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretsAi : MonoBehaviour
{
    public float damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float range = 100f;                      // The distance the gun can fire.
    public GameObject HeadTurrets;
    public bool Shot;
    // A layer mask so the raycast only hits things on the shootable layer.

    public LineRenderer gunLine;                           // Reference to the line renderer.
    public AudioSource gunAudio;                           // Reference to the audio source.
    public Light gunLight;                                 // Reference to the light component.
    public Transform Zombie;

    public LayerMask layers;
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
    float timer;                                    // A timer to determine when to fire.
    Ray shootRay = new Ray();                       // A ray from the gun end forwards.
    RaycastHit shootHit;
    Ray Headray = new Ray();                       // A ray from the gun end forwards.
    RaycastHit headHit; // A raycast hit to get information about what was hit.

    Vector3 m_lastPOsition = Vector3.zero;
    Quaternion m_lookRotation;
    void Awake ()
    {
        // Create a layer mask for the Shootable layer.


        // Set up the references.

        //gunLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
        //gunLight = GetComponent<Light>();
        //faceLight = GetComponentInChildren<Light> ();

    }
    private void Start ()
    {

    }

    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        if (Zombie)
        {

            if (m_lastPOsition != Zombie.transform.position)
            {
                m_lastPOsition = Zombie.position;
                m_lookRotation = Quaternion.LookRotation(m_lastPOsition - HeadTurrets.transform.position);

            }

            if (HeadTurrets.transform.rotation != m_lookRotation)
            {
                HeadTurrets.transform.rotation = Quaternion.RotateTowards(HeadTurrets.transform.rotation, m_lookRotation, 100 * Time.deltaTime);

            }
            Headray.origin = HeadTurrets.transform.GetChild(1).transform.position;
            Headray.direction = HeadTurrets.transform.GetChild(1).transform.forward;
            Debug.DrawRay(Headray.origin, Headray.direction * 500, Color.red);
            if (Physics.Raycast(Headray, out headHit, 500))
            {
              
                if (headHit.transform.CompareTag("AI"))
                {

                    Shot = true;



                }
                else
                {
                    Shot = false;
                }

            }
        }
        else
        {
            Shot = false;

        }

        // If the Fire1 button is being press and it's time to fire...
        if (Shot && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            // ... shoot the gun.
            Shoot();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }
    }


    public void DisableEffects ()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;

        gunLight.enabled = false;
    }


    void Shoot ()
    {
        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        gunAudio.Play();

        // Enable the lights.
        gunLight.enabled = true;




        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, HeadTurrets.transform.GetChild(1).position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = HeadTurrets.transform.GetChild(1).position;
        shootRay.direction = HeadTurrets.transform.GetChild(1).forward;


        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, 500))
        {

            if (shootHit.transform.CompareTag("AI"))
            {
                shootHit.transform.GetComponent<Health>().HelthDamage(damagePerShot, true);




            }
            if (shootHit.transform.CompareTag("Player"))
            {
                shootHit.transform.GetComponent<Health>().HelthDamage(damagePerShot,false);




            }


            gunLine.SetPosition(1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("AI"))
        {
            int l = Random.Range(0, 3);
            if (l == 0)
            {
         
                other.GetComponent<ZombieLevel1>().newTraget = HeadTurrets.transform.parent.parent;
            }





        }
    }
    private void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("AI"))
        {

   
            Zombie = other.transform;




        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("AI"))
        {

            Zombie = null;




        }
    }
}

