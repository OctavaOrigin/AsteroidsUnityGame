using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    public static float resetTime;
    private static float lastTimeUsed;
    static float usageDuration;
    static int charges;
    static int maxCharges;

    public static bool isReadyToFire()
    {
        return ((lastTimeUsed >= resetTime) || (charges > 0));
    }

    public static bool isEnoughCharged()
    {
        return lastTimeUsed >= resetTime;
    }

    public static float GetUsageDuration()
    {
        return usageDuration;
    }

    public static void UseLaser()
    {
        lastTimeUsed = 0;
        charges -= 1;

        Debug.Log(charges);
    }

    public static float TimeSinceLastUsed()
    {
        return lastTimeUsed;
    }

    public static void SetUpLaser()
    {
        resetTime = 15f;
        lastTimeUsed = 0f;
        usageDuration = 2f;
        charges = 0;
        maxCharges = 2;
    }

    public static void CountLaserReset()
    {
        if (!isEnoughCharged())
        {
            lastTimeUsed += Time.deltaTime;
        }
        else
        {
            if (charges != maxCharges)
            {
                charges++;
                lastTimeUsed = 0;
            }
        }
    }

    public static int GetCharges()
    {
        return charges;
    }

    public static int GetMaxCharges()
    {
        return maxCharges;
    }
}
