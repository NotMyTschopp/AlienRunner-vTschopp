// 'SaveAndLoad.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script manages saving and loading the game via a scriptable object.

using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SaveAndLoadScriptableObject", order = 1)]
public class SaveAndLoadScriptableObject : ScriptableObject {

    public int highscore;
    public int lastRun;
}
