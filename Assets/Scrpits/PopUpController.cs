using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    [SerializeField] ItemEntry itemEntryPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Button returnButton;

    public void StoreSetup()
    {
        foreach (var item in Consistency.Instance.lockedClothes)
        {
            var entry = Instantiate(itemEntryPrefab);
            entry.transform.SetParent(gridTransform);
            entry.transform.localScale = Vector3.one;
            entry.SetupStoreEntry(item);
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
        }
    }

    public void IventorySetup()
    {
        foreach (var item in Consistency.Instance.unlockedClothes)
        {
            var entry = Instantiate(itemEntryPrefab);
            entry.transform.SetParent(gridTransform);
            entry.transform.localScale = Vector3.one;
            entry.SetupIventoryEntry(item);
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDisable()
    {
        foreach (Transform item in gridTransform)
        {
            Destroy(item.gameObject);
        }
    }
}
