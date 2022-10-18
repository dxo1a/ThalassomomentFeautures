using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUPERCharacter;

public class DoorButton : MonoBehaviour, IInteractable
{   
    Animator doorAnimator;
    public GameObject prefab_oxygenTrigger, triggersSpawnPoint;
    public bool isDoorClosed;
    private GameObject instOxygenTrigger;

    private void Start()
    {
        doorAnimator = transform.parent.transform.parent.GetComponent<Animator>();
        isDoorClosed = true;
    }

    public bool Interact(){
        if (isDoorClosed)
        {
            OpenDoor();
            isDoorClosed = !isDoorClosed;
            instOxygenTrigger = Instantiate(prefab_oxygenTrigger, triggersSpawnPoint.transform);
            print($"<color=#CE7E00>Room oxygen DECREASING!</color>");
        }
        if (!isDoorClosed)
        {
            isDoorClosed = !isDoorClosed;
            Destroy(instOxygenTrigger);
            print($"<color=#CE7E00>Room oxygen ADD!</color>");
        }
        return false;
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool("doorOpened", true);
        
    }

    public void CloseDoor()
    {
        doorAnimator.SetBool("doorOpened", false);
        
    }
}
