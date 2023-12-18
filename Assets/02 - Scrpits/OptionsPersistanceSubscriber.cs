using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPersistanceSubscriber : MonoBehaviour
{
    [SerializeField] Button eraseButton;
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (eraseButton != null)
            eraseButton.onClick.AddListener(Consistency.Instance.EraseData);
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(Consistency.Instance.ChangeVolume);
            volumeSlider.SetValueWithoutNotify(Consistency.Instance.audioSource.volume);
        }
    }

    private void OnEnable()
    {
        if (volumeSlider != null)
        {
            volumeSlider.SetValueWithoutNotify(Consistency.Instance.audioSource.volume);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
