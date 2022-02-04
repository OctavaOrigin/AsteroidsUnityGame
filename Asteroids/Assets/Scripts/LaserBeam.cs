using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    public static float resetTime;
    private static float lastTimeUsed;
    static float usageDuration;

    public static bool isReadyToFire()
    {
        return (lastTimeUsed >= resetTime);
    }

    public static float GetUsageDuration()
    {
        return usageDuration;
    }

    public static void UseLaser()
    {
        lastTimeUsed = 0;
    }

    public static float TimeSinceLastUsed()
    {
        return lastTimeUsed;
    }

    public static void SetUpLaser()
    {
        resetTime = 4f;
        lastTimeUsed = 0f;
        usageDuration = 2f;
    }

    public static void CountLaserReset()
    {
        if (!isReadyToFire())
        {
            lastTimeUsed += Time.deltaTime;
        }
    }
}
