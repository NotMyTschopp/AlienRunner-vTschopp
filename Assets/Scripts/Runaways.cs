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

    [SerializeField] GameObject groundReference;

    private GameObject runawayPrefab;
    private GameObject runawayInstance;

    private float humanSpawnTime = 2;
    private float mammothSpawnTime = 10;

    private void SetRunaway(string runaway)
    {
        if(runaway == "Human")
        {
            runawayPrefab = GlobalVariables.human;
            runawayPrefab.transform.localPosition = GlobalVariables.human.transform.localPosition;
        }
        else if(runaway == "Mammoth")
        {
            runawayPrefab = GlobalVariables.mammoth;
            runawayPrefab.transform.localPosition = GlobalVariables.mammoth.transform.localPosition;
        }
        else
        {
            Debug.LogError("ERROR_1: Runaway '" + runaway + "' doesn't exist.");
        }
    }

    private void runawayAI(GameObject[] runaway)
    {
        for(int i = 0; i < runaway.Length; i++)
        {
            if(runaway[i].tag == "HumanLeft")
            {
                runaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.humanSpeed;
            }
            else if(runaway[i].tag == "HumanRight")
            {
                runaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.humanSpeed * (-1);
            }
            else if(runaway[i].tag == "MammothLeft")
            {
                runaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.mammothSpeed;
            }
            else if(runaway[i].tag == "MammothRight")
            {
                runaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.mammothSpeed * (-1);
            }
            else
            {
                Debug.LogError("ERROR_2: No runaway with desired tag.");
            }
        }
    }

    private void SpawnRunaway(string runaway)
    {
        SetRunaway(runaway);

        if(GlobalVariables.flipCoin() == true)
        {
            runawayInstance = Instantiate(runawayPrefab, GlobalVariables.gameCanvas.transform, false);
            runawayInstance.transform.localPosition = runawayPrefab.transform.localPosition;

            runawayInstance.tag = runaway + "Left";
        }
        else
        {
            runawayInstance = Instantiate(runawayPrefab, GlobalVariables.gameCanvas.transform, false);
            runawayInstance.transform.localPosition = runawayPrefab.transform.localPosition * new Vector2(-1, 1);

            runawayInstance.tag = runaway + "Right";
        }
    }

    private void Update()
    {
        humanSpawnTime -= Time.deltaTime;
        mammothSpawnTime -= Time.deltaTime;

        if (humanSpawnTime < 0)
        {
            SpawnRunaway("Human");
            humanSpawnTime = GlobalVariables.humanSpawnTime;
        }
        else if(mammothSpawnTime < 0)
        {
            SpawnRunaway("Mammoth");
            mammothSpawnTime = GlobalVariables.mammothSpawnTime;
        }
    }

    private void FixedUpdate()
    {
        runawayAI(GameObject.FindGameObjectsWithTag("HumanLeft"));
        runawayAI(GameObject.FindGameObjectsWithTag("HumanRight"));
        runawayAI(GameObject.FindGameObjectsWithTag("MammothLeft"));
        runawayAI(GameObject.FindGameObjectsWithTag("MammothRight"));
    }
}
