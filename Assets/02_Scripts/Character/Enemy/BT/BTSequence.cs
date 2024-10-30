public class BTSequence : BTNode
{
    public override BTNodeState Evaluate()
    {
        foreach (BTNode node in children)
        {
            BTNodeState result = node.Evaluate();

            if (result == BTNodeState.Failure)
            {
                return BTNodeState.Failure;
            }
            else if (result == BTNodeState.Running) 
            {
                return BTNodeState.Running;
            }
        }
        return BTNodeState.Success;
    }
}
