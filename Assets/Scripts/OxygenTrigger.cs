using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTrigger : MonoBehaviour
{
    IEnumerator room_decreaseOxygen, room_addOxygen;
    private GameObject roomZone;
    private GameObject contactedRoomZone;
    private Room room;
    private Room contactedRoom;
    public float totalOxygen, targetOxygenPerRoom;
    public float roomTargetOxygen, contactedRoomTargetOxygen;

    void Start()
    {
        roomZone = transform.parent.transform.parent.gameObject;
        room = roomZone.GetComponent<Room>();
        //room_addOxygen = roomZone.GetComponent<Room>().room_AddOxygen();
    }

    private void Update()
    {
        if (room.canTransfer) TransferOxygen();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "openZone") //уменьшение кислорода в комнате
        {
            //room.isOxygenDecreasing = true;
        }

        if(other.gameObject.tag == "roomZone")
        {
            
            contactedRoom = other.gameObject.GetComponent<Room>();
            //room.isOxygenDecreasing = true;
            //contactedRoom.isOxygenDecreasing = true;
            print(contactedRoom.name);
            room.canTransfer = true;
            contactedRoom.canTransfer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "roomZone")
        {
            //room.isOxygenDecreasing = false;
            room.canTransfer = false;
        }
    }

    public void TransferOxygen()
    {
        totalOxygen = (room.room_curOxygen + contactedRoom.room_curOxygen);
        targetOxygenPerRoom = totalOxygen / 2;

        room.room_curOxygen = Mathf.MoveTowards(room.room_curOxygen, targetOxygenPerRoom, room.room_oxygenStep * Time.deltaTime);
        contactedRoom.room_curOxygen = Mathf.MoveTowards(contactedRoom.room_curOxygen, targetOxygenPerRoom, room.room_oxygenStep * Time.deltaTime);

        
        // or
        //room.room_curOxygen = Mathf.Lerp(room.room_curOxygen, targetOxygenPerRoom, 5 * Time.deltaTime);
        //contactedRoom.room_curOxygen = Mathf.Lerp(contactedRoom.room_curOxygen, targetOxygenPerRoom, 5 * Time.deltaTime);
    }
}
