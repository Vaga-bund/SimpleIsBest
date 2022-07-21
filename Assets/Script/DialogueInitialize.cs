using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInitialize : MonoBehaviour
{
    private void Awake()
    {
        DialogueDatas.AddDialogue(new Dialogue("0000", "����", "0001"));
        DialogueDatas.AddDialogue(new Dialogue("0001", "������ �ݰ���", "0002"));
        DialogueDatas.AddDialogue(new Dialogue("0002", "�ʵ� �ݰ���?", "0003", "0004"));
        DialogueDatas.AddDialogue(new Dialogue("0003", "���� �츮 �����", ""));
        DialogueDatas.AddDialogue(new Dialogue("0004", "��ģ��", ""));
    }
}
