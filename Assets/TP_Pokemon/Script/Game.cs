using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    int defaultSeed;

    int seed = 0;
    public string Seed { get => seed.ToString(); set => seed = int.Parse(value); }


    [SerializeField] List<Human> Players = new List<Human>(3);
    [SerializeField] List<Pokemon> possiblePokemons = new List<Pokemon>(3);

    [SerializeField] public Human opponent1 { get; private set; }
    [SerializeField] public Human opponent2 { get; private set; }

    bool isGameStarted = false;
    bool isBattleStarted = false;
    bool isGameEnded = false;

    //Fight Variables
    public int TurnIndex { get; private set; } = 1;

    public Pokemon oponent1CurrentPokemon { get; private set; }
    public Pokemon oponent2CurrentPokemon { get; private set; }

    public event Action OnTurnChange;

    //Add all the pokemons and players to the list, it is not automatic because we can add more pokemons and players later
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
    public void InitGame()
    {
        if (isGameStarted || isGameEnded) return;

        isGameStarted = true;
        defaultSeed = System.DateTime.Now.Millisecond;

        //Setting up the seed
        if (seed == 0) { seed = defaultSeed; }
        Random.InitState(seed);
        Debug.Log($"La partie commence sur le terrain {seed}");

        //Setting up the player's pokemons
        for (int i = 0; i < Players.Count; i++)
        {
            Human _currentPlayer = Players[i];
            switch (_currentPlayer.characterType)
            {
                //If the player is a healer, he doesn't have pokemons
                case "Healer":
                    Debug.Log($"{_currentPlayer.characterName} est un soigneur et ne possède pas de pokemons");
                    _currentPlayer.gameScript = this;
                    break;
                default:
                    Pokemon _pokemon1 = possiblePokemons[Random.Range(0, possiblePokemons.Count)];
                    switch (_pokemon1.PokemonName)
                    {
                        case "Bulbizarre":
                            _currentPlayer.CatchPokemon(gameObject.AddComponent<Bulbizarre>());
                            break;
                        case "Salameche":
                            _currentPlayer.CatchPokemon(gameObject.AddComponent<Salameche>());
                            break;
                        case "Carapuce":
                            _currentPlayer.CatchPokemon(gameObject.AddComponent<Carapuce>());
                            break;
                        default:
                            Debug.Log($"Erreur dans l'ajout des pokemons aux dresseurs");
                            return;
                    }

                    Pokemon _pokemon2 = possiblePokemons[Random.Range(0, possiblePokemons.Count)];
                    switch (_pokemon2.PokemonName)
                    {
                        case "Bulbizarre":
                            _currentPlayer.CatchPokemon(gameObject.AddComponent<Bulbizarre>());
                            break;
                        case "Salameche":
                            _currentPlayer.CatchPokemon(gameObject.AddComponent<Salameche>());
                            break;
                        case "Carapuce":
                            _currentPlayer.CatchPokemon(gameObject.AddComponent<Carapuce>());
                            break;
                        default:
                            Debug.Log($"Erreur dans l'ajout des pokemons aux dresseurs");
                            return;
                    }

                    Debug.Log($"{_currentPlayer.characterName} possède {_currentPlayer.pokemons.Count} pokemons, un {_pokemon1.PokemonName} et un {_pokemon2.PokemonName}");
                    break;
            }
        }
    }

    /// <summary>
    /// Choose a random opponent who don't have the healer type.
    /// </summary>
    public void SetOpponents1()
    {
        if (!isGameStarted || isGameEnded) { return; }
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
        if (!isGameStarted || isGameEnded) { return; }
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
        if (isGameEnded) { return; }

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

        if (isGameEnded) { return; }
        isBattleStarted = true;
        Debug.Log($"Début du combat entre {opponent1.characterName} et {opponent2.characterName}");

        //Set the active pokemon for each opponent
        oponent1CurrentPokemon = opponent1.pokemons[0];
        Debug.Log($"{opponent1.characterName} envoie son {oponent1CurrentPokemon.PokemonName}");
        oponent1CurrentPokemon.GetOutPokeball();

        oponent2CurrentPokemon = opponent2.pokemons[0];
        Debug.Log($"{opponent2.characterName} envoie son {oponent2CurrentPokemon.PokemonName}");
        oponent2CurrentPokemon.GetOutPokeball();

        EndTurn();
    }

    /// <summary>
    /// Make the current pokemon of the opponent that have to play attack the current pokemon of the other opponent.
    /// </summary>
    public void PlayerAttackTackle()
    {
        if (isGameEnded) { return; }

        //Checks if the battle has started
        if (!isBattleStarted)
        {
            Debug.Log("On ne peut pas attaquer");
            return;
        }

        Human _currentOpponent;
        Pokemon _currentOpponentPokemon;
        Human _targetMaster;
        Pokemon _target;

        //Set wich player will attack with wich pokemon
        switch (TurnIndex % 2)
        {
            case 0:
                _currentOpponent = opponent1;
                _currentOpponentPokemon = oponent1CurrentPokemon;
                _targetMaster = opponent2;
                _target = oponent2CurrentPokemon;
                break;
            case 1:
                _currentOpponent = opponent2;
                _currentOpponentPokemon = oponent2CurrentPokemon;
                _targetMaster = opponent1;
                _target = oponent1CurrentPokemon;
                break;
            default:
                return;
        }

        //Launch the attack if the pokemon is not in his pokeball
        if (!_currentOpponentPokemon.isInPokeball)
        {
            Debug.Log($"{_currentOpponent.characterName} attaque avec {_currentOpponentPokemon.PokemonName}");
            _currentOpponentPokemon.Takle(_target);
            CheckKOPokemon(_target, _targetMaster);

            if (_targetMaster.NumberofPokemonsKO >= 2) { return; }

            EndTurn();
            return;
        }

        Debug.Log($"{_currentOpponentPokemon.PokemonName} ne peut pas attaquer, il est dans sa pokeball ");
    }

    /// <summary>
    /// Make the current pokemon of the opponent that have to play attack the current pokemon of the other opponent.
    /// </summary>
    public void PlayerAbilityAttack()
    {
        if (isGameEnded) { return; }

        //Checks if the battle has started
        if (!isBattleStarted)
        {
            Debug.Log("On ne peut pas utiliser de capacité");
            return;
        }

        Human _currentOpponent;
        Pokemon _currentOpponentPokemon;
        Human _targetMaster;
        Pokemon _target;

        //Set wich player will attack with wich pokemon
        switch (TurnIndex % 2)
        {
            case 0:
                _currentOpponent = opponent1;
                _currentOpponentPokemon = oponent1CurrentPokemon;
                _targetMaster = opponent2;
                _target = oponent2CurrentPokemon;
                break;
            case 1:
                _currentOpponent = opponent2;
                _currentOpponentPokemon = oponent2CurrentPokemon;
                _targetMaster = opponent1;
                _target = oponent1CurrentPokemon;
                break;
            default:
                return;
        }

        //Launch the ability if the pokemon is not in his pokeball
        if (!_currentOpponentPokemon.isInPokeball)
        {
            Debug.Log($"{_currentOpponent.characterName} utilise la capacité de {_currentOpponentPokemon.PokemonName}");
            _currentOpponentPokemon.Ability(_target);
            CheckKOPokemon(_target, _targetMaster);

            if (_targetMaster.NumberofPokemonsKO >= 2) { return; }

            EndTurn();
            return;
        }

        Debug.Log($"{_currentOpponentPokemon.PokemonName} ne peut pas utiliser sa capacité, il est dans sa pokeball");
    }

    /// <summary>
    /// The player that have to play switch his current pokemon with his other pokemon.
    /// </summary>
    public void SwitchPokemon()
    {
        if (isGameEnded) { return; }

        //Checks if the battle has started or if it is the end of the game
        if (!isBattleStarted )
        {
            Debug.Log("On ne peut pas faire de changement de pokemon");
            return;
        }
        List<Pokemon> _temp = new List<Pokemon>(2);
        Human _currentOpponent;
        Pokemon _currentOpponentPokemon;
        Pokemon _newPokemon;

        int _playerTurn;

        switch (TurnIndex % 2)
        {
            case 0:
                _currentOpponent = opponent1;
                _currentOpponentPokemon = oponent1CurrentPokemon;
                _playerTurn = 0;
                break;
            case 1:
                _currentOpponent = opponent2;
                _currentOpponentPokemon = oponent2CurrentPokemon;
                _playerTurn = 1;
                break;
            default:
                return;
        }

        _temp.AddRange(_currentOpponent.pokemons);
        _temp.Remove(_currentOpponentPokemon);
        _newPokemon = _temp[0];


        //Checks if the other pokemon is dead
        if (_newPokemon.pv <= 0)
        {
            Debug.Log($"{_newPokemon.PokemonName} est KO, impossible de le changer");
            return;
        }

        Debug.Log($"{_currentOpponent.characterName} change de pokemon et rappelle son {_currentOpponentPokemon.PokemonName}");
        _currentOpponentPokemon.GetInPokeball();

        _currentOpponentPokemon = _newPokemon;

        Debug.Log($"{_currentOpponent.characterName} envoie son {_currentOpponentPokemon.PokemonName}");
        _currentOpponentPokemon.GetOutPokeball();

        switch (_playerTurn)
        {
            case 0:
                oponent1CurrentPokemon = _currentOpponentPokemon;
                break;
            case 1:
                oponent2CurrentPokemon = _currentOpponentPokemon;
                break;
            default:
                Debug.Log("Erreur dans le changement de pokemon");
                return;
        }

        EndTurn();
    }

    /// <summary>
    /// Checks if the pokemon is KO and if the player still have more pokemon.
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_targetMaster"></param>
    void CheckKOPokemon(Pokemon _target, Human _targetMaster)
    {
        if (isGameEnded) { return; }

        switch (_target.pv <= 0)
        {
            case true:
                _targetMaster.NumberofPokemonsKO += 1;
                break;
            default:
                return;
        }

        if (_targetMaster.NumberofPokemonsKO >= 2)
        {
            if (_targetMaster == opponent1)
            {
                EndBattle(opponent2);
            }
            else
            {
                EndBattle(opponent1);
            }
        }
    }

    /// <summary>
    /// End the turn, trigger the event OnTurnChange and increment the turn index.
    /// </summary>
    void EndTurn()
    {
        if (isGameEnded) { return; }

        TurnIndex += 1;
        OnTurnChange?.Invoke();
    }

    /// <summary>
    /// End the battle and reset the turn index.
    /// </summary>
    void EndBattle(Human _winner)
    {
        if (!isBattleStarted)
        {
            Debug.Log("Le combat n'a pas commencé, il ne peut pas y avoir de gagnant");
            return;
        }

        isGameEnded = true;
        Debug.Log($"Fin du combat entre {opponent1.characterName} et {opponent2.characterName}. \n Le gagnant est {_winner.characterName} \n Merci d'avoir joué a {this.name}");
    }
}
