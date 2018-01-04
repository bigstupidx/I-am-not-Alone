using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoneCameraMovving : MonoBehaviour
{
    public float smooth = 1.5f;         // The relative speed at which the camera will catch up.
    public Transform cameraToPOs;
    public Transform attaackTransform;
    private Transform player;           // Reference to the player's transform.
    private Vector3 relCameraPos;       // The relative position of the camera from the player.
    private float relCameraPosMag;      // The distance of the camera from the player.
    private Vector3 newPos;             // The position the camera is trying to reach.
    private SelectionWeaponForPC selection;
    Transform target;

    Vector3 cameraToPosMoving;
    public HouseInside houseInside;
    Transform cameraTransform;
    private bool cameraFixPos;
    public bool m_changeCamera;
    public Toggle cameraTog;
    public Vector3 defaultDistance = new Vector3(0, 16, 12);
    public float distanceDamp = 10;

    public static void CameraSaveSetBool (string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
    }

    public static bool CameraSaveGetBool (string key)
    {
        int value = PlayerPrefs.GetInt(key);

        if (value == 1)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    void Awake ()
    {
        cameraTransform = GetComponent<Transform>();
        // Setting up the reference.
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        selection = GameObject.Find("WeaponController").GetComponent<SelectionWeaponForPC>();
        // Setting the relative position as the initial relative position of the camera in the scene.
        target = player;
        relCameraPos = cameraTransform.position - target.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;

        cameraTog.isOn = CameraSaveGetBool("Camera");
        m_changeCamera = cameraTog.isOn;
        defaultDistance = cameraToPOs.position - target.position;
        PlayerPrefs.Save();
    }
    public void ChangeCamera (Toggle TOG)
    {
        m_changeCamera = TOG.isOn;
        cameraTog.isOn = m_changeCamera;
        CameraToPOsition();
        //    defaultDistance = cameraToPOs.position;
    }

    private void OnApplicationPause (bool pause)
    {
        CameraSaveSetBool("Camera", m_changeCamera);


        PlayerPrefs.Save();
    }
    private void OnApplicationQuit ()
    {
        CameraSaveSetBool("Camera", m_changeCamera);

        PlayerPrefs.Save();
    }

    void Update ()
    {

        if (!selection.Fire1)
        {
            target = player;

            if (m_changeCamera)
            {
                relCameraPos = Vector3.Lerp(relCameraPos, cameraToPOs.position - target.position, 0.2f * Time.deltaTime);


                cameraFixPos = true;
            }
        }
        else
        {

            if (m_changeCamera)
            {
                if (cameraFixPos)
                {
                    relCameraPos = cameraTransform.position - target.position;

                    relCameraPosMag = relCameraPos.magnitude - 0.5f;
                    cameraFixPos = false;
                }
            }
            target = attaackTransform;
        }



        //// The abovePos is directly above the player at the same distance as the standard position.
        //Vector3 abovePos = target.position + Vector3.up * relCameraPosMag;

        ////// An array of 5 points to check if the camera can see the player.
        //Vector3[] checkPoints = new Vector3[3];

        ////// The first is the standard position of the camera.
        //checkPoints[0] = standardPos;

        ////// The next three are 25%, 50% and 75% of the distance between the standard position and abovePos.
        //checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
        //checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.50f);


        ////// Run through the check points...
        //for (int i = 0; i < checkPoints.Length; i++)
        //{
        //    // ... if the camera can see the player...
        //    if (ViewingPosCheck(checkPoints[i]))
        //        // ... break from the loop.
        //        break;
        //}


        if (!m_changeCamera)
        {

            Vector3 toPos = target.position + defaultDistance;
            Vector3 curos = Vector3.Lerp(cameraTransform.position, toPos, distanceDamp * Time.deltaTime);
            cameraTransform.position = curos;
            // The standard position of the camera is the relative position of the camera from the player.

        }
        else
        {
            Vector3 standardPos = target.position + relCameraPos;
            cameraTransform.position = Vector3.Slerp(cameraTransform.position, standardPos, smooth * Time.deltaTime);


            // Make sure the camera is looking at the player.

        }

        SmoothLookAt();
    }


    //bool ViewingPosCheck (Vector3 checkPos)
    //{
    //    RaycastHit hit;


    //    // If a raycast from the check position to the player hits something...
    //    if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))


    //        if (hit.transform != target)
    //        {
    //            //   newViewCamera = false;
    //            // This position isn't appropriate.
    //            return false;
    //        }



    //    // If we haven't hit anything or we've hit the player, this is an appropriate position.
    //    newPos = checkPos;
    //    return true;
    //}

    public void CameraToPOsition ()
    {

        if (m_changeCamera)
        {
            if (target)
            {
                relCameraPos = cameraToPOs.position - target.position;
            }
            relCameraPosMag = relCameraPos.magnitude - 0.5f;
        }
        else
        {
            defaultDistance = cameraToPOs.position - target.position;
        }


    }

    void SmoothLookAt ()
    {
        // Create a vector from the camera towards the player.
        Vector3 relPlayerPosition = target.position - cameraTransform.position;

        // Create a rotation based on the relative position of the player being the forward vector.
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);

        // Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }
}