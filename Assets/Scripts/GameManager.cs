using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int bedugCount;
    public int kentonganCount;
    public int rebanaCount;

    public TextMeshProUGUI bedugText;
    public TextMeshProUGUI kentonganText;
    public TextMeshProUGUI rebanaText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(string itemName, int value)
    {
        switch (itemName)
        {
            case "Bedug":
                bedugCount += value;
                bedugText.text = bedugCount + "/5";
                break;

            case "Kentongan":
                kentonganCount += value;
                kentonganText.text = kentonganCount + "/5";
                break;

            case "Rebana":
                rebanaCount += value;
                rebanaText.text = rebanaCount + "/5";
                break;

            default:
                Debug.LogWarning("Unknown item collected: " + itemName);
                break;
        }
    }
}
