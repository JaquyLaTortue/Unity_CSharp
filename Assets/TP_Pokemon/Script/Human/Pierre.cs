using UnityEngine;

public class Pierre : Human, IHealer
{
    public void SetUpHealerValues()
    {
        CharacterType = "Healer";
    }

    public void HealAll()
    {
        int _healValue = 40;
        Debug.Log($"{CharacterName} va soigner tous les Pokemons encore actifs de {_healValue}PV");
        Human _opponent1 = GameScript.Opponent1;
        Human _opponent2 = GameScript.Opponent2;

        for (int i = 0; i < _opponent1.Pokemons.Count; i++)
        {
            _opponent1.Pokemons[i].Heal(_healValue);
        }

        for (int i = 0; i < _opponent2.Pokemons.Count; i++)
        {
            _opponent2.Pokemons[i].Heal(_healValue);
        }
    }

    public void HealOne(Pokemon _target)
    {
        Debug.Log($"{CharacterName} va soigner completement {_target.PokemonName}");
        _target.Heal(999999999);
    }

    private void Start()
    {
        CharacterName = "Pierre";
        SetUpHealerValues();
    }
}
