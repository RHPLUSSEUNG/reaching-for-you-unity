using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ElectricShockConfirmed : Active
{
    public override bool Activate()
    {
        ElectricShock shock = new();

        #region 임시 수정 코드
        //shock.SetDebuff(2, target, 1);
        Managers.Active.Damage(target,50);
        #endregion 

        return true;
    }
}