using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public Timer timer;
    public CameraPosition cam;
    bool finished = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.timerFinished && !finished)
        {
            //cam.MoveToPosition(3);
            finished = true;
        }
    }

    public void changeFinished()
    {
        finished = false;
    }
}
