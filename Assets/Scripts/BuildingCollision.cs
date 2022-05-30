using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollision : MonoBehaviour
{
    public bool canBeDropped = true;

    //===============================================================
    // MONOBEHAVIOUR
    //===============================================================

    private void OnTriggerEnter(Collider other)
    {
        Building b = other.GetComponentInParent<Building>();
        if (!b || b == GetComponentInParent<Building>()) return;
        canBeDropped = false;
    }

    private void OnTriggerExit(Collider other)
    {
        Building b = other.GetComponentInParent<Building>();
        if (!b || b == GetComponentInParent<Building>()) return;
        canBeDropped = true;
    }

    private void OnDrawGizmos()
    {
        BoxCollider b = GetComponent<BoxCollider>();
        if(IsGrounded())
            Gizmos.color = Color.blue;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f), .1f);
        Gizmos.DrawWireSphere(transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f), .1f);
        Gizmos.DrawWireSphere(transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f), .1f);
        Gizmos.DrawWireSphere(transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f), .1f);
    }

    //===============================================================
    // PUBLIC METHODS
    //===============================================================

    public bool IsGrounded()
    {
        BoxCollider b = GetComponent<BoxCollider>();

        Vector3 pointA = transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f);
        Vector3 pointB = transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f);
        Vector3 pointC = transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f);
        Vector3 pointD = transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f);

        if (Cast(pointA) && Cast(pointB) && Cast(pointC) && Cast(pointD))
            return true;

        return false;
    }

    //===============================================================
    // PRIVATE METHODS
    //===============================================================

    private bool Cast(Vector3 point)
    {
        Collider[] colliders = Physics.OverlapSphere(point, .1f);

        foreach (Collider c in colliders)
        {
            if (c.transform.CompareTag("Ground"))
                return true;
        }

        return false;
    }
}
