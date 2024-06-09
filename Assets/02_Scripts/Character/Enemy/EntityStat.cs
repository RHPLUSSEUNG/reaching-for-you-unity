using UnityEngine;

public class EntityStat : MonoBehaviour
{
    [SerializeField]
    int _level;
    [SerializeField]
    int _maxHp;
    [SerializeField]
    int _hp;
    [SerializeField]
    int _maxMp;
    [SerializeField]
    int _mp;
    [SerializeField]
    int _baseDamage;
    [SerializeField]
    int _attackRange;
    [SerializeField]
    int _movePoint;
    [SerializeField]
    int _maxMovePoint;
    [SerializeField]
    int _actPoint;
    [SerializeField]
    int _sight;
    [SerializeField]
    float _defense;
    [SerializeField]
    float _healReciveMultiply;

    [SerializeField]
    bool _hidden;

    [SerializeField]
    float _burn;
    [SerializeField]
    float _oxygen;
    [SerializeField]
    bool _bind;
    [SerializeField]
    int _shock;

    public int Level { get { return _level; } set { _level = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxMp { get { return _maxMp; } set { _maxMp = value; } }
    public int Mp { get { return _mp; } set { _mp = value; } }
    public int BaseDamage { get { return _baseDamage; } set { _baseDamage = value; } }
    public int AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public int MovePoint { get { return _movePoint; } set { _movePoint = value; } }
    public int MaxMovePoint { get { return _maxMovePoint; } set { _maxMovePoint = value; } }
    public int ActPoint { get { return _actPoint; } set { _actPoint = value; } }
    public int Sight { get { return _sight; } set { _sight = value; } }
    public float Defense { get { return _defense; } set { _defense = value; } }
    public float HealReciveMultiply { get { return _healReciveMultiply; } set { _healReciveMultiply = value; } }
    public bool Hidden { get { return _hidden; } set { _hidden = value; } }
    public float Burn { get { return _burn; } set { _burn = value; } }
    public float Oxygen { get { return _oxygen; } set { _oxygen = value; } }
    public bool Bind { get { return _bind; } set { _bind = value; } }
    public int Shock { get { return _shock; } set { _shock = value; } }

}
