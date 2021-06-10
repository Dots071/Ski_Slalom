using UnityEngine;
using UnityEngine.Events;

public class Events : Singleton<Events>
{
    public UnityEvent playerGotHit;
    public UnityEvent missedFlag;
    public UnityEvent RaceStarted;
    public UnityEvent RaceFinished;
    public UnityEvent QuitGameSequenceInitiated;

    [System.Serializable] public class EventGameState : UnityEvent<GameManger.GameState, GameManger.GameState> { }



}
