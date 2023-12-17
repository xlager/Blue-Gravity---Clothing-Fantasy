using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed=5;
    [SerializeField] Animator animator;
    [SerializeField] List<PlayerClothesHelper> clothes; 
    [SerializeField] GameObject inventory; 
    [SerializeField] PopUpController inventoryController; 
    private List<NPC> interactibles;
    bool canMove = true;
    bool isMoving = false;
    private bool inventoryOpen;

    public void Start()
    {
        interactibles = FindObjectsOfType<NPC>().ToList();
        foreach (var item in interactibles)
        {
            item.onInteracted.AddListener(CantMove);
        }
        DialogueUISingleton.Instance.onCloseAll.AddListener(CanMove);
        Consistency.Instance.onClothesChanged.AddListener(ChangeEquip);
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        CheckInventory();
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

    public void ChangeEquip(ClothesClass cloth)
    {
        foreach (var item in clothes)
        {
            if (cloth.identificator == item.identificator)
            {
                item.library.spriteLibraryAsset = cloth.libraryAsset;
                cloth.isEquiped = true;
            }
        }
    }

    private void CheckInventory()
    {
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            if (!inventoryOpen)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        canMove = false;
        inventoryOpen = true;
        inventory.SetActive(true);
        inventory.transform.position = Vector3.zero;
        inventoryController.IventorySetup();
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
        inventoryOpen = false;
        CanMove();
    }
}
