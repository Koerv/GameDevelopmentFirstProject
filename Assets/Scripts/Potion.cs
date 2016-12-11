using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour {

    public const int HP = 0;
    public const int STRENGTH = 1;
    public const int STEALTH = 2;
    public const int POISON = 3;

    public Sprite[] sprites;

    public int type;

    public void activate(Hero hero)
    {
        switch (type)
        {
            case HP:
                hero.hp += hero.level * 2;
                break;
            case STRENGTH:
                hero.strength += hero.level;
                break;
            case STEALTH:
                hero.isStealthed = true;
                break;
            case POISON:
                hero.isPoisoned = true;
                break;
            default:
                return;
        }
    }

    public string typeToString()
    {
        switch (type)
        {
            case HP:
                return "HP";
            case STRENGTH:
                return "Strength";
            case STEALTH:
                return "Stealth";
            case POISON:
                return "Poison";
            default:
                return "No type";
        }
    }
}
