using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyConatiner;
    [SerializeField]
    private GameObject[] _powerUps;
    private bool _stopSpanning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn game objects every 5 seconds
    // Create a coroutine of type IEnumerator -- Yeild events
    // while loop 
    IEnumerator SpawnEnemyRoutine()
    {
        // infinite while loop
        //Instantiate enemy prefab
        //yeild wait for 5 seconds
        while(_stopSpanning == false)
        {
            Vector3 posToSpwan = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpwan, Quaternion.identity);
            newEnemy.transform.parent = _enemyConatiner.transform;
            yield return new WaitForSeconds(2.5f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        // every 3-7 seconds spawn a power up
        while (_stopSpanning == false)
        {
            Vector3 posToSpwan = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerUps[randomPowerUp], posToSpwan, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3 , 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpanning = true;
    }
}
