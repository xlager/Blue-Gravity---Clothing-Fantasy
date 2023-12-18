using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    bool canInteract = false;
    public UnityEvent onInteracted;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            canInteract = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canInteract = false;
    }
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E)) 
        {
            onInteracted.Invoke();
        }
    }
}
