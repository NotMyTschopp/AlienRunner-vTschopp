// 'ContactWithSpaceship.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script manages the actions when somethings hits the spaceship.

using UnityEngine;
using TMPro;

public class ContactWithSpaceship : MonoBehaviour {

    [SerializeField] GameObject runawayCounterLabel;

    // Theoretically, it would be possible that the game object is destroyed
    // and the fuel gauge is not updated. This happens because the fuel gauge
    // is only updated when the mouse button is held. But human reflexes are to slow
    // to release in the exact same moment the game object is destroyed so we'll ignore this.
    private void AddFuel(int fuelToAdd)
    {
        if(GlobalVariables.fuelPercentage <= 100 - fuelToAdd)
        {
            GlobalVariables.fuelPercentage = GlobalVariables.fuelPercentage + fuelToAdd;
        }
        else
        {
            GlobalVariables.fuelPercentage = 100;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Contains("Mammoth") == true)
        {
            AddFuel(30);
        }
        else
        {
            GlobalVariables.abductedRunaways++;
            runawayCounterLabel.GetComponent<TextMeshProUGUI>().text = GlobalVariables.abductedRunaways.ToString();
        }

        Destroy(collision.gameObject);
        GlobalVariables.tractorBeamStatus = true;
    }
}
