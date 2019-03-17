// 'Controls.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script manages the player/spaceship controls.

using UnityEngine;

public class Controls : MonoBehaviour {

    private float smoothTime = 0.3f;

    private Vector2 velocity;

    private void SetSpaceshipPosition()
    {
        GlobalVariables.spaceship.transform.localPosition = Vector2.SmoothDamp(GlobalVariables.spaceship.transform.localPosition,
            GlobalVariables.GetMousePosition() * new Vector2(1, 0) + new Vector2(0, GlobalVariables.spaceship.transform.localPosition.y),
            ref velocity, smoothTime);
    }

    private void Update()
    {
        SetSpaceshipPosition();
    }
}
