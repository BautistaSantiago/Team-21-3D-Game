using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class EndMenu : MonoBehaviour
{
    // Update is called once per frame
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
