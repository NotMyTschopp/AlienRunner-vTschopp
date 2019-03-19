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

    [SerializeField] GameObject ground;

    private int offset = 100;

    private void KillRunaway()
    {
        if(tag.Contains(GlobalVariables.tagLeft) == true)
        {
            if(gameObject.transform.localPosition.x > GlobalVariables.mammoth.transform.localPosition.x * (-1) + offset)
            {
                Destroy(gameObject);
            }
        }
        else if(tag.Contains(GlobalVariables.tagRight) == true)
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
        if(tag.Contains(GlobalVariables.tagFalling) == true && collision.gameObject == ground)
        {
            if(GlobalVariables.FlipCoin() == true)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                if (tag.Contains(GlobalVariables.tagPrimaryRunaway) == true)
                {
                    tag = GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagLeft;
                }
                else
                {
                    tag = GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagLeft;
                }
            }
            else if(GlobalVariables.FlipCoin() == false)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                if (tag.Contains(GlobalVariables.tagPrimaryRunaway) == true)
                {
                    tag = GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagRight;
                }
                else
                {
                    tag = GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagRight;
                }
            }
        }
    }

    private void Update()
    {
        KillRunaway();
    }
}
