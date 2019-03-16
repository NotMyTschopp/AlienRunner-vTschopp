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

    private int offset = 200; // Yeah, you could calculate the width of the runaway, but that's not necessary.

    private GameObject ufo;

    private void killRunaway()
    {
        if(this.tag == "NeanderthalLeft"
            && this.transform.localPosition.x
            > GlobalVariables.resolution.x / 2 + offset)
        {
            Destroy(this.gameObject);
        }
        else if(this.tag == "NeanderthalRight"
            && this.transform.localPosition.x
            < GlobalVariables.resolution.x / 2 * (-1) - offset)
        {
            Destroy(this.gameObject);
        }
        else if(this.transform.localPosition.y
            + this.GetComponent<RectTransform>().rect.height / 2
            > ufo.transform.localPosition.y - ufo.GetComponent<RectTransform>().rect.height / 2)
        {
            Destroy(this.gameObject);
            GlobalVariables.tractorBeamStatus = true;
        }
    }

    // Use OnCollisionStay2D() to avoid falling through the ground.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(tag.Contains("Falling") == true && collision.gameObject.name == "Ground")
        {
            if(GlobalVariables.flipCoin() == true)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                tag = "NeanderthalLeft";
            }
            else if(GlobalVariables.flipCoin() == false)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                tag = "NeanderthalRight";
            }
        }
    }

    private void Start()
    {
        ufo = GameObject.Find("UFO");
    }

    private void Update()
    {
        killRunaway();
    }
}
