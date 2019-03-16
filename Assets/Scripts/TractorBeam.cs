﻿// 'TractorBeam.cs' is part of AlienRunner-vTschopp
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

    private Controls controls;

    private float smoothTime = 0.5f;

    private Vector2 velocity;

    private int checkTag(GameObject objectToCheck)
    {
        if(objectToCheck.gameObject.tag == "NeanderthalLeft"
            || objectToCheck.gameObject.tag == "NeanderthalRight")
        {
            return 0;
        }
        else if(objectToCheck.gameObject.tag == "DinoLeft"
            || objectToCheck.gameObject.tag == "DinoRight")
        {
            return 1;
        }
        else
        {
            Debug.Log("ERROR_3: No runaway with a desired tag.");
            return 2;
        }
    }

    private void abductRunaway(GameObject setRunaway)
    {
        GlobalVariables.tractorBeamStatus = false;
        setRunaway.gameObject.GetComponent<Rigidbody2D>().velocity = GlobalVariables.tractorBeamSpeed;
    }

    private void setAbductedRunawaysPosition(GameObject setRunaway)
    {
        setRunaway.transform.localPosition = Vector2.SmoothDamp(setRunaway.transform.localPosition,
            controls.GetMousePosition() * new Vector2(1, 0) + new Vector2(0, setRunaway.transform.localPosition.y),
            ref velocity, smoothTime);
    }

    private void Start()
    {
        controlsScript = GameObject.Find("Controls");
        controls = controlsScript.GetComponent<Controls>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetMouseButton(0) && GlobalVariables.tractorBeamStatus == true && checkTag(collision.gameObject) == 0)
        {
            collision.gameObject.tag = "AbductedNeanderthal"; // Ignore by fixed update in 'Runaway.cs'!
            abductRunaway(collision.gameObject);
        }
        else if(Input.GetMouseButton(0) && GlobalVariables.tractorBeamStatus == true && checkTag(collision.gameObject) == 1)
        {
            collision.gameObject.tag = "AbductedDino"; // Ignore by fixed update in 'Runaway.cs'!
            abductRunaway(collision.gameObject);
        }
    }

    private void Update()
    {
        if(GlobalVariables.tractorBeamStatus == false)
        {
            setAbductedRunawaysPosition(GameObject.FindGameObjectWithTag("AbductedNeanderthal"));
        }
    }
}
