using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Menus { Inventory = 0, Store = 1 };
public class DialogueUISingleton : MonoBehaviour
{
    public static DialogueUISingleton Instance;
    [SerializeField] GameObject dialogueUIObject;
    [SerializeField] GameObject dialogueConfirmationUIObject;
    [SerializeField] Button confirmButton;
    [SerializeField] Button negationButton;
    [SerializeField] GameObject storeMenu;

    public TextMeshProUGUI dialogueUI;
    public TextMeshProUGUI dialogName;
    bool isTyping = false;

    public UnityEvent onTypeEnded;
    public UnityEvent onCloseAll;
    public UnityEvent onPlayerInput;
    bool waitForPlayerInput;

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
        dialogName.text = npcDialogue.SpeakerName;
        DialogueStart(npcDialogue.Conversation[0], true);
        if (npcDialogue.hasConfirmation)
        {
            SetConfirmationValues(npcDialogue.menuType, 
                npcDialogue.Conversation[npcDialogue.ConfirmationText],
                npcDialogue.Conversation[npcDialogue.NegationText]);
        }
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

    public void SetConfirmationValues(Menus menuType, string yes, string no)
    {
        negationButton.onClick.RemoveAllListeners();
        confirmButton.onClick.RemoveAllListeners();
        switch (menuType)
        {
            case Menus.Inventory:
                break;
            case Menus.Store:
                confirmButton.onClick.AddListener(OpenStore);
                confirmButton.onClick.AddListener(() => DialogueStart(yes,false));
                negationButton.onClick.AddListener(() => dialogueConfirmationUIObject.SetActive(false));
                negationButton.onClick.AddListener(() => StartCoroutine(SetWaitInput()));
                negationButton.onClick.AddListener(() => onPlayerInput.AddListener(CloseAll));
                negationButton.onClick.AddListener(() => DialogueStart(no, false));
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
    public void OpenMenu()
    {
        Debug.LogError("DONE");
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

}
