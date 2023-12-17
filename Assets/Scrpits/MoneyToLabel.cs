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
        playerMoneyLabel.text = Consistency.Instance.PlayerMoney.ToString();
    }
}
