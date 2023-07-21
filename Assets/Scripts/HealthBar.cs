using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image planetHealth;
    [SerializeField] private float planetHealthReducer;
    [SerializeField] private float health = 100;
    private float currentHealth;

    private void OnEnable()
    {
        Asteroid.OnCrash += ReduceHP;
    }
    private void OnDisable()
    {
        Asteroid.OnCrash -= ReduceHP;
    }

    private void Start()
    {
        currentHealth = health;
    }

    private void ReduceHP()
    {
        Debug.Log("damaged");
        currentHealth -= 10;
        Debug.Log(currentHealth);
        StartCoroutine(ReduceFillAmountPlanet(currentHealth));
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("DeathScene");
        }
    }

    private IEnumerator ReduceFillAmountPlanet(float fillHealth)
    {
        float targetFillAmount = fillHealth / health;
        Debug.Log(targetFillAmount);
        while (planetHealth.fillAmount > targetFillAmount)
        {
            planetHealth.fillAmount = Mathf.Lerp(planetHealth.fillAmount, targetFillAmount, Time.deltaTime * planetHealthReducer);
            yield return null;
        }
    }
}
