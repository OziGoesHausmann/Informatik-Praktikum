using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CuttingMinigame : MonoBehaviour
{
    public RectTransform moldPos;

    public RectTransform moldCut;

    private float maxPos = 360f;


    public RectTransform goodZone;
    public RectTransform perfectZone;

    public Animator knifeAnim;


    public bool minigamePaused;

    float okayZonePosition;
    float okayZoneStart;
    float okayZoneEnd;

    float perfectZonePosition;
    float perfectZoneStart;
    float perfectZoneEnd;

    public RectTransform knifePos;

    public float score;

    [Range(0.5f, 1.5f)]
    public float skill;

    public TMP_Text result;


    public TMP_Text mealResultName;
    public TMP_Text durabilityResult;
    public TMP_Text satiationResult;
    public TMP_Text garbageResult;

    public GameObject mealResultScreen;

    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomBadPart();
        AdjustSkillSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!minigamePaused)
            {
                TimedClick();
            }

            //Interact();
        }
    }

    void SpawnRandomBadPart()
    {
        Vector3 newPos = new Vector3(Random.Range(-maxPos, maxPos), 0f, 0f);
        moldPos.localPosition += newPos;
        moldCut.localPosition += newPos;

        float xPos = moldCut.localPosition.x;

        okayZoneStart = xPos - goodZone.rect.width / 2;
        okayZoneEnd = xPos + goodZone.rect.width / 2;

        perfectZoneStart = xPos - perfectZone.rect.width / 2;
        perfectZoneEnd = xPos + perfectZone.rect.width / 2;
    }

    void AdjustSkillSpeed()
    {
        knifeAnim.speed = skill;
    }

    public void Interact()
    {
        minigamePaused = !minigamePaused;

        if (!minigamePaused)
        {
            knifeAnim.StopPlayback();
        }
        else
        {
            knifeAnim.StartPlayback();
        }
    }

    public void TimedClick()
    {
        CheckBar();
    }

    void CheckBar()
    {
        knifeAnim.StartPlayback();
        minigamePaused = true;

        float knifePosX = knifePos.localPosition.x;
        bool isChecked = false;

        if (!isChecked)
        {
            if (knifePosX >= perfectZoneStart && knifePosX <= perfectZoneEnd)
            {
                score += 1;
                Debug.Log("Perfect Cut!");
                isChecked = true;
            }
            else if (knifePosX >= okayZoneStart && knifePosX <= okayZoneEnd)
            {
                score += 0;
                Debug.Log("Okay Cut!");
                isChecked = true;
            }
            else
            {
                score += -1;
                Debug.Log("Bad Cut!");
                isChecked = true;
            }
        }

        StartCoroutine(MealResult());
    }

    IEnumerator MealResult()
    {
        switch (score)
        {
            case -1:
                result.text = "Bad Cut!";
                break;

            case 0:
                result.text = "Okay Cut!";
                break;

            case 1:
                result.text = "Perfect Cut!";
                break;
        }

        yield return new WaitForSeconds(2f);

        mealResultScreen.SetActive(true);

        if (score > 0)
        {
            durabilityResult.text = "+";
            satiationResult.text = "+";
            garbageResult.text = "+";
        }

        durabilityResult.text += score.ToString();
        satiationResult.text += (score*2).ToString();
        garbageResult.text += score.ToString();

        yield return new WaitForSeconds(3f);

        mealResultScreen.SetActive(false);

        durabilityResult.text = "";
        satiationResult.text = "";
        garbageResult.text = "";
        result.text = "";
        score = 0;

        knifeAnim.StopPlayback();
        AdjustSkillSpeed();
        minigamePaused = false;
    }
}
