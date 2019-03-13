// 'Controls.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script manages the player controls.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour {

    [SerializeField] Canvas gameCanvas;

    private float smoothTime = 0.3f;

    private GameObject ufo;

    private Vector2 resolution;
    private Vector2 velocity;

    public void SetVariables()
    {
        ufo = GameObject.Find("UFO");
        resolution = gameCanvas.GetComponent<CanvasScaler>().referenceResolution;
    }

    private Vector2 GetMousePosition()
    {
        return new Vector2(Input.mousePosition.x / gameCanvas.scaleFactor - resolution.x / 2,
            Input.mousePosition.y / gameCanvas.scaleFactor - resolution.y / 2);
    }

    public void SetUFOPosition()
    {
        ufo.transform.localPosition = Vector2.SmoothDamp(ufo.transform.localPosition,
            GetMousePosition() * new Vector2(1, 0) + new Vector2(0, ufo.transform.localPosition.y),
            ref velocity, smoothTime);
    }
}
