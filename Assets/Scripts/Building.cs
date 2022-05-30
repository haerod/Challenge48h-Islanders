using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string nom = "Ecrivez nom du bâtiment ici";
    public string description = "Description du bâtiment ici (courte)";
    public Sprite imageBouton;
    public List<Points> points;

    private List<MeshRenderer> rends = new List<MeshRenderer>();
    private Vector3 pointerWorldPoz;
    private Transform ground;
    private Plane underPlane;
    private SphereCollision sphere;
    private int currentPoints;
    private BuildingText pointsText;
    private BuildingCollision col;
    public bool canDrag = true;

    //===============================================================
    // MONOBEHAVIOUR
    //===============================================================

    private void Start()
    {
        rends = gameObject.GetComponentsInChildren<MeshRenderer>().ToList();
        sphere = GetComponentInChildren<SphereCollision>();
        pointsText = GetComponentInChildren<BuildingText>();
        col = GetComponentInChildren<BuildingCollision>();
    }

    private void Update()
    {
        if (!canDrag) return;
        if (!CanBeDropped() || !col.canBeDropped || !col.IsGrounded())
        {
            SetBuildingActive(false);
        }
        else
        {
            SetBuildingActive(true);
            MouseClick();
        }

        Rotate();
        Drag();
    }

    //===============================================================
    // PUBLIC METHODS
    //===============================================================

    public void CheckBuilding(Building contact, bool isEntering)
    {
        foreach (Points p in points)
        {
            if (p.nom == contact.nom)
            {
                if(isEntering)
                {
                    currentPoints += p.bonus;
                    currentPoints += p.malus;
                    pointsText.SetMainTextActive(true, currentPoints);
                    contact.pointsText.SetMiniTextActive(true, p.bonus + p.malus);
                }
                else
                {
                    currentPoints -= p.bonus;
                    currentPoints -= p.malus;
                    pointsText.SetMainTextActive(true, currentPoints);
                    contact.pointsText.SetMiniTextActive(false);
                }
            }
            else
                continue;
        }
    }

    //===============================================================
    // PRIVATE METHODS
    //===============================================================

    private void MouseClick()
    {
        if (!Input.GetMouseButton(0)) return;
        canDrag = false;
        sphere.gameObject.SetActive(false);
        pointsText.SetMainTextActive(false);
        foreach (Building b in sphere.GetAllBuildings())
        {
            b.pointsText.SetMiniTextActive(false);
        }
        Manager.instance.AddPoints(currentPoints);
        Manager.instance.NewBuilding(Manager.instance.currentIndex);
        Manager.instance.SetButtonsActive(true);
        Manager.instance.buildingSound.Stop();
        Manager.instance.buildingSound.Play();
        Animator anim = GetComponent<Animator>();
        if(anim)
            anim.SetTrigger("bounce");
    }

    private void Drag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Camera.main.transform.forward, pointerWorldPoz);
        if (plane.Raycast(ray, out float enter))
        {
            transform.position = ray.GetPoint(enter);
        }
    }

    private bool CanBeDropped()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Ground
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                pointerWorldPoz = hit.point;
                ground = hit.transform;
                return true;
            }
        }

        return false;
    }

    private void Rotate()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            transform.Rotate(new Vector3(0, -90, 0));
        }
    }

    private void SetBuildingActive(bool value)
    {
        rends.ForEach(o => o.enabled = value);
        pointsText.SetMainTextActive(value, currentPoints);

        if (value)
        {
            if(Manager.instance.allowedCursor)
                Cursor.SetCursor(Manager.instance.allowedCursor, new Vector2(16, 16), CursorMode.Auto);
            else
                Cursor.SetCursor(null, new Vector2(16, 16), CursorMode.Auto);
        }
        else
            Cursor.SetCursor(Manager.instance.forbiddenCursor, new Vector2(16,16), CursorMode.Auto);
    }
}

[System.Serializable]
public class Points
{
    public string nom = "Nouveau bâtiment";
    public int bonus;
    public int malus;
}
