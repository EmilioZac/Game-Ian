using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    public Transform player;
    private float lookSpeed = 2.0f;
    private float verticalLimit = 60.0f;
    private float verticalRotation = 0.0f;
    private float horizontalRotation = 0.0f;
    private float horizontalLimit = 60.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(-1, 6, -11);
        float horizontal = Input.GetAxis("Mouse X") * lookSpeed;
        float vertical = Input.GetAxis("Mouse Y") * lookSpeed;
        player.Rotate(vertical, horizontal, 0);
        verticalRotation -= vertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLimit, verticalLimit);
        horizontalRotation += horizontal;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -horizontalLimit, horizontalLimit);
        transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);

    }
}
