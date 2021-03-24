using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticTutorialStage 
{
    public enum TutorStages
    {
        NotStarted,
        First,
        Second,
        Third,
        Finished
    }
    public static TutorStages Stage;
}
