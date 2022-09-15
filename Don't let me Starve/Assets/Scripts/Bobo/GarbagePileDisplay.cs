using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbagePileDisplay : MonoBehaviour
{
    public ParticleSystem garbageRain;

    public GameObject[] pileStates;
    private int pileStateIndex;

    public int garbage;

    // Start is called before the first frame update
    void Start()
    {
        NextDay();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextDayDelay()
    {
        Invoke("NextDay", 2.5f);
    }
    public void NextDay()
    {
        StartCoroutine(GarbagePileChange());
    }

    public IEnumerator GarbagePileChange()
    {
        garbageRain.Play();

        yield return new WaitForSeconds(3f);

        CheckGarbagePile();
    }

    public void CheckGarbagePile()
    {
        if (garbage == 0)
        {
            pileStateIndex = 0;
        }
        else if (garbage > 0 && garbage < 25)
        {
            pileStateIndex = 1;
        }
        else if (garbage >= 25 && garbage < 50)
        {
            pileStateIndex = 2;
        }
        else if (garbage >= 50 && garbage < 75)
        {
            pileStateIndex = 3;
        }
        else if (garbage >= 75)
        {
            pileStateIndex = 4;
        }

        foreach (GameObject pile in pileStates)
        {
            pile.SetActive(false);
        }

        pileStates[pileStateIndex].SetActive(true);
    }

    void LoseCondition()
    {
        if(garbage > 100)
        {
            Debug.Log("GAME OVER");
        }
    }
}
