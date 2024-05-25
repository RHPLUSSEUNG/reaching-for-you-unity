using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField]
    private int _stun = 0;
    [SerializeField]
    private int _shackle = 0;
    [SerializeField]
    private int _silence = 0;
    [SerializeField]
    private int _slow = 0;

    public bool IsStun()
    {
        if(_stun > 0)
        {
            return true;
        }
        return false;
    }
    public bool IsShackle ()
    {
        if (_shackle > 0)
        {
            return true;
        }
        return false;
    }
    public bool IsSilence()
    {
        if (_silence > 0)
        {
            return true;
        }
        return false;
    }
    public int GetSlow()
    {
        return _slow;
    }

    public void ChangeStun(bool stun)
    {
        if(stun == true)
        {
            _stun++;
        }
        else
        {
            _stun--;
        }
    }
    public void ChangeShackle(bool shackle)
    {
        if (shackle == true)
        {
            _shackle++;
        }
        else
        {
            _shackle--;
        }
    }
    public void ChangeSilence(bool silence)
    {
        if (silence == true)
        {
            _silence++;
        }
        else
        {
            _silence--;
        }
    }
    public void ChangeSlow(bool state, int slow)
    {
        if (state == true)
        {
            _slow += slow;
            this.gameObject.GetComponent<EntityStat>().MovePoint -= slow;
        }
        else
        {
            _slow -= slow;
            this.gameObject.GetComponent<EntityStat>().MovePoint += slow;
        }
    }
}
