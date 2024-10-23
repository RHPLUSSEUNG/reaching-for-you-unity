using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Character")]
public class CharacterData : ScriptableObject
{
    public int characterId;
    public string characterName;
    public characterType characterType;
}