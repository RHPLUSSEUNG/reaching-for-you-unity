public enum ElementType
{
    Water = 0,
    Fire = 1,
    Grass = 2,
    Ground = 3,
    Electric = 4,
    None = 5,
};

public enum skillType
{
    Passive = 0,
    Active = 1,
    Travel = 2,
};

public enum ItemType
{
    Equipment = 0,
    Consume = 1,
};

public enum EquipPart
{
    Head = 0,
    UpBody = 1,
    DownBody = 2,
    Weapon = 3,
};

public enum TargetObject
{
    Enemy = 0,
    Friendly = 1,
    Tile = 2,
    Me = 3,
}

public enum BattleState
{
    Start = 0,
    PlayerTurn = 1,
    EnemyTurn = 2,
    Victory = 3,
    Defeat = 4,
    Wait = 5,
};

public enum ButtonState
{
    Idle = 0,
    Skill = 1,
    Item = 2,
    PlayerSet = 3,
    Attack =4,
}

public enum Buttons
{
    button1,
    button2,
    button3,
    button4,
}

public enum AnimState
{
    Idle,
    Move,
    Attack,
    Hit,
    Roll
}
public enum Direction
{
    Left,
    Right
}