using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueType
{
    None = 0, Next, YesOrNo
}

public class Dialogue
{
    public string dialogueCode;
    public string dialogueContent;
    public string nextMessage;
    public string yesMessage;
    public string noMessage;
    public DialogueType dialogueType;

    public Dialogue()
    {

    }

    public Dialogue(string code, string content, string nextCode)
    {
        dialogueCode = code;
        dialogueContent = content;
        nextMessage = nextCode;
        dialogueType = DialogueType.Next;
    }

    public Dialogue(string code, string content, string yesCode, string noCode)
    {
        dialogueCode = code;
        dialogueContent = content;
        yesMessage = yesCode;
        noMessage = noCode;
        dialogueType = DialogueType.YesOrNo;
    }
}
