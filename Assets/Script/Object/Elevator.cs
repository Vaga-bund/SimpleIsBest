using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    [Header("Levels to Load")]
    public string nextStageLevel;

    public bool useIntergerLoadLevel = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "Player")
        {
            if(nextStageLevel == "Final")
            {
                Debug.Log("Clear");
                Application.Quit();
            }
            else if (useIntergerLoadLevel)
                LoadStage();
        }
    }

    public void CheckElevator()
    {
        useIntergerLoadLevel = true;
    }

    public void LoadStage()
    {
        SceneManager.LoadScene(nextStageLevel);
    }
}