// 'LevelBuilder.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script builds the level including spawing of the UFO.

using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    [SerializeField] GameObject controlsScript;

    [SerializeField] GameObject ufoPrefab;
    [SerializeField] GameObject groundPrefab;

    private GameObject ufo;
    private GameObject ground;

    private Controls controls;

    private float ufoPositionFromTop;

    private void SpawnUFO()
    {
        ufo = Instantiate(ufoPrefab, GlobalVariables.gameCanvas.transform, false);
        ufo.transform.localPosition = new Vector2(0, ufoPositionFromTop);

        ufo.name = "UFO"; // Use 'name' instead of 'tag' because there's only one UFO.
    }

    private void CreateGround()
    {
        ground = Instantiate(groundPrefab, GlobalVariables.gameCanvas.transform, false);
        ground.transform.localPosition = new Vector2(0, -540);
    }

    // Use this for initialization
    private void Start()
    {
        controls = controlsScript.GetComponent<Controls>();
        ufoPositionFromTop = GlobalVariables.resolution.y / 2 - 300;

        SpawnUFO();
        CreateGround();

        controls.SetObjects();
    }
	
	// Update is called once per frame
	private void Update()
    {
        controls.SetUFOPosition();
	}
}
