using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCameraHandler : MonoBehaviour
{
    private BallController ball;
    public FMOD.Studio.EventInstance instance;

    public float FMODspeed;
    public float Ballspeed;

    public float transitionTime;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Level 0");
        instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == null) ball = FindObjectOfType<BallController>();
        if (ball == null) return;

        Ballspeed = Mathf.RoundToInt(ball.speed);

        instance.setParameterByName("Speed", Mathf.Lerp(FMODspeed, Ballspeed, Time.deltaTime / transitionTime));
        instance.getParameterByName("Speed", out FMODspeed);
    }

    void OnDestroy()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }    


}
