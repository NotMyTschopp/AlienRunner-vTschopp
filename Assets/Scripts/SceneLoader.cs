// 'SceneLoader.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script manages the loading of levels and quitting of the application.
// Use as click function on buttons.

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public SaveAndLoadScriptableObject saveAndLoadValues;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TryAgain()
    {
        GlobalVariables.abductedRunaways = 0;
        GlobalVariables.fuelPercentage = 100;
        GlobalVariables.tractorBeamStatus = true;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainScene" && GlobalVariables.fuelPercentage <= 0)
        {
            saveAndLoadValues.lastRun = GlobalVariables.abductedRunaways;
            SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
        }
    }
}
