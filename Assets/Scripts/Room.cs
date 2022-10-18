using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class Room : MonoBehaviour
{
    IEnumerator player_addOxygen, player_decreaseOxygen, room_addOxygen, room_decreaseOxygen;

    [Foldout("Oxygen")] [Range(0.0f, 100.0f)]
    public float room_curOxygen = 100f, room_maxOxygen = 100f, room_maxRemovedOxygen = 100f;
    [Foldout("Stats multipliers")] [Range(0, 25)]
    public int room_oxygenStep;
    [ReadOnly]
    public float room_minOxygen, room_minRemovedOxygen = 0f;
    public float decreaseCurrentAvg;
    public float room_removedOxygen;

    private GameObject player;
    private CharacterController characterController;
    public GameObject roomUI;
    private TMP_Text roomUI_oxygenLevelText, roomUI_removedOxygen;
    
    public bool isRoomContainsOxygen;
    //public bool isOxygenDecreasing = false;

    public bool canTransfer = false;

    public OxygenTrigger oxygenTrigger;

    void Start()
    {
        roomUI_oxygenLevelText = roomUI.GetComponent<RoomUI>().oxygenLevelText;
        roomUI_removedOxygen = roomUI.GetComponent<RoomUI>().removedOxygenText;
        player = GameObject.FindGameObjectWithTag("Player");
        characterController = player.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (room_curOxygen > 0)
        {
            isRoomContainsOxygen = true;
            //characterController.isRoomHasOxygen = true;
        }
        else
        {
            isRoomContainsOxygen = false;
            //characterController.isRoomHasOxygen = false;
        }
        CalculateRoomOxygen();
        
    }

    //public IEnumerator room_DecreaseOxygen()
    //{
    //    if (isRoomContainsOxygen)
    //    {
    //        while(room_curOxygen >= room_minOxygen)
    //        {
    //            yield return new WaitForSeconds(1);
    //            room_curOxygen -= Time.fixedDeltaTime * 10 * room_decreaseOxygenStep;
    //            if (room_curOxygen <= room_minOxygen) room_curOxygen = room_minOxygen;
    //            roomUI_oxygenLevelText.text = "Oxygen " + Mathf.Round(room_curOxygen) + "%";
    //        }
    //    }
    //}

    //public IEnumerator room_AddOxygen()
    //{
    //    while(room_curOxygen <= room_maxOxygen)
    //    {
    //        yield return new WaitForSeconds(1);
    //        room_curOxygen += Time.fixedDeltaTime * 10 * room_addOxygenStep;
    //        if (room_curOxygen >= room_maxOxygen) room_curOxygen = room_maxOxygen;
    //        roomUI_oxygenLevelText.text = "Oxygen " + Mathf.Round(room_curOxygen) + "%";
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        player_addOxygen = characterController.AddOxygen();
    //        StopCoroutine(player_decreaseOxygen);
    //        StartCoroutine(player_addOxygen);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            characterController.inRoom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            characterController.inRoom = false;
        }
    }

    #region OxygenRoom
    private void CalculateRoomOxygen(){
        //если включен генератор кислорода, то восстанавливать и если пробоина, то убавлять
        //if(room_curOxygen != room_minOxygen && isOxygenDecreasing && room_removedOxygen != room_maxRemovedOxygen){
        //    room_curOxygen = Mathf.MoveTowards(room_curOxygen, 0, room_decreaseOxygenStep*Time.deltaTime);
            roomUI_oxygenLevelText.text = "Oxygen     " + Mathf.Round(room_curOxygen).ToString() + "%";
            room_removedOxygen = room_maxOxygen - room_curOxygen;
            
        //
            //room_removedOxygen = Mathf.MoveTowards(room_removedOxygen, room_maxRemovedOxygen, room_oxygenStep * Time.deltaTime);
            roomUI_removedOxygen.text = "Removed     " + Mathf.Round(room_removedOxygen).ToString() + "%";
        //}
        //if(room_curOxygen != room_maxOxygen && !isOxygenDecreasing && room_removedOxygen != room_minRemovedOxygen){
        //    room_curOxygen = Mathf.MoveTowards(room_curOxygen, room_maxOxygen, room_addOxygenStep*Time.deltaTime);
            //roomUI_oxygenLevelText.text = "Oxygen     " + Mathf.Round(room_curOxygen).ToString() + "%";
        //
            //room_removedOxygen = Mathf.MoveTowards(room_removedOxygen, 0, room_oxygenStep * Time.deltaTime);
            //roomUI_removedOxygen.text = "Removed     " + Mathf.Round(room_removedOxygen).ToString() + "%";
        //}
    }
    #endregion
}
