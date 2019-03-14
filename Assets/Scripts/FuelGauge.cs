// 'FuelGauge.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script manages the fuel of the UFO and the
// fuel gauge drawn on the user interface.

using UnityEngine;

public class FuelGauge : MonoBehaviour {

    [SerializeField] Texture2D fuelIcon;

    [SerializeField] GUIStyle fuelBarStyle = new GUIStyle();
    [SerializeField] GUIStyle fuelBarStyleBackground = new GUIStyle();

    private Vector2 position = new Vector2(20, 20);
    private Vector2 size = new Vector2(200, 20);

    private int fuelPercentage = new int();

    private void drawFuelBar()
    {
        GUI.BeginGroup(new Rect(position.x, position.y, size.x, size.y));

        // Background of fuel bar.
        GUI.Box(new Rect(0, 0, size.x, size.y), "", fuelBarStyleBackground);

        // Fuel bar
        GUI.BeginGroup(new Rect(0, 0, size.x * (fuelPercentage / 100f), size.y));
        GUI.Box(new Rect(0, 0, size.x * (fuelPercentage / 100f), size.y), fuelPercentage + "%", fuelBarStyle);
        GUI.EndGroup();

        GUI.EndGroup();
    }

    private void OnGUI()
    {
        drawFuelBar();
    }

    private void Start()
    {
        fuelPercentage = 100;
    }
}
