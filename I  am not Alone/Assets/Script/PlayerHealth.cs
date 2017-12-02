using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100.0f;
    public float CurHelth = 100.0f;
    public Image HealthPlayer;
    public GameObject imageGameOver;
    public Image damageImage;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    bool damaged;
    // Use this for initialization
    void Start ()
    {
        HealthPlayer.fillAmount = CurHelth / MaxHealth;

    }

    private void Update ()
    {
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, 5.0f * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }

    public void HelthDamage (float damage)
    {


        CurHelth -= damage;

        damaged = true;
        HealthPlayer.fillAmount = CurHelth / MaxHealth;

        HealthPlayer.fillAmount = CurHelth / MaxHealth;



        if (CurHelth > MaxHealth)
        {
            CurHelth = MaxHealth;

        }
        if (CurHelth <= 0)
        {
            CurHelth = 0;

            if (transform.CompareTag("Player"))
            {
                imageGameOver.SetActive(true);

                Time.timeScale = 0;

            }





        }
    }

}
