using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class SurvivalStats : MonoBehaviour
{
    [Foldout("Health")] [Range(0.0f, 100.0f)]
    public float curHealth = 100f, maxHealth = 100f;
    [Foldout("Hunger")] [Range(0.0f, 100.0f)]
    public float curHunger = 100f, maxHunger = 100f;
    [Foldout("Oxygen")] [Range(0.0f, 100.0f)]
    public float curOxygen = 100f, maxOxygen = 100f;
    [Foldout("Stats multipliers")] [Range(0, 25)]
    public int addOxygenStep, decreaseOxygenStep, addHungerStep, decreaseHungerStep;

    public static bool decreasingOxygen = true;
    
    [ReadOnly]
    public float minHunger = 0f, minHealth = 0f, minOxygen = 0f;
    [Foldout("Sliders")]
    public Slider hungerSlider, healthSlider, oxygenSlider;

    void Start()
    {
        hungerSlider.interactable = false; healthSlider.interactable = false; oxygenSlider.interactable = false;
        hungerSlider.maxValue = maxHunger; healthSlider.maxValue = maxHealth; oxygenSlider.maxValue = maxOxygen;
        hungerSlider.minValue = minHunger; healthSlider.minValue = minHealth; oxygenSlider.minValue = minOxygen;
        hungerSlider.value = curHunger; healthSlider.value = curHealth; oxygenSlider.value = curOxygen;

        StartCoroutine(DecreaseHunger());
        
    }

    void Update()
    {
        //healthTextTMP.text = Mathf.Round(curHealth).ToString() + "%";
        //hungerTextTMP.text = Mathf.Round(curHunger).ToString() + "%";
        //oxygenTextTMP.text = Mathf.Round(curOxygen).ToString() + "%";
        hungerSlider.value = curHunger;
        healthSlider.value = curHealth;
    }

    public IEnumerator DecreaseOxygen()
    {
        while(curOxygen >= minOxygen)
        {
            yield return new WaitForSeconds(1);
            curOxygen -= Time.fixedDeltaTime * 10 * decreaseOxygenStep;
            oxygenSlider.value = curOxygen;
            if (curOxygen <= minOxygen) curOxygen = minOxygen;
        }
    }

    public IEnumerator AddOxygen()
    {
        while(curOxygen <= maxOxygen)
        {
            yield return new WaitForSeconds(1);
            curOxygen += Time.fixedDeltaTime * 10 * addOxygenStep;
            oxygenSlider.value = curOxygen;
            if (curOxygen >= maxOxygen) curOxygen = maxOxygen;
        }
    }

    public IEnumerator DecreaseHunger()
    {
        while(curHunger >= minHunger)
        {
            yield return new WaitForSeconds(1);
            curHunger -= Time.fixedDeltaTime * 10 * decreaseHungerStep;
            hungerSlider.value = curHunger;
            if (curHunger <= minHunger) curHunger = minHunger;
        }
    }

    public IEnumerator AddHunger()
    {
        while(curHunger <= maxHunger)
        {
            yield return new WaitForSeconds(1);
            curHunger -= Time.fixedDeltaTime * 10 * addHungerStep;
            hungerSlider.value = curHunger;
            if (curHunger >= minHunger) curHunger = maxHunger;
        }
    }
}
