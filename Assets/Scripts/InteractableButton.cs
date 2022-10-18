using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum EInteractableType
//{
    //EIP_Button//,
    //----------,
    //----------
//}

public class InteractableButton : MonoBehaviour
{
    public bool open = false;
    private Animator doorAnimator;

    public GameObject prefab_oxygenTrigger, triggersSpawnPoint;
    private GameObject instOxygenTrigger;

    private GameObject roomZone;
    private Room room;
    //public EInteractableType interactableType;

    private void Start()
    {
        doorAnimator = transform.parent.GetComponent<Animator>();
        roomZone = transform.parent.transform.parent.gameObject.transform.GetChild(1).gameObject;
        room = roomZone.GetComponent<Room>();
    }

    public void DoorFunc()
    {
            bool alreadyChecked = false;
            if (open) //closing door
            {
                doorAnimator.SetBool("doorOpened", false);
                open = false;
                alreadyChecked = true;
                Destroy(instOxygenTrigger);
                //room.isOxygenDecreasing = false;
                print($"<color=#CE7E00>Room oxygen ADD!</color>");
            }
            if (!open && !alreadyChecked) //opening door
            {
                doorAnimator.SetBool("doorOpened", true);
                open = true;
                instOxygenTrigger = Instantiate(prefab_oxygenTrigger, triggersSpawnPoint.transform);
                print($"<color=#CE7E00>Room oxygen DECREASING!</color>");
            }
    }
}
