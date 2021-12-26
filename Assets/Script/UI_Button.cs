using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Button : MonoBehaviour
{
    /* for using require: 
        BuildingBP within scene
    */

    public GameObject blueprint;
    public void CallPrefab() {
        Instantiate(blueprint);
    }

    public void LoadGameplay() {
        StartCoroutine(DelayAndLoadScene());
    }

    public void ExitGame() {
        Application.Quit();
    }

    private IEnumerator DelayAndLoadScene() {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Scene1");
        FindObjectOfType<AudioManager>().Play("Stage1");
        FindObjectOfType<AudioManager>().Stop("MainMenu");
    }
}
