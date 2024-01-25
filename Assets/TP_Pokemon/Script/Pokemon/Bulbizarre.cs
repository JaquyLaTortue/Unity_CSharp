using UnityEngine;

public class Bulbizarre : Pokemon
{
    private void Start()
    {
        PokemonName = "Bulbizarre";
        Pv = 120;
        MaxPv = 120;
        attack = 40;
        defense = 60;
        speed = 30;
        Type = "Grass";
        typeAdvantage = "Water";
        typeDisadvantage = "Fire";
    }

    public override void Ability(Pokemon _target)
    {
        Soin();
    }

    public void Soin()
    {
        if (!IsInPokeball)
        {
            Debug.Log($"{PokemonName} utilise Soin");
            Heal(30);
            return;
        }

        Debug.Log($"{PokemonName} ne peut rien faire, il est dans sa pokeball");
    }

    public void FouetLianes(Pokemon _target)
    {
        if (!IsInPokeball)
        {
            float _baseDamage = attack * 0.8f;
            _target.TakeDamage(_baseDamage, "Grass");
        }

        Debug.Log($"{PokemonName} ne peut rien faire, il est dans sa pokeball");
    }
}
