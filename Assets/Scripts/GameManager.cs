using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public GameObject playerPrefab;
    public GameObject meteorPrefab;
    public GameObject bigMeteorPrefab;
    public bool gameOver = false;
    public Transform cameraTransform;

    Vector2 meteorSpawnRange = new(8, 7.5f);

    public int meteorCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(playerPrefab, transform.position, Quaternion.identity);
        InvokeRepeating("SpawnMeteor", 1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            CancelInvoke();
        }

        if (meteorCount == 5)
        {
            BigMeteor();
        }
    }

    void SpawnMeteor()
    {
        Instantiate(meteorPrefab, meteorPosition(), Quaternion.identity);
    }

    void BigMeteor()
    {
        meteorCount = 0;
        Instantiate(bigMeteorPrefab, meteorPosition(), Quaternion.identity);
    }

    Vector3 meteorPosition()
    {
        return 
            new Vector3(cameraTransform.position.x, cameraTransform.position.y, 0) + 
            new Vector3(Random.Range(-meteorSpawnRange.x, meteorSpawnRange.x), meteorSpawnRange.y, 0);
    }
}
