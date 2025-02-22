using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopQuiz : MonoBehaviour, IItem
{
    public static event Action<int> OnQuizCollect;
    public int worth;

    public void Collect()
    {
        OnQuizCollect.Invoke(worth);
        Destroy(gameObject);
    }

}
