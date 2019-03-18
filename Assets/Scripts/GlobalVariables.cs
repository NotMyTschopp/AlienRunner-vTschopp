// 'GlobalVariables.cs' is part of AlienRunner-vTschopp
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

    [SerializeField] Canvas gameCanvasReference;

    [SerializeField] GameObject spaceshipReference;
    [SerializeField] GameObject humanReference;
    [SerializeField] GameObject mammothReference;

    public static Canvas gameCanvas;

    public static GameObject spaceship;
    public static GameObject human;
    public static GameObject mammoth;

    public static int fuelPercentage = 100;
    public static int abductedRunaways;

    public static float fuelSpeed = 0.25f; // Higher numbers result in slower consumption.
    public static float humanSpawnTime = 5f;
    public static float mammothSpawnTime = 30f;

    public static Vector2 resolution;
    public static Vector2 humanSpeed = new Vector2(2f, 0f);
    public static Vector2 mammothSpeed = new Vector2(1f, 0f);
    public static Vector2 humanLiftSpeed = new Vector2(0f, 1f);
    public static Vector2 mammothLiftSpeed = new Vector2(0, 0.75f);

    public static Vector2 GetMousePosition()
    {
        return new Vector2(Input.mousePosition.x / gameCanvas.scaleFactor - resolution.x / 2,
            Input.mousePosition.y / gameCanvas.scaleFactor - resolution.y / 2);
    }

    public static bool tractorBeamStatus = true;

    public static bool flipCoin()
    {
        if (Random.Range(0f, 1f) < 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Start ()
    {
        gameCanvas = gameCanvasReference;

        spaceship = spaceshipReference;
        human = humanReference;
        mammoth = mammothReference;

        resolution = gameCanvas.GetComponent<CanvasScaler>().referenceResolution;
    }
}
