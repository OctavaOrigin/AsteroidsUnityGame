using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] Slider reset;
    [SerializeField] TextMeshProUGUI angle;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI charges;
    [SerializeField] TextMeshProUGUI coordinates;
    [SerializeField] TextMeshProUGUI finalWords;
    [SerializeField] Button tryAgainButton;

    Player player;
    Coroutine Updating;

    bool gameStoped;


    private void Start()
    {
        gameStoped = false;
        player = FindObjectOfType<Player>();
        reset.maxValue = LaserBeam.resetTime;
    }

    private void Update()
    {
        if (!gameStoped)
        {
            if (Time.timeScale == 0)
            {
                finalWords.gameObject.SetActive(true);
                finalWords.text = finalWords.text + " " + EnemyManager.playersScore.ToString();
                tryAgainButton.gameObject.SetActive(true);
                gameStoped = true;
            }

            reset.value = LaserBeam.TimeSinceLastUsed();

            if (Updating == null)
                Updating = StartCoroutine(SlowUpdate());
        }        
    }

    IEnumerator SlowUpdate()
    {
        charges.text = LaserBeam.GetCharges().ToString();
        angle.text = (( int ) player.GetAngle()).ToString();
        speed.text = System.Math.Round(player.GetSpeed(),2).ToString();
        coordinates.text = player.GetCoordinates().ToString();

        
        yield return new WaitForSeconds(0.1f);
        Updating = null;
    }

    private void RestartLevel()
    {
        
    }
}
