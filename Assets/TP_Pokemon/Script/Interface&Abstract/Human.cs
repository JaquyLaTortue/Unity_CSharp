using System.Collections.Generic;
using UnityEngine;

public abstract class Human : MonoBehaviour
{
    /// <summary>
    /// A Human cannot have more than 2 Pokemons
    /// </summary>
    public List<Pokemon> Pokemons { get; private set; } = new List<Pokemon>(2);

    public Game GameScript { get; set; }

    public string CharacterName { get; protected set; }

    public string CharacterType { get; protected set; }

    public int NumberofPokemonsKO = 0;

    public void CatchPokemon(Pokemon _pokemon)
    {
        Pokemons.Add(_pokemon);
    }
}
