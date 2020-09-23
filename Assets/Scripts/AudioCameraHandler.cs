using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCameraHandler : MonoBehaviour
{
    private BallController ball;
    public FMOD.Studio.EventInstance instance;

    public int level;

    public float FMODspeed;
    public float Ballspeed;

    public float transitionTime;

    public int blocksCount;

    // Start is called before the first frame update
    void Start()
    {
        if (level == 0)
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/Level 0");
        if (level == 1)
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/Level 1");
        if (level == 2)
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/Level 2");
        instance.start();

        blocksCount = GameObject.Find("Blocks").transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (level == 0 || level == 1)
        {
            if (ball == null) ball = FindObjectOfType<BallController>();
            if (ball == null) return;
            Ballspeed = Mathf.RoundToInt(ball.speed);
        }

        if (level == 2)
        {
            float bCount = GameObject.Find("Blocks").transform.childCount;
            Ballspeed = Mathf.Ceil(10f * ((blocksCount - bCount) / blocksCount)) ;
        }

        instance.setParameterByName("Speed", Mathf.Lerp(FMODspeed, Ballspeed, Time.deltaTime / transitionTime));
        instance.getParameterByName("Speed", out FMODspeed);
    }

    void OnDestroy()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }    


}
