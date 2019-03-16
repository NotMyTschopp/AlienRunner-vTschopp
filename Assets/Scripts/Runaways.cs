// 'Runaways.cs' is part of AlienRunner-vTschopp
// and licensed under the BSD 2-Clause License.
//
// For more information visit:
// https://github.com/NotMyTschopp/AlienRunner-vTschopp
//
//
// This script spawns and controls the runaways.

using UnityEngine;

public class Runaways : MonoBehaviour {

    [SerializeField] GameObject neanderthalPrefab;
    [SerializeField] GameObject dinoPrefab;
    [SerializeField] GameObject ground;

    private GameObject runaway;
    private GameObject runawayPrefab;

    private float spawnTimeNeanderthals = 2;
    private float spawnTimeDinos = 10;
    private float startPosition;
    private float positionFromBottom;

    private void SetPositionFromBottom(GameObject prefab)
    {
        positionFromBottom = GlobalVariables.resolution.y / 2 * (-1)
            + prefab.GetComponent<RectTransform>().rect.height / 2
            + ground.GetComponent<RectTransform>().rect.height / 2;
    }

    private void SetRunaway(string setRunaway)
    {
        if(setRunaway == "Neanderthal")
        {
            runawayPrefab = neanderthalPrefab;
            SetPositionFromBottom(runawayPrefab);
        }
        else if(setRunaway == "Dino")
        {
            runawayPrefab = dinoPrefab;
            SetPositionFromBottom(runawayPrefab);
        }
        else
        {
            Debug.LogError("ERROR_1: Runaway '" + setRunaway + "' doesn't exist.");
        }
    }

    private void runawayAI(GameObject[] setRunaway)
    {
        for(int i = 0; i < setRunaway.Length; i++)
        {
            if(setRunaway[i].tag == "NeanderthalLeft")
            {
                setRunaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.neanderthalSpeed;
            }
            else if(setRunaway[i].tag == "NeanderthalRight")
            {
                setRunaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.neanderthalSpeed * (-1);
            }
            else if(setRunaway[i].tag == "DinoLeft")
            {
                setRunaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.dinoSpeed;
            }
            else if(setRunaway[i].tag == "DinoRight")
            {
                setRunaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.dinoSpeed * (-1);
            }
            else
            {
                Debug.LogError("ERROR_2: No runaway with desired tag.");
            }
        }
    }

    private void SpawnRunaway(string setRunaway)
    {
        SetRunaway(setRunaway);

        if(GlobalVariables.flipCoin() == true)
        {
            startPosition = GlobalVariables.resolution.x / 2 * (-1)
                - runawayPrefab.GetComponent<RectTransform>().rect.width / 2;

            runaway = Instantiate(runawayPrefab, GlobalVariables.gameCanvas.transform, false);
            runaway.transform.localPosition = new Vector2(startPosition, positionFromBottom);

            runaway.tag = setRunaway + "Left";
        }
        else
        {
            startPosition = GlobalVariables.resolution.x / 2
                + runawayPrefab.GetComponent<RectTransform>().rect.width / 2;

            runaway = Instantiate(runawayPrefab, GlobalVariables.gameCanvas.transform, false);
            runaway.transform.localPosition = new Vector2(startPosition, positionFromBottom);

            runaway.tag = setRunaway + "Right";
        }
    }

    private void Update()
    {
        spawnTimeNeanderthals -= Time.deltaTime;
        spawnTimeDinos -= Time.deltaTime;

        Debug.Log("Neanderthals: " + spawnTimeNeanderthals + "; Dinos: " + spawnTimeDinos + ";");

        if (spawnTimeNeanderthals < 0)
        {
            SpawnRunaway("Neanderthal");
            spawnTimeNeanderthals = GlobalVariables.spawnTimeNeanderthals;
        }
        else if(spawnTimeDinos < 0)
        {
            SpawnRunaway("Dino");
            spawnTimeDinos = GlobalVariables.spawnTimeDinos;
        }
    }

    private void FixedUpdate()
    {
        runawayAI(GameObject.FindGameObjectsWithTag("NeanderthalLeft"));
        runawayAI(GameObject.FindGameObjectsWithTag("NeanderthalRight"));
        runawayAI(GameObject.FindGameObjectsWithTag("DinoLeft"));
        runawayAI(GameObject.FindGameObjectsWithTag("DinoRight"));
    }
}
