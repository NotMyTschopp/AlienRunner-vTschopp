// 'Highscore.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script manages what is displayed on the end scene (highscore).

using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour {

    public SaveAndLoadScriptableObject saveAndLoadValues;

    void Start () {
		if(saveAndLoadValues.lastRun > saveAndLoadValues.highscore)
        {
            GetComponent<TextMeshProUGUI>().text = saveAndLoadValues.lastRun + "*"; // '*' indicates a new highscore.
            saveAndLoadValues.highscore = saveAndLoadValues.lastRun;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = saveAndLoadValues.lastRun.ToString();
        }
    }
}
