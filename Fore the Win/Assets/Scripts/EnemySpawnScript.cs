using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnScript : MonoBehaviour
{
    // array order: BouncyBoi, Crwaller, Charger
    public int[] targetAmt = {3, 3, 3}; // target amount of each enemy
    public int[] curAmt = {0, 0, 0}; // current amount of each enemy
    public float nmeLiving = 0; // amount of living enemies currently on field
    public float max;
    public float nmeRemaining; // total amount of enemies that should be on the field
    public GameObject bboi;
    public GameObject crw;
    public GameObject ch;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        nmeRemaining = FindTotal(targetAmt);
        nmeLiving = 0;
        //max = 3;
        Debug.Log(nmeRemaining);
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        // TODO spawn new enemy type, leave it for like 5 sec
        //while (FindTotal(curAmt) < FindTotal(targetAmt))
        //{
        yield return new WaitForSeconds(1);
        //Debug.Log("waited 1.5s");
        while(FindTotal(curAmt) < FindTotal(targetAmt))
        {
            //Debug.Log("FindTotal(curAmt) < FindTotal(targetAmt)");
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            if (nmeLiving < max)
            {
                int chosen = Random.Range(0, 3);
                if (curAmt[chosen] < targetAmt[chosen])// && (nmeLiving < max))
                // while there's less than max amt of a specific enemy and less than max amt of enemies on field
                {
                    float y = Random.Range(0.0f, 0.75f);
                    Debug.Log(y);
                    yield return new WaitForSeconds(y);
                    //Debug.Log("(curAmt[chosen] < targetAmt[chosen]) && (nmeLiving < max)");
                    curAmt[chosen] += 1;
                    nmeLiving += 1;
                    //nmeRemaining += 1;
                    //Debug.Log("Num: " + chosen + " curAmt: " + curAmt[chosen] + " targetAmt: " + targetAmt[chosen]);
                    switch (chosen)
                    {
                        case 0:
                            var newEnemy1 = Instantiate(bboi, transform.position + new Vector3(Random.value * 2, 0, Random.value * 4 - 2),
                                transform.rotation);
                            newEnemy1.GetComponent<Unit>().spawner = this;
                            //nmeCurrent++;
                            break;
                        case 1:
                            var newEnemy2 = Instantiate(crw, transform.position + new Vector3(Random.value * 2, 0, Random.value * 4 - 2),
                                transform.rotation);
                            newEnemy2.GetComponent<Unit>().spawner = this;
                            //nmeCurrent++;
                            break;
                        case 2:
                            var newEnemy3 = Instantiate(ch, transform.position + new Vector3(Random.value * 2, 0, Random.value * 4 - 2),
                                transform.rotation);
                            newEnemy3.GetComponent<Unit>().spawner = this;
                            //nmeCurrent++;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        Debug.Log("finished");
        //}

        /* crab spawner
        while (true)
        {
            var newEnemy3 = Instantiate(crw, transform.position + new Vector3(Random.value * 2, 0, Random.value * 4 - 2),
                    transform.rotation);
            yield return new WaitForSeconds(2);
        }*/
    }

    int FindTotal(int[] arr)
    {
        int total = 0;
        foreach(int amt in arr)
        {
            total += amt;
        }

        return total;
    }

    public void DecrementLiving()
    {
        nmeRemaining -= 1;
        nmeLiving -= 1;
        //nmeCurrent--;
        //Debug.Log(nmeRemaining);
        if(nmeRemaining == 0)
        {
            StartCoroutine(EndGame());
        }
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            cooldown = Random.Range(1, 4);
            //Debug.Log("enemy spawn " + cooldown);
        }
               
    }

    IEnumerator EndGame()
    {
        //Debug.Log("good job");
        yield return new WaitForSeconds(1);
        if(SceneManager.GetActiveScene().buildIndex == 6){
            SceneManager.LoadScene(7);
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
