using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Text coinsText;

    private int coins;

    private void Start()
    {
        this.coins = (PlayerPrefs.HasKey("coins")) ? PlayerPrefs.GetInt("coins") : 0;
        this.coinsText.text = coins.ToString();
    }

    public void ChangeCoins(bool isAdding)
    {
        if (isAdding)
        {
            this.coins += 50;
            SaveAndUpdate(this.coins);
        }
        else
        {
            this.coins -= 50;
            SaveAndUpdate(this.coins);
        }
    }

    private void SaveAndUpdate(int coins)
    {
        PlayerPrefs.SetInt("coins", coins);
        this.coinsText.text = coins.ToString();
    }

    public void NavigateTo(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
