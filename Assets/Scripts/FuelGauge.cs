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
using TMPro;

public class FuelGauge : MonoBehaviour {

    [SerializeField] GameObject fuelGauge;
    [SerializeField] GameObject runawayCounterLabel;

    private Vector2 fuelGaugeInit;

    private int fuelPercentage;

    private float timer;

    private void Start()
    {
        runawayCounterLabel.GetComponent<TextMeshProUGUI>().text = GlobalVariables.abductedRunaways.ToString();
        fuelGaugeInit = fuelGauge.GetComponent<RectTransform>().sizeDelta;
        fuelPercentage = GlobalVariables.fuelPercentage;
        timer = GlobalVariables.fuelSpeed;
    }

    private void Update()
    {
        fuelGauge.GetComponent<RectTransform>().sizeDelta = new Vector2(fuelGaugeInit.x * GlobalVariables.fuelPercentage / 100,
            fuelGaugeInit.y);

        timer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && fuelPercentage > 0)
        {
            if(timer < 0)
            {
                fuelPercentage = GlobalVariables.fuelPercentage -= 1;
                timer = GlobalVariables.fuelSpeed;
            }
        }
    }
}
