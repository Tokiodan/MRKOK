using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOKHelper : MonoBehaviour
{
    static public float FloorPos(GameObject Entity)
    {
        Ray floorCheck = new Ray(Entity.transform.position, Vector3.down);
        RaycastHit hitData;

        if (Physics.Raycast(floorCheck, out hitData, 100f, LayerMask.NameToLayer("Default")))
        {
            return hitData.normal.y;
        }
        return 0f;
    }
}
