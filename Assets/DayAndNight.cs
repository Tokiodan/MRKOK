
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    Vector3 rot=Vector3.zero;
    float degpersec=3;
    void Update()
    {
        rot.x=degpersec*Time.deltaTime;
        transform.Rotate(rot,Space.World);
    }
}
