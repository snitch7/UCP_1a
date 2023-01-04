using UnityEngine;

[CreateAssetMenu(fileName = "Create Actor",menuName = "Create Actor")]
public class SOActorModel : ScriptableObject {
    public string ActorName;
    public AttackTypes AttackType;
    public int Score;

    public enum AttackTypes {
        Wave, Player, Flee, Bullet
    }

    public string Description;
    public int Health;
    public int Speed;
    public int HitPower;
    public GameObject Actor;
    public GameObject ActorsBullets;
}