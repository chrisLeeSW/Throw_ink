using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSubController : MonoBehaviour
{
    public List<GameObject> stars;
    public List<GameObject> ranks;
    private int currentStar;

    public int CurrentStar
    {
        get { return currentStar; }
        set
        {
            currentStar = value;
        }
    }

    private void Awake()
    {
        if(!gameObject.activeSelf) 
        {
            return;
        }
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].SetActive(false);
            ranks[i].SetActive(false);
        }
        switch (CurrentStar)
        {
            case 0:
                ranks[CurrentStar].SetActive(true);
                break;
            case 1:
                stars[CurrentStar].SetActive(true);
                ranks[CurrentStar].SetActive(true);
                break;
            case 2:
                for (int i = 0; i < CurrentStar; ++i)
                {
                    stars[i].SetActive(true);
                }
                ranks[CurrentStar].SetActive(true);
                break;
            case 3:
                for (int i = 0; i < CurrentStar; ++i)
                {
                    stars[i].SetActive(true);
                }
                ranks[CurrentStar].SetActive(true);
                break;
        }
    }
}
