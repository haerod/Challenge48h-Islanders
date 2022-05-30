using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        Building contact = other.GetComponentInParent<Building>();

        if (!contact) return;

        Building parent = GetComponentInParent<Building>();

        if (contact == parent) return;

        parent.CheckBuilding(contact, true);
    }

    private void OnTriggerExit(Collider other)
    {
        Building contact = other.GetComponentInParent<Building>();

        if (!contact) return;

        Building parent = GetComponentInParent<Building>();

        parent.CheckBuilding(contact, false);
    }

    public List<Building> GetAllBuildings()
    {
        List<Building> toReturn = new List<Building>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x);
        Building parent = GetComponentInParent<Building>();

        foreach (Collider c in colliders)
        {
            Building b = c.transform.GetComponentInParent<Building>();

            if (!b) continue;
            if (b == parent) continue;

            toReturn.Add(b);
        }

        return toReturn;
    }
}
