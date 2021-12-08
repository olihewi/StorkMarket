using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager INSTANCE;
    public float money;
    public float moneyLossPerSecond;
    public TextMeshProUGUI moneyText;
    
    void Start()
    {
        INSTANCE = this;
    }

    void Update()
    {
        money -= moneyLossPerSecond * Time.deltaTime;
        moneyText.text = money.ToString("C") + "\nTime until bankruptcy: " + (money / moneyLossPerSecond).ToString("N")+"s";
        if (money < 0) SceneManager.LoadScene(1);
    }
}
