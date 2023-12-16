using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardMovement : MonoBehaviour
{
    [SerializeField] float speed=5;
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(0, (1 * Time.deltaTime * speed), 0);
            //transform.position = new Vector3(transform.position.x, transform.position.y + (1 * Time.deltaTime * speed), 0) ;
        if (Input.GetKey(KeyCode.A))
            transform.position = new Vector3(transform.position.x - (1 * Time.deltaTime * speed), transform.position.y, 0) ;
        if (Input.GetKey(KeyCode.S))
            transform.position = new Vector3(transform.position.x, transform.position.y - (1 * Time.deltaTime * speed), 0) ;
        if (Input.GetKey(KeyCode.D))
            transform.position = new Vector3(transform.position.x + (1 * Time.deltaTime * speed), transform.position.y, 0) ;


    }
}
