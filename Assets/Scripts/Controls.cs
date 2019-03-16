// 'Controls.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script manages the player controls.

using UnityEngine;

public class Controls : MonoBehaviour {

    private float smoothTime = 0.3f;

    private GameObject ufo;
    private GameObject tractorBeam;

    private Vector2 velocity;

    public void SetObjects()
    {
        ufo = GameObject.Find("UFO"); // Use 'name' instead of 'tag' because there's only one UFO.
    }

    public Vector2 GetMousePosition()
    {
        return new Vector2(Input.mousePosition.x / GlobalVariables.gameCanvas.scaleFactor - GlobalVariables.resolution.x / 2,
            Input.mousePosition.y / GlobalVariables.gameCanvas.scaleFactor - GlobalVariables.resolution.y / 2);
    }

    public void SetUFOPosition()
    {
        ufo.transform.localPosition = Vector2.SmoothDamp(ufo.transform.localPosition,
            GetMousePosition() * new Vector2(1, 0) + new Vector2(0, ufo.transform.localPosition.y),
            ref velocity, smoothTime);
    }
}
