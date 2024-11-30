public class BTCondition : BTNode
{
    private System.Func<bool> condition;

    public BTCondition(System.Func<bool> condition)
    {
        this.condition = condition;
    }

    public override BTNodeState Evaluate()
    {
        return condition() ? BTNodeState.Success : BTNodeState.Failure;
    }
}
