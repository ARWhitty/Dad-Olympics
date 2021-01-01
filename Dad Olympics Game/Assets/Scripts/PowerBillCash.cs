using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBillCash : MonoBehaviour
{
    public int BillTotal = 0;
    public int TimerDelayAmount = 1;

    public Text BillTotalText;

    protected float CashTimer;

    void Update()
    {
        CashTimer += Time.deltaTime;

        if (CashTimer >= TimerDelayAmount)
        {
            CashTimer = 0f;
            BillTotal++;
            BillTotalText.text = "Power Bill: $" + BillTotal;

        }
    }
}
