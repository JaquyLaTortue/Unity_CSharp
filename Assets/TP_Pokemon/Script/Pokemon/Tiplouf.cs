using UnityEngine;

public class Tiplouf : Pokemon
{
    private void Start()
    {
        PokemonName = "Tiplouf";
        Pv = 110;
        MaxPv = 110;
        attack = 55;
        defense = 55;
        speed = 40;
        Type = "Water";
        typeAdvantage = "Fire";
        typeDisadvantage = "Grass";
    }

    public override void Ability(Pokemon _target)
    {
        Ecume(_target);
    }

    public void Ecume(Pokemon _target)
    {
        if (!IsInPokeball)
        {
            Debug.Log($"{PokemonName} utilise Ecume");
            float _baseDamage = attack * 1.4f;
            _target.TakeDamage(_baseDamage, "Water");
            return;
        }

        Debug.Log($"{PokemonName} ne peut rien faire, il est dans sa pokeball");
    }
}
