using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject interactionButton;
    [SerializeField] TextDialogue textObject;
    bool isInRange = false;
    bool startedConversation = false;
    public UnityEvent onInteracted;
    public UnityEvent onInteractionEnd;

    private void Start()
    {
        DialogueUISingleton.Instance.onCloseAll.AddListener(() => StartCoroutine(ResetConversation()));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionButton.SetActive(false);
            isInRange = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionButton.SetActive(true);
            isInRange = true;
        }
    }

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !startedConversation)
            {
                onInteracted?.Invoke();
                startedConversation = true;
                StartInteraction();
            }
        }
    }
    private void StartInteraction()
    {
        DialogueUISingleton.Instance.SetupDialogue(textObject);
    }
    private IEnumerator ResetConversation()
    {
        yield return new WaitForSeconds(1f);
        startedConversation = false;
    }
}
