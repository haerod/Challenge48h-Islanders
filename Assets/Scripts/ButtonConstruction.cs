using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonConstruction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private int listIndex;

    public void AddNewBuildingIndex(int index)
    {
        listIndex = index;
        GetComponent<Image>().sprite = Manager.instance.levelBuildings[index].imageBouton;
    }

    public void ClickOnButton()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(true);

        Text txt = transform.GetComponentInChildren<Text>();
        Building b = Manager.instance.levelBuildings[listIndex];
        txt.text = "";

        // Name
        txt.text += "<b>" + b.nom + "</b>";

        // Description
        txt.text += "\n" + "\n" + "<i>" + b.description + "</i>" + "\n";

        // Bonus
        bool bonus = false;
        foreach (Points p in b.points)
        {
            if(p.bonus != 0)
            {
                bonus = true;
                break;
            }
        }

        if(bonus)
        {
            txt.text += "\nBonus : ";

            foreach (Points p in b.points)
            {
                if (p.bonus != 0)
                {
                    txt.text += p.nom + "(" + p.bonus + "), ";
                }
            }

            txt.text += "\n";
        }

        // Malus
        bool malus = false;
        foreach (Points p in b.points)
        {
            if(p.malus != 0)
            {
                malus = true;
                break;
            }
        }

        if (malus)
        {
            txt.text += "\nMalus : ";

            foreach (Points p in b.points)
            {
                if (p.malus != 0)
                {
                    txt.text += p.nom + "(" + p.malus + "), ";
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }


}
