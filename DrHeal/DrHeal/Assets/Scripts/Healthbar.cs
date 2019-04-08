using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Healthbar : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public Slider healthbar;

    // Use this for initialization
    void Start()
    {

        MaxHealth = 20f;

        CurrentHealth = MaxHealth;
        healthbar.value = CalculateHealth();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(1);
    }
    public void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        healthbar.value = CalculateHealth();
        if (CurrentHealth<= 0)
            Die();
    }

    float CalculateHealth()
    {
        return CurrentHealth /MaxHealth;
    }

    void Die()
    {
        CurrentHealth = 0;
        Debug.Log("oh you dead");
       // SceneManager.LoadScene("GameOver");

    }
} 