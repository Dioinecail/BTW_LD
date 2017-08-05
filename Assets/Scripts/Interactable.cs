using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowInteraction();
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideInteraction();
        }
    }


    protected virtual void ShowInteraction()
    {

    }
    protected virtual void HideInteraction()
    {

    }

    protected virtual void Interact()
    {

    }
}
