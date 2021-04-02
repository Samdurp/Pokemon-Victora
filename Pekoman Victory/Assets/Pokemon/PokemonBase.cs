using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase : ScriptableObject
{
    //Name
    [SerializeField] string name;

    // Gender of the Pokemon
    [SerializeField] PokemonGender gender;

    [TextArea]
    [SerializeField] string description;

    // Pokedex Number
    [SerializeField] int dexNo;

    // Sprites
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    // Types 
    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    // Stats
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int specialAttack;
    [SerializeField] int defense;
    [SerializeField] int specialDefense;
    [SerializeField] int speed;

    // CatchRate
    [SerializeField] int catchRate;

    // This does not reflect my views; there are more than 2 genders irl. But not for Pokemon!
    public enum PokemonGender
    {
        Male, Female
    }

    public enum PokemonType
    {
        None, 
        Normal,
        Fire,
        Water,
        Electric,
        Grass,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dark,
        Dragon,
        Steel,
        Fairy
    }
}
