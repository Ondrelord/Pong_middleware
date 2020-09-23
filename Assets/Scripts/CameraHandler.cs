using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform paddle;
    public float distanceFromPaddle;

    [Range(0,10)]
    public float followingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -distanceFromPaddle), paddle.position, followingSpeed*0.01f);
        //GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -distanceFromPaddle), paddle.position, followingSpeed * 0.01f));
    }
}
