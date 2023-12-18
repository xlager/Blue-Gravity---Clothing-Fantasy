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
    [SerializeField] GameObject options; 
    private List<NPC> interactibles;
    bool canMove = true;
    bool isMoving = false;
    bool isInteracting = false;
    public bool IsIntereacting => isInteracting || inventoryOpen;
    private bool inventoryOpen;

    public void Start()
    {
        interactibles = FindObjectsOfType<NPC>().ToList();
        foreach (var item in interactibles)
        {
            item.onInteracted.AddListener(CantMove);
            item.onInteracted.AddListener(Interacting);
        }
        DialogueUISingleton.Instance.onCloseAll.AddListener(CanMove);
        DialogueUISingleton.Instance.onCloseAll.AddListener(StopInteracting);
        Consistency.Instance.onClothesChanged.AddListener(ChangeEquip);
        SetupEquipment();
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        CheckInventory();
        HasStopedMoving();
        CheckForOptions();
        
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

    public void CantMove()
    {
        canMove = false;
    }
    void CanMove()
    {
        canMove = true;
    }
    public void Interacting()
    {
        isInteracting = true;
    }
    public void StopInteracting()
    {
        isInteracting = false;
    }

    public void ChangeEquip(ClothesClass cloth)
    {
        foreach (var item in clothes)
        {
            if (cloth.identificator == item.identificator)
            {
                item.library.spriteLibraryAsset = cloth.libraryAsset;
                if(!Consistency.Instance.equippedClothes.list.Contains(cloth))
                    Consistency.Instance.equippedClothes.list.Add(cloth);
                cloth.isEquiped = true;
            }
        }
        Consistency.Instance.SaveData();
    }

    private void CheckInventory()
    {
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            if (!isInteracting)
            {
                if (!inventoryOpen)
                    OpenInventory();
                else
                    CloseInventory();
            }
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

    private void SetupEquipment()
    {
        foreach (var item in Consistency.Instance.equippedClothes.list)
        {
            ChangeEquipNoSave(item);
        }
    }

    private void ChangeEquipNoSave(ClothesClass cloth)
    {
        foreach (var item in clothes)
        {
            if (cloth.identificator == item.identificator)
            {
                item.library.spriteLibraryAsset = cloth.libraryAsset;
              //  cloth.isEquiped = true;
            }
        }
    }
    private void CheckForOptions()
    {
        if (!isInteracting && Input.GetKey(KeyCode.Escape))
        {
            options.SetActive(true);
        }
    }
}
