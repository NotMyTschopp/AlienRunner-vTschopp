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
        if(objectToCheck.tag == GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagLeft || objectToCheck.tag == GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagRight
            || objectToCheck.tag == GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagLeft || objectToCheck.tag == GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagRight)
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

        if(abductedRunaway.tag == GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagAbducted)
        {
            abductedRunaway.GetComponent<Rigidbody2D>().velocity = GlobalVariables.humanLiftSpeed;
        }
        else if(abductedRunaway.tag == GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagAbducted)
        {
            abductedRunaway.GetComponent<Rigidbody2D>().velocity = GlobalVariables.mammothLiftSpeed;
        }
    }

    private void FallingRunaway(string runaway)
    {
        abductedRunaway.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        abductedRunaway.tag = runaway + GlobalVariables.tagFalling;
        GlobalVariables.tractorBeamStatus = true;
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

            if(abductedRunaway.tag.Contains(GlobalVariables.tagPrimaryRunaway) == true)
            {
                abductedRunaway.tag = GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagAbducted;
            }
            else if(abductedRunaway.tag.Contains(GlobalVariables.tagSecondaryRunaway) == true)
            {
                abductedRunaway.tag = GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagAbducted;
            }

            AbductRunaway();
        }
        else if(abductedRunaway != null && abductedRunaway.tag == GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagAbducted && GlobalVariables.fuelPercentage <= 0)
        {
            FallingRunaway(GlobalVariables.tagPrimaryRunaway);
        }
        else if(abductedRunaway != null && abductedRunaway.tag == GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagAbducted && GlobalVariables.fuelPercentage <= 0)
        {
            FallingRunaway(GlobalVariables.tagSecondaryRunaway);
        }
        else if(Input.GetMouseButton(0) == false && abductedRunaway != null && abductedRunaway.tag == GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagAbducted)
        {
            FallingRunaway(GlobalVariables.tagPrimaryRunaway);
        }
        else if(Input.GetMouseButton(0) == false && abductedRunaway != null && abductedRunaway.tag == GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagAbducted)
        {
            FallingRunaway(GlobalVariables.tagSecondaryRunaway);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(abductedRunaway != null && collision.tag == GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagAbducted)
        {
            FallingRunaway(GlobalVariables.tagPrimaryRunaway);
        }
        else if(abductedRunaway != null && collision.tag == GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagAbducted)
        {
            FallingRunaway(GlobalVariables.tagSecondaryRunaway);
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
