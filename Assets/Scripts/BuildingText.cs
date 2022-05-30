using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingText : MonoBehaviour
{
    public Text pointsText;

    private Transform buildingParent;

    private void Awake()
    {
        pointsText.gameObject.SetActive(false);
    }

    private void Update()
    {
        pointsText.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + (Vector3.up * Manager.instance.textHeightOffset));        
    }

    public void SetMiniTextActive(bool value, int modifier = 0)
    {
        pointsText.gameObject.SetActive(value);

        if (!value) return;

        pointsText.fontSize = Manager.instance.miniTextSize;
        pointsText.color = Manager.instance.miniTextColor;
        pointsText.text = modifier.ToString();
    }

    public void SetMainTextActive(bool value, int modifier = 0)
    {
        pointsText.gameObject.SetActive(value);

        if (!value) return;

        string toPrint = "";

        if (modifier >= 0)
            toPrint = "+" + modifier;
        else
            toPrint = modifier.ToString();

        pointsText.fontSize = Manager.instance.mainTextSize;
        pointsText.color = Manager.instance.mainTextColor;

        pointsText.text = toPrint;
    }

}
