using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CookingMinigame : MonoBehaviour
{
    public Animator markerAnim;

    public RectTransform bar;
    public RectTransform goodZone;
    public RectTransform perfectZone;

    float barWidth;

    [Range(0.0f, 1f)]
    public float goodZonePortion;

    [Range(0.0f, 1f)]
    public float perfectZonePortion;

    bool minigamePaused;

    float goodZonePosition;
    float goodZoneStart;
    float goodZoneEnd;

    float perfectZonePosition;
    float perfectZoneStart;
    float perfectZoneEnd;

    public RectTransform marker;

    public TMP_Text scoreDisplay;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        minigamePaused = false;
        barWidth = bar.rect.width;

        goodZone.sizeDelta = new Vector2(barWidth * goodZonePortion, goodZone.sizeDelta.y);
        perfectZone.sizeDelta = new Vector2(barWidth * perfectZonePortion, perfectZone.sizeDelta.y);




        goodZonePosition = goodZone.localPosition.x;
        goodZoneStart = goodZonePosition - goodZone.rect.width/2;
        goodZoneEnd = goodZonePosition + goodZone.rect.width/2;

        perfectZonePosition = perfectZone.localPosition.x;
        perfectZoneStart = perfectZonePosition - perfectZone.rect.width/2;
        perfectZoneEnd = perfectZonePosition + perfectZone.rect.width/2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        scoreDisplay.text = score.ToString();
    }

    public void Interact()
    {
        if (!minigamePaused)
        {
            markerAnim.StopPlayback();
        }
        else
        {
            markerAnim.StartPlayback();
        }

        minigamePaused = !minigamePaused;
    }

    public void TimedClick()
    {
        CheckBar();
    }


    void CheckBar()
    {
        float markerPosition = marker.localPosition.x;

        if (markerPosition >= perfectZoneStart && markerPosition <= perfectZoneEnd)
        {
            score += 10;
            return;
        }
        else if (markerPosition >= goodZoneStart && markerPosition <= goodZoneEnd)
        {
            score += 5;
            return;
        }
        else
        {
            score -= 1;
        }
    }
}
