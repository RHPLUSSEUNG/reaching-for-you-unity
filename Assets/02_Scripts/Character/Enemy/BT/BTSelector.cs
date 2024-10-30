public class BTSelector : BTNode
{
    public override BTNodeState Evaluate()
    {
        foreach (BTNode node in children)
        {
            BTNodeState result = node.Evaluate();

            if (result == BTNodeState.Success)
            {
                return BTNodeState.Success;
            }
            else if (result == BTNodeState.Running)
            {
                return BTNodeState.Running;
            }
        }
        return BTNodeState.Failure;
    }
}
