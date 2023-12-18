using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PopUpController : MonoBehaviour
{
    [SerializeField] ItemEntry itemEntryPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Button returnButton;
    [SerializeField] Popups popupType;
    private List<ItemEntry> items;
    public void StoreSetup()
    {
        items = new List<ItemEntry>();
        foreach (var item in Consistency.Instance.lockedClothes.list)
        {

            var entry = Instantiate(itemEntryPrefab);
            entry.transform.SetParent(gridTransform);
            entry.transform.localScale = Vector3.one;
            entry.SetupStoreEntry(item);
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
            entry.onBuyDone.AddListener(UpdateNavigator);
            items.Add(entry);
            
        }
    }

    public void IventorySetup()
    {
        items = new List<ItemEntry>();
        foreach (var item in Consistency.Instance.unlockedClothes.list)
        {
            var entry = Instantiate(itemEntryPrefab);
            entry.transform.SetParent(gridTransform);
            entry.transform.localScale = Vector3.one;
            entry.SetupIventoryEntry(item);
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
            items.Add(entry);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(popupType == Popups.Inventory)
            UpdateEquippeds();
    }

    public void OnDisable()
    {
        foreach (Transform item in gridTransform)
        {
            Destroy(item.gameObject);
        }
    }

    public void UpdateEquippeds()
    {
        foreach (var item in items)
        {
            item.IsEquiped();
        }
    }

    private void UpdateNavigator()
    {
        foreach (var item in items)
        {
            if (item.ObjectButton.interactable)
            {
                EventSystem.current.SetSelectedGameObject(item.ObjectButton.gameObject);
                return;
            }
        }
        EventSystem.current.SetSelectedGameObject(returnButton.gameObject);
    }
}
