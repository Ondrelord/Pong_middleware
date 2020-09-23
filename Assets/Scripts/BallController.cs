using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    //public Transform paddleReflectionPoint;

    public float speed;

    //public ArenaManager manager;

    public TextMeshProUGUI distanceText;

    private Vector3 oldVelocity;
    private float hitByPaddle;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    void Update()
    {
        distanceText.SetText(transform.position.z.ToString("0.0"));
        distanceText.transform.parent.position = transform.position + (distanceText.transform.localToWorldMatrix.MultiplyVector(new Vector3(0.75f, 0.75f, -0.5f)) );
        distanceText.transform.parent.rotation = Quaternion.Euler(Vector3.zero);
        distanceText.color = Color.Lerp(Color.red, Color.yellow, Mathf.Clamp(transform.position.z - 1f, 0f, 2f)/2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //getting velocity for ricochet
        oldVelocity = rb.velocity;

        //delay for multiple paddle hits
        if (hitByPaddle >= 0)
            hitByPaddle -= Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 reflectNormal;

        if (collision.gameObject.tag == "Death")
        {
            //manager.LostLife();

            //transform.position = new Vector3(0, 0, 0.5f);
            //rb.velocity = Vector3.zero;
            GetComponent<TrailRenderer>().Clear();

            Destroy(gameObject);

            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            //if hit by paddle recently, ignore next hits
            if (hitByPaddle > 0)
                return;
            hitByPaddle = 0.7f;

            reflectNormal = (transform.position - collision.transform.GetChild(0).position) / Vector3.Distance(transform.position, collision.transform.position);
            
            Camera.main.GetComponent<CameraShake>().ShakeFor(0.5f);
        }
        else
        {
            // get the point of contact
            ContactPoint contact = collision.contacts[0];
            reflectNormal = contact.normal;
        }
        // reflect our old velocity off the contact point's normal vector
        Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, reflectNormal);

        // assign the reflected velocity back to the rigidbody
        rb.velocity = reflectedVelocity.normalized * speed;

        // rotate the object by the same ammount we changed its velocity
        Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
        transform.rotation = rotation * transform.rotation;
    }

}
