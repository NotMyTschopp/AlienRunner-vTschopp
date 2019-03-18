// 'TractorBeam.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script controls the tractor beam behaviour.

using UnityEngine;

public class TractorBeam : MonoBehaviour {

    private GameObject abductedRunaway;

    private float smoothTime = 0.75f;

    private Vector2 velocity;

    private bool CheckTag(GameObject objectToCheck)
    {
        if(objectToCheck.tag == "HumanLeft" || objectToCheck.tag == "HumanRight"
            || objectToCheck.tag == "MammothLeft" || objectToCheck.tag == "MammothRight")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void AbductRunaway()
    {
        GlobalVariables.tractorBeamStatus = false;

        if(abductedRunaway.tag == "HumanAbducted")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().velocity = GlobalVariables.humanLiftSpeed;
        }
        else if(abductedRunaway.tag == "MammothAbducted")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().velocity = GlobalVariables.mammothLiftSpeed;
        }
    }

    private void SetAbductedRunawayPosition()
    {
        abductedRunaway.transform.localPosition = Vector2.SmoothDamp(abductedRunaway.transform.localPosition,
            GlobalVariables.GetMousePosition() * new Vector2(1, 0) + new Vector2(0, abductedRunaway.transform.localPosition.y),
            ref velocity, smoothTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetMouseButton(0) && GlobalVariables.tractorBeamStatus == true
            && CheckTag(collision.gameObject) == true && GlobalVariables.fuelPercentage > 0)
        {
            abductedRunaway = collision.gameObject;

            if(abductedRunaway.tag.Contains("Human") == true)
            {
                abductedRunaway.tag = "HumanAbducted";
            }
            else if(abductedRunaway.tag.Contains("Mammoth") == true)
            {
                abductedRunaway.tag = "MammothAbducted";
            }

            AbductRunaway();
        }
        else if(abductedRunaway != null && abductedRunaway.tag == "HumanAbducted" && GlobalVariables.fuelPercentage <= 0)
        {
            abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            abductedRunaway.tag = "HumanFalling";
            GlobalVariables.tractorBeamStatus = true;
        }
        else if(abductedRunaway != null && abductedRunaway.tag == "MammothAbducted" && GlobalVariables.fuelPercentage <= 0)
        {
            abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            abductedRunaway.tag = "MammothFalling";
            GlobalVariables.tractorBeamStatus = true;
        }
        else if(Input.GetMouseButton(0) == false && abductedRunaway != null && abductedRunaway.tag == "HumanAbducted")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            abductedRunaway.tag = "HumanFalling";
            GlobalVariables.tractorBeamStatus = true;
        }
        else if(Input.GetMouseButton(0) == false && abductedRunaway != null && abductedRunaway.tag == "MammothAbducted")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            abductedRunaway.tag = "MammothFalling";
            GlobalVariables.tractorBeamStatus = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(abductedRunaway != null && collision.tag == "HumanAbducted")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GlobalVariables.tractorBeamStatus = true;
            abductedRunaway.tag = "HumanFalling";
        }
        else if(abductedRunaway != null && collision.tag == "MammothAbducted")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GlobalVariables.tractorBeamStatus = true;
            abductedRunaway.tag = "MammothFalling";
        }
    }

    private void Update()
    {
        if(GlobalVariables.tractorBeamStatus == false)
        {
            SetAbductedRunawayPosition();
        }
    }
}
