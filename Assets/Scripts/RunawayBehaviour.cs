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

    private void killRunaway()
    {
        if(this.gameObject.tag == "NeanderthalLeft"
            && this.gameObject.transform.localPosition.x
            > GlobalVariables.resolution.x / 2 + offset)
        {
            Destroy(this.gameObject);
        }
        else if(this.gameObject.tag == "NeanderthalRight"
            && this.gameObject.transform.localPosition.x
            < GlobalVariables.resolution.x / 2 * (-1) - offset)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        killRunaway();
    }
}
