using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject bigRock;
    [SerializeField] GameObject miniRock;
    [SerializeField] GameObject spaceShip;

    public void BigRockDies()
    {
        
    }

    public void MiniRockDies()
    {
        Debug.Log("MiniBlockDies");
    }

    public void spaceShipDies()
    {
        Debug.Log("spaceShipDies");
    }
}
