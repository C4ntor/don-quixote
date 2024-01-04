using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void loadGame() {
        //generateTerrain()
        SceneManager.LoadScene("game");
        //disable and destroy mainMenu();
        //turn on Healthbar and spawn player and camera.

    }

    public void loadMenu()
    {
        SceneManager.LoadScene("start");
    }

    // Update is called once per frame
    public void quitGame()
    {
        Application.Quit();
    }
}
