using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGetter : MonoBehaviour
{
    [SerializeField] int valueToGet;
    [SerializeField] TextDialogue claimedText;
    private bool claimed = false;
    private bool canClaim = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !claimed)
        {
            canClaim = true;
        }
     
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !claimed)
        {
            canClaim = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canClaim && !claimed)
        {
            Consistency.Instance.MoneyGot(valueToGet);
            claimed = true;
            claimedText.Conversation[0] = "You got " + valueToGet.ToString() + " money";
            DialogueUISingleton.Instance.SetupDialogue(claimedText);
        }
    }
}
