using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatas
{
    public static Dictionary<string, Dialogue>
    dialogueList = new Dictionary<string, Dialogue>();

    public static void AddDialogue(Dialogue dialogue)
    {
        dialogueList.Add(dialogue.dialogueCode, dialogue);
    }

    public static Dialogue GetDialogue(string code)
    {
        return dialogueList[code];
    }
}
