using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject[] planets;

    private GameObject selectedPlanet;

    [SerializeField]
    private Text value;

    [SerializeField]
    private Text coinsText;

    private int coins;

    private void Start()
    {
        LoadProgress(planets);

        // load and select the saved game object 
        // or select the first planet if there is no saved value
        if (PlayerPrefs.HasKey("selected"))
        {
            SelectPlanet(planets[PlayerPrefs.GetInt("selected")]);
        }
        else
        {
            SelectPlanet(planets[0]);
        }

        // get the total coins
        coins = (PlayerPrefs.HasKey("coins")) ? PlayerPrefs.GetInt("coins") : 0;
    }

    public void OnClick(int index)
    {
        SelectPlanet(planets[index]);
    }

    public void Unlock()
    {
        int index = GetIndex(selectedPlanet);
        int balance = coins - int.Parse(value.text);


        if (coins >= int.Parse(value.text) && (GetChildGameObject(selectedPlanet, 3).activeSelf == true))
        {
            // withdraw the game object value from total coins and update text value
            PlayerPrefs.SetInt("coins", balance);
            coinsText.text = balance.ToString();

            GetChildGameObject(selectedPlanet, 3).SetActive(false);

            PlayerPrefs.SetInt("selected", index);
            PlayerPrefs.SetInt("status " + index, 1);
        }

    }

    private void SelectPlanet(GameObject obj)
    {
        int index = GetIndex(obj);

        if (selectedPlanet)
        {
            GetChildGameObject(selectedPlanet, 1).SetActive(false);
            selectedPlanet = obj;
        }
        else
        {
            selectedPlanet = obj;
        }
        GetChildGameObject(obj, 1).SetActive(true);

        //change text value with the planet game object value
        value.text = GetChildGameObject(obj, 0).GetComponent<Text>().text;

        if (GetChildGameObject(selectedPlanet, 3).activeSelf == false)
        {
            PlayerPrefs.SetInt("selected", index);
        }
    }

    // iterates trought the list and unlocks the game object if the status equals 1
    private void LoadProgress(GameObject[] list)
    {
        for (int i = 0; i <= list.Length; i++)
        {
            if (PlayerPrefs.GetInt("status " + i) == 1)
            {
                GetChildGameObject(list[i], 3).SetActive(false);
            }
        }
    }

    // return a child game object from a game object
    private GameObject GetChildGameObject(GameObject obj, int child)
    {
        return obj.transform.GetChild(child).gameObject;
    }

    private void OnScreenLeave()
    {
        int index = GetIndex(selectedPlanet);
        PlayerPrefs.SetInt("selected", index);
        PlayerPrefs.SetInt("status " + index, 1);
    }

    // find the game object index and save
    private int GetIndex(GameObject obj)
    {
        int index = 0;

        while (obj != planets[index])
        {
            index++;
        }

        return index;
    }
}