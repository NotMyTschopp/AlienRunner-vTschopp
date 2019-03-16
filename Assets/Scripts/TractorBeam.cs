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

    private GameObject controlsScript;
    private GameObject abductedRunaway;

    private Controls controls;

    private float smoothTime = 0.75f;

    private Vector2 velocity;

    private bool checkTag(GameObject objectToCheck)
    {
        if(objectToCheck.tag == "NeanderthalLeft" || objectToCheck.tag == "NeanderthalRight"
            || objectToCheck.tag == "DinoLeft" || objectToCheck.tag == "DinoRight")
        {
            return true;
        }
        else
        {
            Debug.Log("Tag not matching. Runaway will be ignored.");
            return false;
        }
    }

    private void abductRunaway()
    {
        GlobalVariables.tractorBeamStatus = false;
        if(abductedRunaway.tag == "AbductedNeanderthal")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().velocity = GlobalVariables.liftSpeedNeanderthal;
        }
        else if(abductedRunaway.tag == "AbductedDino")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().velocity = GlobalVariables.liftSpeedDino;
        }
    }

    private void setAbductedRunawayPosition()
    {
        abductedRunaway.transform.localPosition = Vector2.SmoothDamp(abductedRunaway.transform.localPosition,
            controls.GetMousePosition() * new Vector2(1, 0) + new Vector2(0, abductedRunaway.transform.localPosition.y),
            ref velocity, smoothTime);
    }

    private void Start()
    {
        controlsScript = GameObject.Find("Controls");
        controls = controlsScript.GetComponent<Controls>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetMouseButton(0) && GlobalVariables.tractorBeamStatus == true
            && checkTag(collision.gameObject) == true && GlobalVariables.fuelPercentage > 0)
        {
            abductedRunaway = collision.gameObject;

            if(abductedRunaway.tag.Contains("Neanderthal") == true)
            {
                abductedRunaway.tag = "AbductedNeanderthal";
            }
            else if(abductedRunaway.tag.Contains("Dino") == true)
            {
                abductedRunaway.tag = "AbductedDino";
            }

            abductRunaway();
        }
        else if(Input.GetMouseButton(0) == false && abductedRunaway != null && abductedRunaway.tag == "AbductedNeanderthal")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            abductedRunaway.tag = "FallingNeanderthal";
            GlobalVariables.tractorBeamStatus = true;
        }
        else if(Input.GetMouseButton(0) == false && abductedRunaway != null && abductedRunaway.tag == "AbductedDino")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            abductedRunaway.tag = "FallingDino";
            GlobalVariables.tractorBeamStatus = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(abductedRunaway != null && abductedRunaway.tag == "AbductedNeanderthal")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GlobalVariables.tractorBeamStatus = true;
            abductedRunaway.tag = "FallingNeanderthal";
        }
        else if(abductedRunaway != null && abductedRunaway.tag == "AbductedDino")
        {
            abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GlobalVariables.tractorBeamStatus = true;
            abductedRunaway.tag = "FallingDino";
        }
    }

    private void Update()
    {
        if(GlobalVariables.tractorBeamStatus == false)
        {
            setAbductedRunawayPosition();
        }
    }
}
