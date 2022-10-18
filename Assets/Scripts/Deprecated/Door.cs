using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open = false;
    public Transform buttonTransform;
    public float distToOpen = 4;
    private Transform playerTransform, playerCameraTransform;
    //public string openAnimationString, closeAnimationString;
    private Animator doorAnimator;
    
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        if (open)
            doorAnimator.SetBool("doorOpened", true);
    }

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool alreadyChecked = false;
        float angle = Vector3.Angle(buttonTransform.position = playerCameraTransform.position, buttonTransform.position + (playerCameraTransform.right * buttonTransform.localScale.magnitude) - playerCameraTransform.position);
        if (Vector3.Distance(playerTransform.position, buttonTransform.position) <= distToOpen)
        if (Vector3.Angle(buttonTransform.position - playerCameraTransform.position, playerCameraTransform.forward) <= angle)
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (open)
            {
                doorAnimator.SetBool("doorOpened", false);
                open = false;
                alreadyChecked = true;
            }
            if (!open && !alreadyChecked)
            {
                doorAnimator.SetBool("doorOpened", true);
                open = true;
            }
        }
    }

    public static bool IsAnimationPlaying(Animator animator, string animationName)
    {
        // берем информацию о состоянии 
        var animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // смотрим, есть ли в нем имя какой-то анимации, то возвращаем true
        if (animatorStateInfo.IsName(animationName))
            return true;
        return false;
    }
}
