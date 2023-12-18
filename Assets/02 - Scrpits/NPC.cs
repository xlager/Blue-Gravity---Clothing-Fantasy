using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject interactionButton;
    [SerializeField] TextDialogue textObject;
    [SerializeField] TextDialogue secondaryTextDialogue;
    [SerializeField] bool willHaveSecondaryTalk;
    [SerializeField] int howManyTimesTalk;
    int counter=0;
    bool haveAlreadyTalked;
    bool isInRange = false;
    bool startedConversation = false;
    public UnityEvent onInteracted;
    public UnityEvent onInteractionEnd;
    private PlayerController playerController;

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
            playerController = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionButton.SetActive(true);
            isInRange = true;
            playerController = collision.GetComponentInParent<PlayerController>();
        }
    }

    private void Update()
    {
        if (isInRange && playerController != null && !playerController.IsIntereacting)
        {
            if (Input.GetKeyDown(KeyCode.E) && !startedConversation)
            {
                onInteracted?.Invoke();
                startedConversation = true;
                StartInteraction();
                counter++;
            }
        }
    }
    private void StartInteraction()
    {
        if (willHaveSecondaryTalk && !haveAlreadyTalked && counter >= howManyTimesTalk)
            DialogueUISingleton.Instance.SetupDialogue(secondaryTextDialogue);
        else
            DialogueUISingleton.Instance.SetupDialogue(textObject);
    }
    private IEnumerator ResetConversation()
    {
        yield return new WaitForSeconds(.5f);
        startedConversation = false;
    }
}
