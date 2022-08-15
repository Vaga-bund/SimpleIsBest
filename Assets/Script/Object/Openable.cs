using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Openable : TInteractable
{
    [Header("Elevator Reference")]
    public GameObject openElv;
    public GameObject closedElv;

    [Header("Checking To Open")]
    private SpriteRenderer sr;
    private bool isOpen;
    private bool setOpen;

    [Header("Check Able to Pass")]
    public Elevator elevator;

    public override void Interact()
    {
        if (isOpen && setOpen)
        {
            openElv.SetActive(false);
            closedElv.SetActive(true);
        }
        else
        {
            openElv.SetActive(true);
            closedElv.SetActive(false);

            elevator.CheckElevator();
        }

        isOpen = !isOpen;
    }

    public override void SetOpen()
    {
        setOpen = !setOpen;
    }    

    private void Start()
    {
        /*
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;
        */
    }
}