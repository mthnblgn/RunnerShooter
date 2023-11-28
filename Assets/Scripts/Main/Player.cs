using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static int _money;
    private string _saveKey = "Money";
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _FinishText;
    [SerializeField] private GameController _gameController;
    [SerializeField] private Button[] _buttons;
    public int Money {  get { return _money; } set { _money = value; } }
    private void Start()
    {
        _money = PlayerPrefs.GetInt(_saveKey, 0);
        _moneyText.text = _money.ToString();
        EnoughMoneyCheck();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {

        }
    }
    public void UpdateMoney(int amount)
    {
        _money += amount;
        _moneyText.text = _money.ToString();
        PlayerPrefs.SetInt(_saveKey, _money);
        EnoughMoneyCheck();
    }

    private void EnoughMoneyCheck()
    {
        if (_money < 1000)
        {
            for (int i = 0; i < 3; i++)
            {
                _buttons[i].interactable = false;
            }
        }
    }

    public static int GetMoney()
    {
        return _money;
    }
}
