using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class ActivatorTraning : MonoBehaviour
{
    public PlayableDirector m_director;
    public GameObject[] ActiveGameObject;
    bool active = true;
    // Use this for initialization

    private void OnTriggerEnter (Collider other)
    {
        if (active)
        {
            if (other.CompareTag(Tags.player))
            {
                for (int i = 0; i < ActiveGameObject.Length; i++)
                {
                    ActiveGameObject[i].SetActive(true);
                }
                m_director.Play();
                active = false;
            } 
        }
    }
}
