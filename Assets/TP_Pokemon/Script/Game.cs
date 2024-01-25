using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    private int defaultSeed;

    private int seed = 0;

    public string Seed { get => seed.ToString(); set => seed = int.Parse(value); }

    public List<Human> players = new List<Human>();
    public List<Pokemon> possiblePokemons = new List<Pokemon>();

    [SerializeField] public Human Opponent1 { get; private set; }

    [SerializeField] public Human Opponent2 { get; private set; }

    IHealer currentHealer;

    bool isGameStarted = false;
    bool isBattleStarted = false;
    bool isGameEnded = false;

    // Fight Variables
    public int TurnIndex { get; private set; } = 1;

    public Pokemon Oponent1CurrentPokemon { get; private set; }

    public Pokemon Oponent2CurrentPokemon { get; private set; }

    public event Action OnTurnChange;

    // Add all the Pokemons and players to the list, it is not automatic because we can add more Pokemons and players later
    private void Start()
    {
        possiblePokemons.Add(gameObject.AddComponent<Salameche>());
        possiblePokemons.Add(gameObject.AddComponent<Bulbizarre>());
        possiblePokemons.Add(gameObject.AddComponent<Carapuce>());

        players.Add(gameObject.AddComponent<Sasha>());
        players.Add(gameObject.AddComponent<Ondine>());
        players.Add(gameObject.AddComponent<Pierre>());
    }

    /// <summary>
    /// Init the game with a seed.
    /// Set it to 0 to use the default seed wich is random.
    /// </summary>
    /// <param name="_seed"></param>
    public void InitGame()
    {
        if (isGameStarted || isGameEnded)
        {
            return;
        }

        isGameStarted = true;
        defaultSeed = System.DateTime.Now.Millisecond;

        // Setting up the seed
        if (seed == 0)
        {
            seed = defaultSeed;
        }
        Random.InitState(seed);
        Debug.Log($"La partie commence sur le terrain {seed}");

        // Setting up the player's Pokemons
        for (int i = 0; i < players.Count; i++)
        {
            Human _currentPlayer = players[i];
            switch (_currentPlayer.CharacterType)
            {
                // If the player is a healer, he doesn't have Pokemons
                case "Healer":
                    Debug.Log($"{_currentPlayer.CharacterName} est un soigneur et ne possède pas de Pokemons");
                    currentHealer = (IHealer)_currentPlayer;
                    _currentPlayer.GameScript = this;
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
                            Debug.Log($"Erreur dans l'ajout des Pokemons aux dresseurs");
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
                            Debug.Log($"Erreur dans l'ajout des Pokemons aux dresseurs");
                            return;
                    }

                    Debug.Log($"{_currentPlayer.CharacterName} possède {_currentPlayer.Pokemons.Count} Pokemons, un {_pokemon1.PokemonName} et un {_pokemon2.PokemonName}");
                    break;
            }
        }
    }

    /// <summary>
    /// Choose a random opponent who don't have the healer type.
    /// </summary>
    public void SetOpponents1()
    {
        if (!isGameStarted || isBattleStarted || isGameEnded)
        {
            return;
        }

        Human _temporaryOpponent = players[Random.Range(0, players.Count)];

        // If the opponent is a healer or if it is the same as the second opponent, we choose another one
        if (_temporaryOpponent.CharacterType == "Healer" || _temporaryOpponent == Opponent2)
        {
            SetOpponents1();
        }
        else
        {
            Opponent1 = _temporaryOpponent;
            Debug.Log($"L'adversaire 1 est {Opponent1.CharacterName}");
        }
    }

    /// <summary>
    /// Choose a random opponent who don't have the healer type.
    /// </summary>
    public void SetOpponents2()
    {
        if (!isGameStarted || isBattleStarted || isGameEnded)
        {
            return;
        }

        Human _temporaryOpponent = players[Random.Range(0, players.Count)];

        // If the opponent is a healer or if it is the same as the second opponent, we choose another one
        if (_temporaryOpponent.CharacterType == "Healer" || _temporaryOpponent == Opponent1)
        {
            SetOpponents2();
        }
        else
        {
            Opponent2 = _temporaryOpponent;
            Debug.Log($"L'adversaire 2 est {Opponent2.CharacterName}");
        }
    }

    /// <summary>
    /// Start a fight between two humans defined earlier with the 2 functions SetOpponentX.
    /// </summary>
    /// <param name="_opponent1"></param>
    /// <param name="_opponent2"></param>
    public void StartBattle()
    {
        if (isGameEnded)
        {
            return;
        }

        if (!isGameStarted || isBattleStarted)
        {
            Debug.Log("La partie n'a pas commencé ou un combat est déjà en cours");
            return;
        }

        if (Opponent1 == null || Opponent2 == null)
        {
            Debug.Log("Il faut deux adversaires pour commencer un combat");
            return;
        }

        if (isGameEnded)
        {
            return;
        }
        isBattleStarted = true;
        Debug.Log($"Début du combat entre {Opponent1.CharacterName} et {Opponent2.CharacterName}");

        // Set the active pokemon for each opponent
        Oponent1CurrentPokemon = Opponent1.Pokemons[0];
        Debug.Log($"{Opponent1.CharacterName} envoie son {Oponent1CurrentPokemon.PokemonName}");
        Oponent1CurrentPokemon.GetOutPokeball();

        Oponent2CurrentPokemon = Opponent2.Pokemons[0];
        Debug.Log($"{Opponent2.CharacterName} envoie son {Oponent2CurrentPokemon.PokemonName}");
        Oponent2CurrentPokemon.GetOutPokeball();

        EndTurn();
    }

    /// <summary>
    /// Make the current pokemon of the opponent that have to play attack the current pokemon of the other opponent.
    /// </summary>
    public void PlayerAttackTackle()
    {
        if (isGameEnded)
        {
            return;
        }

        // Checks if the battle has started
        if (!isBattleStarted)
        {
            Debug.Log("On ne peut pas attaquer");
            return;
        }

        Human _currentOpponent;
        Pokemon _currentOpponentPokemon;
        Human _targetMaster;
        Pokemon _target;

        // Set wich player will attack with wich pokemon
        switch (TurnIndex % 2)
        {
            case 0:
                _currentOpponent = Opponent1;
                _currentOpponentPokemon = Oponent1CurrentPokemon;
                _targetMaster = Opponent2;
                _target = Oponent2CurrentPokemon;
                break;
            case 1:
                _currentOpponent = Opponent2;
                _currentOpponentPokemon = Oponent2CurrentPokemon;
                _targetMaster = Opponent1;
                _target = Oponent1CurrentPokemon;
                break;
            default:
                return;
        }

        // Launch the attack if the pokemon is not in his pokeball
        if (!_currentOpponentPokemon.IsInPokeball)
        {
            Debug.Log($"{_currentOpponent.CharacterName} attaque avec {_currentOpponentPokemon.PokemonName}");
            _currentOpponentPokemon.Takle(_target);
            CheckKOPokemon(_target, _targetMaster);

            if (_targetMaster.NumberofPokemonsKO >= 2)
            {
                return;
            }

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
        if (isGameEnded)
        {
            return;
        }

        // Checks if the battle has started
        if (!isBattleStarted)
        {
            Debug.Log("On ne peut pas utiliser de capacité");
            return;
        }

        Human _currentOpponent;
        Pokemon _currentOpponentPokemon;
        Human _targetMaster;
        Pokemon _target;

        // Set wich player will attack with wich pokemon
        switch (TurnIndex % 2)
        {
            case 0:
                _currentOpponent = Opponent1;
                _currentOpponentPokemon = Oponent1CurrentPokemon;
                _targetMaster = Opponent2;
                _target = Oponent2CurrentPokemon;
                break;
            case 1:
                _currentOpponent = Opponent2;
                _currentOpponentPokemon = Oponent2CurrentPokemon;
                _targetMaster = Opponent1;
                _target = Oponent1CurrentPokemon;
                break;
            default:
                return;
        }

        // Launch the ability if the pokemon is not in his pokeball
        if (!_currentOpponentPokemon.IsInPokeball)
        {
            Debug.Log($"{_currentOpponent.CharacterName} utilise la capacité de {_currentOpponentPokemon.PokemonName}");
            _currentOpponentPokemon.Ability(_target);
            CheckKOPokemon(_target, _targetMaster);

            if (_targetMaster.NumberofPokemonsKO >= 2)
            {
                return;
            }

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
        if (isGameEnded)
        {
            return;
        }

        // Checks if the battle has started or if it is the end of the game
        if (!isBattleStarted)
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
                _currentOpponent = Opponent1;
                _currentOpponentPokemon = Oponent1CurrentPokemon;
                _playerTurn = 0;
                break;
            case 1:
                _currentOpponent = Opponent2;
                _currentOpponentPokemon = Oponent2CurrentPokemon;
                _playerTurn = 1;
                break;
            default:
                return;
        }

        _temp.AddRange(_currentOpponent.Pokemons);
        _temp.Remove(_currentOpponentPokemon);
        _newPokemon = _temp[0];

        // Checks if the other pokemon is dead
        if (_newPokemon.Pv <= 0)
        {
            Debug.Log($"{_newPokemon.PokemonName} est KO, impossible de le changer");
            return;
        }

        Debug.Log($"{_currentOpponent.CharacterName} change de pokemon et rappelle son {_currentOpponentPokemon.PokemonName}");
        _currentOpponentPokemon.GetInPokeball();

        _currentOpponentPokemon = _newPokemon;

        Debug.Log($"{_currentOpponent.CharacterName} envoie son {_currentOpponentPokemon.PokemonName}");
        _currentOpponentPokemon.GetOutPokeball();

        switch (_playerTurn)
        {
            case 0:
                Oponent1CurrentPokemon = _currentOpponentPokemon;
                break;
            case 1:
                Oponent2CurrentPokemon = _currentOpponentPokemon;
                break;
            default:
                Debug.Log("Erreur dans le changement de pokemon");
                return;
        }

        EndTurn();
    }

    /// <summary>
    /// Launch the healOne method of the Healer.
    /// </summary>
    public void HealOne()
    {
        if (isGameEnded || !isGameStarted)
        {
            return;
        }

        Pokemon _currentPlayerPokemon;
        Human _currentPlayerPokemonMaster;

        // Checks wich player is playing
        switch (TurnIndex % 2)
        {
            case 0:
                _currentPlayerPokemon = Oponent1CurrentPokemon;
                _currentPlayerPokemonMaster = Opponent1;
                break;

            case 1:
                _currentPlayerPokemon = Oponent2CurrentPokemon;
                _currentPlayerPokemonMaster = Opponent2;
                break;

            default:
                return;
        }

        if (_currentPlayerPokemon.Pv <= 0)
        {
            Debug.Log($"{_currentPlayerPokemon.PokemonName} est KO, et ne peut être soigné. {_currentPlayerPokemonMaster.CharacterName} doit changer de pokemon");
            return;
        }

        currentHealer.HealOne(_currentPlayerPokemon);
        EndTurn();
    }

    /// <summary>
    /// Launch the healAll method of the Healer.
    /// </summary>
    public void HealAll()
    {
        if (isGameEnded || !isGameStarted)
        {
            return;
        }

        Pokemon _currentPlayerPokemon;
        Human _currentPlayerPokemonMaster;

        // Checks wich player is playing
        switch (TurnIndex % 2)
        {
            case 0:
                _currentPlayerPokemon = Oponent1CurrentPokemon;
                _currentPlayerPokemonMaster = Opponent1;
                break;

            case 1:
                _currentPlayerPokemon = Oponent2CurrentPokemon;
                _currentPlayerPokemonMaster = Opponent2;
                break;

            default:
                return;
        }

        if (_currentPlayerPokemon.Pv <= 0)
        {
            Debug.Log($"{_currentPlayerPokemon.PokemonName} est KO, et ne peut être soigné. {_currentPlayerPokemonMaster.CharacterName} doit changer de pokemon");
            return;
        }

        currentHealer.HealAll();
        EndTurn();
    }

    /// <summary>
    /// Checks if the pokemon is KO and if the player still have more pokemon.
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_targetMaster"></param>
    private void CheckKOPokemon(Pokemon _target, Human _targetMaster)
    {
        if (isGameEnded)
        {
            return;
        }

        switch (_target.Pv <= 0)
        {
            case true:
                _targetMaster.NumberofPokemonsKO += 1;
                break;
            default:
                return;
        }

        if (_targetMaster.NumberofPokemonsKO >= 2)
        {
            if (_targetMaster == Opponent1)
            {
                EndBattle(Opponent2);
            }
            else
            {
                EndBattle(Opponent1);
            }
        }
    }

    /// <summary>
    /// End the turn, trigger the event OnTurnChange and increment the turn index.
    /// </summary>
    private void EndTurn()
    {
        if (isGameEnded)
        {
            return;
        }

        TurnIndex += 1;
        OnTurnChange?.Invoke();
    }

    /// <summary>
    /// End the battle and reset the turn index.
    /// </summary>
    private void EndBattle(Human _winner)
    {
        if (!isBattleStarted)
        {
            Debug.Log("Le combat n'a pas commencé, il ne peut pas y avoir de gagnant");
            return;
        }

        isGameEnded = true;
        Debug.Log($"Fin du combat entre {Opponent1.CharacterName} et {Opponent2.CharacterName}. \n Le gagnant est {_winner.CharacterName} \n Merci d'avoir joué a {this.name}");
    }
}
