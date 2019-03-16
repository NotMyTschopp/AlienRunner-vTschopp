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

    private float timer = 2;
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
        if(setRunaway == "neanderthal")
        {
            runawayPrefab = neanderthalPrefab;
            SetPositionFromBottom(runawayPrefab);
        }
        else if(setRunaway == "dino")
        {
            runawayPrefab = dinoPrefab;
            SetPositionFromBottom(runawayPrefab);
        }
        else
        {
            Debug.Log("ERROR_1: Runaway '" + setRunaway + "' doesn't exist.");
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
            else
            {
                Debug.Log("ERROR_2: No runaway with a desired tag.");
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

            runaway.tag = "NeanderthalLeft";
        }
        else
        {
            startPosition = GlobalVariables.resolution.x / 2
                + runawayPrefab.GetComponent<RectTransform>().rect.width / 2;

            runaway = Instantiate(runawayPrefab, GlobalVariables.gameCanvas.transform, false);
            runaway.transform.localPosition = new Vector2(startPosition, positionFromBottom);

            runaway.tag = "NeanderthalRight";
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            SpawnRunaway("neanderthal");
            timer = 2;
        }
    }

    private void FixedUpdate()
    {
        runawayAI(GameObject.FindGameObjectsWithTag("NeanderthalLeft"));
        runawayAI(GameObject.FindGameObjectsWithTag("NeanderthalRight"));
    }
}
