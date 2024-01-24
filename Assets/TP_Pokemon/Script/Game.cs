using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    int defaultSeed;

    [SerializeField]
    List<Human> Players = new List<Human>(3);
    [SerializeField]
    List<Pokemon> possiblePokemons = new List<Pokemon>(3);

    [SerializeField]
    Human opponent1;
    [SerializeField]
    Human opponent2;

    bool isGameStarted = false;
    bool isBattleStarted = false;

    //Fight Variables
    int turnIndex = 0;
    Pokemon oponent1CurrentPokemon;
    Pokemon oponent2CurrentPokemon;

    private void Start()
    {
        possiblePokemons.Add(gameObject.AddComponent<Salameche>());
        possiblePokemons.Add(gameObject.AddComponent<Bulbizarre>());
        possiblePokemons.Add(gameObject.AddComponent<Carapuce>());

        Players.Add(gameObject.AddComponent<Sasha>());
        Players.Add(gameObject.AddComponent<Ondine>());
        Players.Add(gameObject.AddComponent<Pierre>());
    }

    /// <summary>
    /// Init the game with a seed.
    /// Set it to 0 to use the default seed wich is random.
    /// </summary>
    /// <param name="_seed"></param>
    public void InitGame(int _seed = 0)
    {
        if (isGameStarted) return;

        isGameStarted = true;
        defaultSeed = System.DateTime.Now.Millisecond;

        //Setting up the seed
        if (_seed == 0) { _seed = defaultSeed; }
        Random.InitState(_seed);
        Debug.Log($"La partie commence sur le terrain {_seed}");

        //Setting up the player's pokemons
        for (int i = 0; i < Players.Count; i++)
        {
            Human _currentPlayer = Players[i];
            switch (_currentPlayer.characterType)
            {
                //If the player is a healer, he doesn't have pokemons
                case "Healer":
                    Debug.Log($"{_currentPlayer.characterName} est un soigneur et ne possède pas de pokémons");
                    _currentPlayer.gameScript = this;
                    break;
                default:
                    _currentPlayer.CatchPokemon(possiblePokemons[Random.Range(0, possiblePokemons.Count)]);
                    _currentPlayer.CatchPokemon(possiblePokemons[Random.Range(0, possiblePokemons.Count)]);
                    Debug.Log($"{_currentPlayer.characterName} possède {_currentPlayer.pokemons.Count} pokémons, un {_currentPlayer.pokemons[0].PokemonName} et un {_currentPlayer.pokemons[1].PokemonName}");
                    break;
            }
        }
    }
    /// <summary>
    /// Choose a random opponent who don't have the healer type.
    /// </summary>
    public void SetOpponents1()
    {
        if (!isGameStarted) { return; }
        Human _temporaryOpponent = Players[Random.Range(0, Players.Count)];
        //If the opponent is a healer or if it is the same as the second opponent, we choose another one
        if (_temporaryOpponent.characterType == "Healer" || _temporaryOpponent == opponent2)
        {
            SetOpponents1();
        }
        else
        {
            opponent1 = _temporaryOpponent;
            Debug.Log($"L'adversaire 1 est {opponent1.characterName}");
        }
    }

    /// <summary>
    /// Choose a random opponent who don't have the healer type.
    /// </summary>
    public void SetOpponents2()
    {
        if (!isGameStarted) { return; }
        Human _temporaryOpponent = Players[Random.Range(0, Players.Count)];
        //If the opponent is a healer or if it is the same as the second opponent, we choose another one
        if (_temporaryOpponent.characterType == "Healer" || _temporaryOpponent == opponent1)
        {
            SetOpponents2();
        }
        else
        {
            opponent2 = _temporaryOpponent;
            Debug.Log($"L'adversaire 2 est {opponent2.characterName}");
        }
    }

    /// <summary>
    /// Start a fight between two humans defined earlier with the 2 functions SetOpponentX.
    /// </summary>
    /// <param name="_opponent1"></param>
    /// <param name="_opponent2"></param>
    public void StartBattle()
    {
        if (!isGameStarted || isBattleStarted)
        {
            Debug.Log("La partie n'a pas commencé ou un combat est déjà en cours");
            return;
        }
        if (opponent1 == null || opponent2 == null)
        {
            Debug.Log("Il faut deux adversaires pour commencer un combat");
            return;
        }

        isBattleStarted = true;
        Debug.Log($"Début du combat entre {opponent1.characterName} et {opponent2.characterName}");

        //Set the active pokemon for each opponent
        oponent1CurrentPokemon = opponent1.pokemons[0];
        oponent1CurrentPokemon.GetOutPokeball();
        oponent2CurrentPokemon = opponent2.pokemons[0];
        oponent2CurrentPokemon.GetOutPokeball();

        Debug.Log($"{opponent1.characterName} envoie son {opponent1.pokemons[0].PokemonName}");
        Debug.Log($"{opponent2.characterName} envoie son {opponent2.pokemons[0].PokemonName}");
        turnIndex += 1;
    }

    /// <summary>
    /// Make the current pokemon of the opponent that have to play attack the current pokemon of the other opponent.
    /// </summary>
    public void PlayerAttackTackle()
    {
        if (!isBattleStarted) { return; }
        if (turnIndex % 2 == 0)
        {
            Debug.Log($"{opponent1.characterName} attaque avec {oponent1CurrentPokemon}");
            oponent1CurrentPokemon.Takle(oponent2CurrentPokemon);
            turnIndex += 1;
        }
        else
        {
            Debug.Log($"{opponent2.characterName} attaque avec {oponent2CurrentPokemon}");
            oponent2CurrentPokemon.Takle(oponent1CurrentPokemon);
            turnIndex += 1;
        }
    }

    /// <summary>
    /// Make the current pokemon of the opponent that have to play attack the current pokemon of the other opponent.
    /// </summary>
    public void PlayerAbilityuAttack()
    {
        //TODO : Fix if the active pokemon is bulbizarre
        if (!isBattleStarted) { return; }
        if (turnIndex % 2 == 0)
        {
            Debug.Log($"{opponent1.characterName} attaque avec {oponent1CurrentPokemon}");
            oponent1CurrentPokemon.Ability(oponent2CurrentPokemon);
            turnIndex += 1;
        }
        else
        {
            Debug.Log($"{opponent2.characterName} attaque avec {oponent2CurrentPokemon}");
            oponent2CurrentPokemon.Ability(oponent1CurrentPokemon);
            turnIndex += 1;
        }
    }
}
