﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointScript : MonoBehaviour
{
    [SerializeField] GameObject ArrowForward;
    [SerializeField] GameObject ArrowBack;
    [SerializeField] GameObject ArrowLeft;
    [SerializeField] GameObject ArrowRight;

    public void Forward(bool state)
    {
        ArrowForward.SetActive(state);
    }
    public void Back(bool state)
    {
        ArrowBack.SetActive(state);
    }
    public void Left(bool state)
    {
        ArrowLeft.SetActive(state);
    }
    public void Right(bool state)
    {
        ArrowRight.SetActive(state);
    }
}
