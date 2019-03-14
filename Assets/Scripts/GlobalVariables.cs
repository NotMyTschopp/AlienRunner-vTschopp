﻿// 'GlobalVariables.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script contains all important global variables
// and MUST be on top of the script execution order.

using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour {

    public Canvas gameCanvasRef;

    public static Canvas gameCanvas;

    public static Vector2 resolution;

    // Use this for initialization
    void Start ()
    {
        gameCanvas = gameCanvasRef;
        resolution = gameCanvas.GetComponent<CanvasScaler>().referenceResolution;
    }
}