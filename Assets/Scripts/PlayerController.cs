using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player;

    public float jumpHeight;
    public float planePosition;

    private Vector3 _mousePosition;
    private float _jump;


    // Start is called before the first frame update
    void Start()
    {

        _jump = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if ((_jump *= 0.8f) < 0.1f)
            _jump = 0;

        if (Input.GetMouseButton(0))
            _jump = jumpHeight;

        //Finding mouse to world point for player movement
        _mousePosition = Input.mousePosition;


        _mousePosition = GetWorldPositionOnPlane(_mousePosition, planePosition + _jump);

        bool upMove = Physics.Raycast(transform.position, Vector3.up, LayerMask.GetMask("Arena"));
        bool downMove = Physics.Raycast(transform.position, Vector3.down, LayerMask.GetMask("Arena"));
        bool leftMove = Physics.Raycast(transform.position, Vector3.left, LayerMask.GetMask("Arena"));
        bool rightMove = Physics.Raycast(transform.position, Vector3.right, LayerMask.GetMask("Arena"));

        Vector3 movepos = Vector3.Lerp(player.position, _mousePosition, 0.2f);
        Vector3 movedir = _mousePosition - player.position;


        if (!upMove && movedir.y > 0f) movepos.y = transform.position.y;
        if (!downMove && movedir.y < 0f) movepos.y = transform.position.y;
        if (!rightMove && movedir.x > 0f) movepos.x = transform.position.x;
        if (!leftMove && movedir.x < 0f) movepos.x = transform.position.x;


        gameObject.GetComponent<Rigidbody>().MovePosition(movepos);
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

}
