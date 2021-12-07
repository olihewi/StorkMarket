using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EvalSceneManager
{
    public static bool isInEvalScene = false;

    public static int bodyRating = 0;

    public static void getRating(List<PartAttributes> partRating)
    {
        List<PartAttributes> testList;
        testList = partRating;
        foreach (var item in testList)
        {
            Debug.Log(item.percent);
        }
    }
}
