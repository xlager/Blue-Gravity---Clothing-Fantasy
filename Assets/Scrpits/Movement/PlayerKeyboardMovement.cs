using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerKeyboardMovement : MonoBehaviour
{
    [SerializeField] float speed=5;
    [SerializeField] Animator animator;
    private List<NPC> interactibles;
    bool canMove = true;
    bool isMoving = false;
    public void Start()
    {
        interactibles = FindObjectsOfType<NPC>().ToList();
        foreach (var item in interactibles)
        {
            item.onInteracted.AddListener(CantMove);
        }
        DialogueUISingleton.Instance.onCloseAll.AddListener(CanMove);
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        HasStopedMoving();
    }

    void PlayerMovement()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, (1 * Time.deltaTime * speed), 0);
                animator.SetBool("up", true);
                isMoving = true;
            }
            //transform.position = new Vector3(transform.position.x, transform.position.y + (1 * Time.deltaTime * speed), 0) ;
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate((-1 * Time.deltaTime * speed), 0, 0);
                animator.SetBool("left", true);
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, (-1 * Time.deltaTime * speed), 0);
                animator.SetBool("down", true);
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate((1 * Time.deltaTime * speed), 0, 0);
                animator.SetBool("right", true);
                isMoving = true;
            }
        }
    }

    private void HasStopedMoving()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("up", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("left", false);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("down", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("right", false);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            return;
        isMoving = false;





    }

    void CantMove()
    {
        canMove = false;
    }
    void CanMove()
    {
        canMove = true;
    }

}
