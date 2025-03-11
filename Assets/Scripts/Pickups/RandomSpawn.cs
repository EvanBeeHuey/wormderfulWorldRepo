using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] googlyEyes;
    public int numOf = 5;

    void Start()
    {
        for(int i =0; i < numOf; i++)
        {
            int randomIndex = Random.Range(0, googlyEyes.Length);
            Instantiate(googlyEyes[randomIndex], new Vector3(Random.Range(-48, -32), Random.Range(-19, -12), 0f), Quaternion.identity);
        }
    }

}
