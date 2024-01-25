using UnityEngine;

public class Salameche : Pokemon
{
    private void Start()
    {
        PokemonName = "Salameche";
        Pv = 100;
        MaxPv = 100;
        attack = 60;
        defense = 40;
        speed = 60;
        Type = "Fire";
        typeAdvantage = "Grass";
        typeDisadvantage = "Water";
    }

    public override void Ability(Pokemon _target)
    {
        Flameche(_target);
    }

    public void Flameche(Pokemon _target)
    {
        if (!IsInPokeball)
        {
            Debug.Log($"{PokemonName} utilise Flameche");
            float _baseDamage = attack * 1f;
            _target.TakeDamage(_baseDamage, "Fire");
            return;
        }

        Debug.Log($"{PokemonName} ne peut rien faire, il est dans sa pokeball");
    }
}
