using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject bigRock;
    [SerializeField] GameObject miniRock;
    [SerializeField] GameObject spaceShip;
    private float delayBetweenSpawns = 2f;
    Vector2 spawningPoint;

    Vector2 minCorner,maxCorner;

    Coroutine currentRockCoroutine, currentShipCoroutine;

    float extend;
    public static int playersScore;

    private void Start()
    {
        playersScore = 0;

        minCorner = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        maxCorner = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        extend = bigRock.GetComponent<PolygonCollider2D>().bounds.max.x * 2;
    }



    private void Update()
    {        
        if (currentRockCoroutine == null)
            currentRockCoroutine = StartCoroutine(SpawnRocks());

        if (currentShipCoroutine == null)
            currentShipCoroutine = StartCoroutine(SpawnShips());

    }

    public void BigRockDies()
    {
        playersScore += 10;
    }

    public void MiniRockDies()
    {
        playersScore += 15;
    }

    public void spaceShipDies()
    {
        playersScore += 20;
    }

    private void FindPlaceToSpawn()
    {
        int randomNumber = Random.Range(1,5);
        float randomX = Random.Range(minCorner.x, maxCorner.x);
        float randomY = Random.Range(minCorner.y, maxCorner.y);


        if (randomNumber == 1)
        {
            spawningPoint = new Vector2(randomX,maxCorner.y + extend);
        }else if(randomNumber == 2){
            spawningPoint = new Vector2(randomX,minCorner.y - extend);
        }else if(randomNumber == 3){
            spawningPoint = new Vector2(maxCorner.x + extend,randomY);
        }
        else
        {
            spawningPoint = new Vector2(minCorner.x - extend,randomY);
        }

    }

    IEnumerator SpawnRocks()
    {
        FindPlaceToSpawn();
        Instantiate(bigRock,spawningPoint,Quaternion.identity);
        yield return new WaitForSeconds(delayBetweenSpawns);
        currentRockCoroutine = null;
    }

    IEnumerator SpawnShips()
    {
        FindPlaceToSpawn();
        Instantiate(spaceShip,spawningPoint,Quaternion.identity);
        yield return new WaitForSeconds(delayBetweenSpawns);
        currentShipCoroutine = null;
    }
}
