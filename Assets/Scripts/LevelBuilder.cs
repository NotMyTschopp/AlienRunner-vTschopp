// 'LevelBuilder.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script builds the level.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    [SerializeField] GameObject controlsScript;

    [SerializeField] Canvas gameCanvas;

    [SerializeField] GameObject ufoPrefab;
    [SerializeField] GameObject neanderthalPrefab;
    [SerializeField] GameObject dinoPrefab;
    [SerializeField] GameObject groundPrefab;

    private GameObject ufo;
    private GameObject neanderthal;
    private GameObject dino;
    private GameObject ground;

    private Controls controls;

    private int ufoPositionFromTop = 540 - 300;

    private void SpawnUFO()
    {
        ufo = Instantiate(ufoPrefab, gameCanvas.transform, false);
        ufo.transform.localPosition = new Vector2(0, ufoPositionFromTop);

        ufo.name = "UFO";
    }

    private void SpawnRunaways()
    {

    }

    private void CreateGround()
    {
        ground = Instantiate(groundPrefab, gameCanvas.transform, false);
        ground.transform.localPosition = new Vector2(0, -540);
    }

    // Use this for initialization
    private void Start()
    {
        SpawnUFO();
        CreateGround();

        controls = controlsScript.GetComponent<Controls>();
        controls.SetVariables();
    }
	
	// Update is called once per frame
	private void Update()
    {
        controls.SetUFOPosition();
	}
}
