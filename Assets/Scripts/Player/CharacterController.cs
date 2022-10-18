using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;

public class CharacterController : MonoBehaviour
{
    #region Raycast Interact Variables
    [HideInInspector]
    public int interactDistance = 1;
    private InteractableButton interactableButtonObj = null;
    #endregion

    private Camera mainCamera;

    #region Survival Stats Variables
    [HideInInspector]
    public float curHealth = 100f, maxHealth = 100f;
    [HideInInspector]
    public float curHunger = 100f, maxHunger = 100f;
    [HideInInspector]
    public float curOxygen = 100f, maxOxygen = 100f;
    [HideInInspector]
    public int addOxygenStep, decreaseOxygenStep, addHungerStep, decreaseHungerStep;

    [HideInInspector]
    public float minHunger = 0f, minHealth = 0f, minOxygen = 0f;
    public Slider hungerSlider, healthSlider, oxygenSlider;
    #endregion

    public bool isOxygenEmpty, isOxygenFull;
    public bool inRoom = false;
    private Room room;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        #region Survival Stats
        hungerSlider.interactable = false; healthSlider.interactable = false; oxygenSlider.interactable = false;
        hungerSlider.maxValue = maxHunger; healthSlider.maxValue = maxHealth; oxygenSlider.maxValue = maxOxygen;
        hungerSlider.minValue = minHunger; healthSlider.minValue = minHealth; oxygenSlider.minValue = minOxygen;
        hungerSlider.value = curHunger; healthSlider.value = curHealth; oxygenSlider.value = curOxygen;
        //StartCoroutine(DecreaseHunger());
        #endregion
    }

    void Update()
    {
        //RaycastInteract
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastInteract();
        }

        hungerSlider.value = curHunger;
        healthSlider.value = curHealth;

        if (curOxygen == maxOxygen) isOxygenFull = true;
        else isOxygenFull = false;
        if (curOxygen == minOxygen) isOxygenEmpty = true;
        else isOxygenEmpty = false;

        CalculateOxygen();
        CalculateHunger();
    }

    #region Survival Stats Methods
    //public IEnumerator DecreaseOxygen()
    //{
    //    if (!isOxygenEmpty)
    //    {
    //        while(curOxygen >= minOxygen)
    //        {
    //            yield return new WaitForSeconds(1);
    //           curOxygen -= Time.fixedDeltaTime * 10 * decreaseOxygenStep;
    //            oxygenSlider.value = curOxygen;
    //            if (curOxygen <= minOxygen) curOxygen = minOxygen;
    //        }
    //    }
    //}

    //public IEnumerator AddOxygen()
    //{
    //    if (isOxygenEmpty || !isOxygenFull)
    //    {
    //        while(curOxygen <= maxOxygen)
    //        {
    //            yield return new WaitForSeconds(1);
    //            curOxygen += Time.fixedDeltaTime * 10 * addOxygenStep;
    //            oxygenSlider.value = curOxygen;
    //            if (curOxygen >= maxOxygen) curOxygen = maxOxygen;
    //        }
    //    }
    //}

    //public IEnumerator DecreaseHunger()
    //{
    //    while(curHunger >= minHunger)
    //    {
    //        yield return new WaitForSeconds(1);
    //        curHunger -= Time.fixedDeltaTime * 10 * decreaseHungerStep;
    //        hungerSlider.value = curHunger;
    //        if (curHunger <= minHunger) curHunger = minHunger;
    //    }
    //}

    //public IEnumerator AddHunger()
    //{
    //    while(curHunger <= maxHunger)
    //    {
    //        yield return new WaitForSeconds(1);
    //        curHunger -= Time.fixedDeltaTime * 10 * addHungerStep;
    //        hungerSlider.value = curHunger;
    //        if (curHunger >= minHunger) curHunger = maxHunger;
    //    }
    //}
    #endregion

    void RaycastInteract()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            InteractableButton obj = hit.collider.GetComponent<InteractableButton>();
            if (obj) interactableButtonObj = obj;
            else interactableButtonObj = null;
        }
        else
        {
            interactableButtonObj = null;
        }

        if (Input.GetKeyDown(KeyCode.E) && interactableButtonObj)
        {
            if (hit.transform.gameObject.layer == 6)
                hit.transform.gameObject.SendMessage("DoorFunc");
            //Destroy if u need for pickable objects
            //DestroyImmediate(interactableObj.gameObject);

        }
    }

    #region OxygenPlayer
    private void CalculateOxygen(){
        if(curOxygen != minOxygen && !inRoom || !room.isRoomContainsOxygen){
            curOxygen = Mathf.MoveTowards(curOxygen, 0, decreaseOxygenStep*Time.deltaTime);
            oxygenSlider.value = curOxygen;
        }
        if(curOxygen != maxOxygen && inRoom && room.isRoomContainsOxygen){
            curOxygen = Mathf.MoveTowards(curOxygen, maxOxygen, addOxygenStep*Time.deltaTime);
            oxygenSlider.value = curOxygen;
        }
    }
    #endregion

    #region Hunger
    private void CalculateHunger(){
        curHunger = Mathf.MoveTowards(curHunger, 0, decreaseHungerStep*Time.deltaTime);
        hungerSlider.value = curHunger;
        //if (condition) восстановление голода
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "roomZone")
        {
            room = other.gameObject.GetComponent<Room>();
        }
    }
}
