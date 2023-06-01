using UnityEngine;
using BehaviourTree;

public class GoToDestination : Node
{
    private Transform transform;
    private float speed;

    public GoToDestination(Transform transform, float speed)
    {
        this.transform = transform;
        this.speed = speed;
    }

    public override NodeStatus Process()
    {
        Vector3 destination = (Vector3)GetData("destination");

        if (destination == null)
        {
            return NodeStatus.Failure;
        }

        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            return NodeStatus.Success;
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        return NodeStatus.Running;
    }
}
