using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private Animator Anima;
    new private BoxCollider2D[] collider;

    private bool Open;

    private void Start()
    {
        Anima = GetComponent<Animator>();
        collider = GetComponents<BoxCollider2D>();
    }

    protected override void ShowInteraction()
    {
        Open = true;
        Anima.SetBool("Open", Open);
        foreach(BoxCollider2D col in collider)
        {
            if (col.isTrigger)
                continue;
            else
                col.enabled = false;
        }
    }
    protected override void HideInteraction()
    {
        Open = false;
        Anima.SetBool("Open", Open);
        foreach (BoxCollider2D col in collider)
        {
            if (col.isTrigger)
                continue;
            else
                col.enabled = true;
        }
    }

}
