public class BTAction : BTNode
{
    private System.Func<BTNodeState> action;

    public BTAction(System.Func<BTNodeState> action)
    {
        this.action = action;
    }

    public override BTNodeState Evaluate()
    {
        return action();
    }

}
