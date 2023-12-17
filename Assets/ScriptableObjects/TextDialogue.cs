using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueText")]
public class TextDialogue : ScriptableObject
{
    public string SpeakerName;
    [TextArea(5,10)]
    public string[] Conversation;
    public Menus menuType;
    public bool hasConfirmation;
    public int ConfirmationText;
    public int NegationText;
}
