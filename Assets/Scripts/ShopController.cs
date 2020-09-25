using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public GameObject[] planets;

    public GameObject selectedPlanet;

    [SerializeField]
    private Text value;

    private void Start()
    {
        if (PlayerPrefs.HasKey("selected"))
        {
            SelectPlanet(planets[PlayerPrefs.GetInt("selected")]);
        }
        else
        {
            SelectPlanet(planets[0]);
        }
    }

    public void OnClick(int index)
    {
        SelectPlanet(planets[index]);
    }

    private void SelectPlanet(GameObject obj)
    {
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

        value.text = GetChildGameObject(obj, 0).GetComponent<Text>().text;
    }

    private GameObject GetChildGameObject(GameObject obj, int child)
    {
        return obj.transform.GetChild(child).gameObject;
    }
}
