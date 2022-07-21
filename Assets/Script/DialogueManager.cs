using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Button btnShowDialogue;
    [SerializeField] Text txtContent;
    [SerializeField] private GameObject dialogueForm;
    [SerializeField] private GameObject dialogueYesOrNo;
    [SerializeField] private GameObject dialogueNextOnly;
    [SerializeField] private Button btnYes;
    [SerializeField] private Button btnNo;
    [SerializeField] private Button btnNext;
    private bool isTexting;
    int endIndex;
    float timer;
    float delay = 0.1f;
    Dialogue currentDialogue;

    private void Awake()
    {
        btnShowDialogue.onClick.AddListener(() => ShowDialogue("0000"));
    }

    private void Update()
    {
        if (currentDialogue != null)
        {
            if (isTexting)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    endIndex = currentDialogue.dialogueContent.Length;
                    Debug.Log("내용 다쓰기");
                }
                timer += Time.deltaTime;
                if (timer >= delay)
                {
                    timer -= delay;
                    endIndex = Mathf.Clamp(++endIndex, 0, currentDialogue.dialogueContent.Length);

                    txtContent.text = currentDialogue.dialogueContent.Substring(0, endIndex);
                    if (endIndex >= currentDialogue.dialogueContent.Length)
                    {
                        isTexting = false;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    if (currentDialogue.dialogueType == DialogueType.Next)
                    {
                        Debug.Log("다음 내용으로");
                        ShowDialogue(currentDialogue.nextMessage);
                    }
                }
            }
        }
    }

    private void ShowDialogue(string code)
    {
        if (code != "")
        {
            endIndex = 0;
            dialogueForm.SetActive(true);
            isTexting = true;
            Dialogue dialogue = DialogueDatas.GetDialogue(code);
            currentDialogue = dialogue;
            btnYes.onClick.RemoveAllListeners();
            btnNo.onClick.RemoveAllListeners();
            btnNext.onClick.RemoveAllListeners();
            switch (dialogue.dialogueType)
            {
                case DialogueType.None:
                    break;
                case DialogueType.Next:
                    dialogueNextOnly.SetActive(true);
                    dialogueYesOrNo.SetActive(false);
                    btnNext.onClick.AddListener(() => ShowDialogue(dialogue.nextMessage));
                    break;
                case DialogueType.YesOrNo:
                    dialogueNextOnly.SetActive(false);
                    dialogueYesOrNo.SetActive(true);
                    btnYes.onClick.AddListener(() => ShowDialogue(dialogue.yesMessage));
                    btnNo.onClick.AddListener(() => ShowDialogue(dialogue.noMessage));
                    break;
                default:
                    break;
            }
        }
        else
        {
            dialogueForm.SetActive(false);
            currentDialogue = null;
        }

    }

    private void CloseDialogue()
    {
        dialogueForm.SetActive(false);
    }


}