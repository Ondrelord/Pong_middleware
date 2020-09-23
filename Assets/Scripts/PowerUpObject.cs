using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpObject : MonoBehaviour
{
    public float speed;
    private float lerpTime = 0f;
    private Vector3 spawnPos;

    public TextMeshProUGUI distanceText;

    public FMOD.Studio.EventInstance instance;

    public Power power;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/PowerPickUp");
        spawnPos = transform.position;
        GetComponent<MeshRenderer>().material.color = power.powerColor;
    }

    // Update is called once per frame
    void Update()
    {
        lerpTime += Time.deltaTime * speed;
        transform.position = new Vector3(spawnPos.x, spawnPos.y, Mathf.Lerp(spawnPos.z, -1f, lerpTime));

        distanceText.SetText(transform.position.z.ToString("0.0"));
        distanceText.transform.parent.position = transform.position + (distanceText.transform.localToWorldMatrix.MultiplyVector(new Vector3(0.75f, 0.75f, -0.5f)));
        distanceText.transform.parent.rotation = Quaternion.Euler(Vector3.zero);
        distanceText.color = Color.Lerp(Color.red, Color.yellow, Mathf.Clamp(transform.position.z - 1f, 0f, 2f) / 2);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            power.PickUp();
            instance.start();
            Destroy(gameObject);
        }
    }

}
