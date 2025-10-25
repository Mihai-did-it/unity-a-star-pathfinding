using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SteeringBehavior : MonoBehaviour
{
    public Vector3 target;
    public KinematicBehavior kinematic;
    public List<Vector3> path;
    public float arrivalRadius = 6f;
    public float stopRadius = 1f;
    public TextMeshProUGUI label;

    void Start()
    {
        kinematic = GetComponent<KinematicBehavior>();
        target = transform.position;
        path = null;
        EventBus.OnSetMap += SetMap;
    }

    void Update()
    {
        if (path != null && path.Count > 0)
        {
            Seek(path[0]);
            float d = Vector3.Distance(transform.position, path[0]);
            if (d < stopRadius)
            {
                path.RemoveAt(0);
            }
        }
        else
        {
            Seek(target);
            float d = Vector3.Distance(transform.position, target);
            if (d < stopRadius)
            {
                kinematic.SetDesiredSpeed(0f);
                kinematic.SetDesiredRotationalVelocity(0f);
                if (label != null)
                {
                    label.text = "ARRIVED";
                }
            }
        }
    }

    void Seek(Vector3 goal)
    {
        Vector3 dir = goal - transform.position;
        float dist = Vector3.Distance(transform.position, goal);
        float angle = Vector3.SignedAngle(transform.forward, dir, Vector3.up);

        float speed = kinematic.GetMaxSpeed();
        if (dist < arrivalRadius)
        {
            speed = speed * dist / arrivalRadius;
        }
        kinematic.SetDesiredSpeed(speed);

        float rot = angle;
        float maxR = kinematic.GetMaxRotationalVelocity();
        if (rot > maxR) rot = maxR;
        if (rot < -maxR) rot = -maxR;
        kinematic.SetDesiredRotationalVelocity(rot);

        if (label != null)
        {
            label.text = dist.ToString("F1");
        }
    }

    public void SetTarget(Vector3 t)
    {
        target = t;
        EventBus.ShowTarget(t);
    }

    public void SetPath(List<Vector3> p)
    {
        path = p;
    }

    public void SetMap(List<Wall> o)
    {
        path = null;
        target = transform.position;
    }
}
