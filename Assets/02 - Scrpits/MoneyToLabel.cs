using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyToLabel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerMoneyLabel;
    // Start is called before the first frame update
    void Start()
    {
        playerMoneyLabel.text = Consistency.Instance.playerMoney.ToString();
        Consistency.Instance.onMoneyChanged.AddListener(ChangeLabel);
    }
    void ChangeLabel()
    {
        playerMoneyLabel.text = Consistency.Instance.playerMoney.ToString();
    }
    private void OnDestroy()
    {
        Consistency.Instance.onMoneyChanged.RemoveAllListeners();
    }
}
