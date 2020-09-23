using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration;
    private float startShakeDuration;
    public float strenght;
    private float startStrenght;

    public Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        startStrenght = strenght;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0f)
        {
            strenght = Mathf.Lerp(startStrenght, 0f, 1f - shakeDuration / startShakeDuration);

            transform.localPosition = originalPos + Random.insideUnitSphere * strenght;

            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeDuration = 0f;

            originalPos = transform.localPosition;
        }
    }

    public void ShakeFor(float duration) => startShakeDuration = shakeDuration = duration;
}