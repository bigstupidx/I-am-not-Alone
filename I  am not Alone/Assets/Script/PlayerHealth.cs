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
    public GameObject DualJoy;
    public Image damageImage;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Text healthText;
    bool damaged;
    WaveManager wave;
    GPS gps;
    // Use this for initialization
    void Start ()
    {
        wave = GameObject.Find("Spawner").GetComponent<WaveManager>();
        gps = GameObject.Find("Spawner").GetComponent<GPS>();
        HealthPlayer.fillAmount = CurHelth / MaxHealth;
        healthText.text = CurHelth.ToString();
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

    public void UpdateHealth (float health)
    {
        CurHelth += health;

        if (CurHelth > MaxHealth)
        {
            CurHelth = MaxHealth;

        }
        HealthPlayer.fillAmount = CurHelth / MaxHealth;
        healthText.text = CurHelth.ToString();
        DualJoy.SetActive(true);
    }

    public void HelthDamage (float damage)
    {


        CurHelth -= damage;

        damaged = true;
        HealthPlayer.fillAmount = CurHelth / MaxHealth;



        healthText.text = CurHelth.ToString();

        if (CurHelth > MaxHealth)
        {
            CurHelth = MaxHealth;

        }
        if (CurHelth <= 0)
        {
            CurHelth = 0;
            if (wave.levelWave == 0)
            {
                gps.GetAchiv(GPS.DieFirst);
            }

            imageGameOver.SetActive(true);
            DualJoy.SetActive(false);
            Time.timeScale = 0;







        }
    }

}
