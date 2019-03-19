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

    private float humanSpawnTime = 2; // First time run.
    private float mammothSpawnTime = 10; // First time run.

    private void SetRunaway(string runaway)
    {
        if(runaway == GlobalVariables.tagPrimaryRunaway)
        {
            runawayPrefab = GlobalVariables.human;
            runawayPrefab.transform.localPosition = GlobalVariables.human.transform.localPosition;
        }
        else if(runaway == GlobalVariables.tagSecondaryRunaway)
        {
            runawayPrefab = GlobalVariables.mammoth;
            runawayPrefab.transform.localPosition = GlobalVariables.mammoth.transform.localPosition;
        }
        else
        {
            Debug.LogError("ERROR_1: Runaway '" + runaway + "' doesn't exist.");
        }
    }

    private void RunawayAI(GameObject[] runaway)
    {
        for(int i = 0; i < runaway.Length; i++)
        {
            if(runaway[i].tag == GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagLeft)
            {
                runaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.humanSpeed;
            }
            else if(runaway[i].tag == GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagRight)
            {
                runaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.humanSpeed * (-1);
            }
            else if(runaway[i].tag == GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagLeft)
            {
                runaway[i].GetComponent<Rigidbody2D>().velocity = GlobalVariables.mammothSpeed;
            }
            else if(runaway[i].tag == GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagRight)
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

        if(GlobalVariables.FlipCoin() == true)
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
            SpawnRunaway(GlobalVariables.tagPrimaryRunaway);
            humanSpawnTime = GlobalVariables.humanSpawnTime;
        }
        else if(mammothSpawnTime < 0)
        {
            SpawnRunaway(GlobalVariables.tagSecondaryRunaway);
            mammothSpawnTime = GlobalVariables.mammothSpawnTime;
        }
    }

    private void FixedUpdate()
    {
        RunawayAI(GameObject.FindGameObjectsWithTag(GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagLeft));
        RunawayAI(GameObject.FindGameObjectsWithTag(GlobalVariables.tagPrimaryRunaway + GlobalVariables.tagRight));
        RunawayAI(GameObject.FindGameObjectsWithTag(GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagLeft));
        RunawayAI(GameObject.FindGameObjectsWithTag(GlobalVariables.tagSecondaryRunaway + GlobalVariables.tagRight));
    }
}
