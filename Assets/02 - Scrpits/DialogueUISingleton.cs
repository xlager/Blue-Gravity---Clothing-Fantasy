using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Enums;
using System.Linq;

public class DialogueUISingleton : MonoBehaviour
{
    public static DialogueUISingleton Instance;
    [SerializeField] GameObject dialogueUIObject;
    [SerializeField] GameObject dialogueConfirmationUIObject;
    [SerializeField] Button confirmButton;
    [SerializeField] Button negationButton;
    [SerializeField] GameObject storeMenu;
    [SerializeField] PopUpController storeController;

    public TextMeshProUGUI dialogueUI;
    public TextMeshProUGUI dialogName;
    bool isTyping = false;

    public UnityEvent onTypeEnded;
    public UnityEvent onCloseAll;
    public UnityEvent onPlayerInput;
    bool waitForPlayerInput;
    TextDialogue currentDialogue;
    private texts fullConversation;
    private struct texts
    {
        public List<string> allText;
        public int confirmationValue;
    }


    private static string WHITE_COLOR = "<color=#F3F3F3>";
    [SerializeField] float typeSpeed = 5f;

    private void Awake()
    {
        if ((Instance != null) && (Instance != this))
            Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }
    public void SetupDialogue(TextDialogue npcDialogue)
    {
        currentDialogue = npcDialogue;
        dialogName.text = currentDialogue.SpeakerName;
        fullConversation = new texts();
        fullConversation.allText = new List<string>();
        if (!currentDialogue.hasConfirmation)
        {
            for (int i = 0; i < currentDialogue.Conversation.Length; i++)
            {
                fullConversation.allText.Add(currentDialogue.Conversation[i]);
                fullConversation.confirmationValue = -1;
            }

        }
        else// (npcDialogue.hasConfirmation)
        {
            fullConversation.confirmationValue = -1;
            for (int i = 0; i < currentDialogue.Conversation.Length; i++)
            {
                if (i != currentDialogue.ConfirmationText && i != currentDialogue.NegationText)
                {
                    fullConversation.allText.Add(currentDialogue.Conversation[i]);
                    if(currentDialogue.whenIsConfirmation == i)
                        fullConversation.confirmationValue = fullConversation.allText.Count-1;
                }

            }
            //DialogueStart(npcDialogue.Conversation[1], npcDialogue.hasConfirmation);
            ////SetIteractionValues(npcDialogue.menuType,
            //npcDialogue.Conversation[npcDialogue.ConfirmationText],
            //npcDialogue.Conversation[npcDialogue.NegationText]);
        }
        ContinueConversation();
    }

    public void DialogueStart(string dialogue, bool hasConfirmation = false)
    {
        if (!isTyping)
        {
            dialogueUIObject.SetActive(true);
            StartCoroutine(DialogueType(dialogue, hasConfirmation));
        }
    }
    public IEnumerator DialogueType(string dialogue, bool hasConfirmation)
    {
        isTyping = true;
        dialogueUI.text = "";
        string original = dialogue;
        string display = "";
        int alphaIndex = 0;
        foreach (char c in dialogue.ToCharArray()) 
        {
            alphaIndex++;
            dialogueUI.text = original;
            display = dialogueUI.text.Insert(alphaIndex, WHITE_COLOR);
            dialogueUI.text = display;

            yield return new WaitForSeconds(0.1f/typeSpeed);
        }
        onTypeEnded?.Invoke();
        isTyping = false;
        if (hasConfirmation)
        {
            dialogueConfirmationUIObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(confirmButton.gameObject);
        }
    }

    public void SetIteractionValues(Popups menuType)
    {
        negationButton.onClick.RemoveAllListeners();
        confirmButton.onClick.RemoveAllListeners();
        switch (menuType)
        {
            case Popups.Inventory:
                break;
            case Popups.Store:
                confirmButton.onClick.AddListener(OpenStore);
                confirmButton.onClick.AddListener(storeController.StoreSetup);
                confirmButton.onClick.AddListener(() => DialogueStart(currentDialogue.Conversation[currentDialogue.ConfirmationText],false));

                negationButton.onClick.AddListener(() => dialogueConfirmationUIObject.SetActive(false));
                negationButton.onClick.AddListener(() => StartCoroutine(SetWaitInput()));
                negationButton.onClick.AddListener(() => onPlayerInput.AddListener(CloseAll));
                negationButton.onClick.AddListener(() => DialogueStart(currentDialogue.Conversation[currentDialogue.NegationText], false));
                break;
            case Popups.None:
                StartCoroutine(SetWaitInput());
                onPlayerInput.RemoveAllListeners();
                onPlayerInput.AddListener(ContinueConversation);
                break;
            default:
                break;
        }

    }

    public void OpenStore()
    {
        storeMenu.SetActive(true);
        dialogueConfirmationUIObject.SetActive(false);
    }

    public void CloseAll()
    {
        dialogueUIObject.SetActive(false);
        dialogueConfirmationUIObject.SetActive(false);
        storeMenu.SetActive(false);
        onCloseAll?.Invoke();
        onTypeEnded.RemoveAllListeners();
        onPlayerInput.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        onCloseAll = null;
        onTypeEnded = null;
        onPlayerInput = null;
    }

    private void Update()
    {
        if (waitForPlayerInput)
        {
            if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.E))
            {
                onPlayerInput?.Invoke();
                waitForPlayerInput = false;
            }
        }
    }
    private IEnumerator SetWaitInput()
    {
        yield return new WaitForSeconds(.5f);
        waitForPlayerInput = true;
    }

    private void ContinueConversation()
    {
        if(fullConversation.allText.Count > 0)
        {
            DialogueStart(fullConversation.allText[0], currentDialogue.hasConfirmation);
            fullConversation.allText.RemoveAt(0);
            if (fullConversation.confirmationValue == 0)
                SetIteractionValues(Enums.Popups.Store);
            else
                SetIteractionValues(Enums.Popups.None);
            fullConversation.confirmationValue--;
        }
        else
        {
            CloseAll();
        }
    }

}
