// 'RunawayBehaviour.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script manages the individual behaviour of the runaways.

using UnityEngine;

public class RunawayBehaviour : MonoBehaviour {

    private int offset = 100;

    private void KillRunaway()
    {
        if(tag.Contains("Left") == true)
        {
            if(gameObject.transform.localPosition.x > GlobalVariables.mammoth.transform.localPosition.x * (-1) + offset)
            {
                Destroy(gameObject);
            }
        }
        else if(tag.Contains("Right") == true)
        {
            if(gameObject.transform.localPosition.x < GlobalVariables.mammoth.transform.localPosition.x - offset)
            {
                Destroy(gameObject);
            }
        }
    }

    // Use OnCollisionStay2D() to avoid falling through the ground.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(tag.Contains("Falling") == true && collision.gameObject.name == "GroundLayer")
        {
            if(GlobalVariables.flipCoin() == true)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                if (tag.Contains("Human") == true)
                {
                    tag = "HumanLeft";
                }
                else
                {
                    tag = "MammothLeft";
                }
            }
            else if(GlobalVariables.flipCoin() == false)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                if (tag.Contains("Human") == true)
                {
                    tag = "HumanRight";
                }
                else
                {
                    tag = "MammothRight";
                }
            }
        }
    }

    private void Update()
    {
        KillRunaway();
    }
}
