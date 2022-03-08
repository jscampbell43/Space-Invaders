using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Shield;
    public Transform levelRoot;
    public int numShields = 3;

    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        // Create numShields amount of Shields
        for (float i = 0; i < numShields; i++)
        {
            // Create 4X4 unit shields
            for (float j = 0; j < 4; j++)
            {
                for (float k = 0; k < 4; k++)
                {
                    // j and k offset is to move shields left 10 spaces and down 13 spaces.
                    // i multiplier puts a space of 10 between each shield
                    var spawnedShield = Instantiate(Shield, new Vector3((j-10)+(i*10), k - 13f, 0f), levelRoot.rotation);
                }
            }
        }


    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
