using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInitialize : MonoBehaviour
{
    private void Awake()
    {
        DialogueDatas.AddDialogue(new Dialogue("0000", "ㅎㅇ", "0001"));
        DialogueDatas.AddDialogue(new Dialogue("0001", "만나서 반가워", "0002"));
        DialogueDatas.AddDialogue(new Dialogue("0002", "너도 반갑니?", "0003", "0004"));
        DialogueDatas.AddDialogue(new Dialogue("0003", "ㅎㅎ 우리 사귀자", ""));
        DialogueDatas.AddDialogue(new Dialogue("0004", "미친놈", ""));
    }
}
