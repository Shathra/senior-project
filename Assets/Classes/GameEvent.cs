using UnityEngine;
using System.Collections;

public enum GameEventType {
	EnemySpotted, UnknownObjectSpotted, NoiseHeard, None
}

public class GameEvent {

	public GameEventType Type { get; set; }
    public Enemy Source { get; set; }

    public GameEvent()
    {
        this.Type = GameEventType.None;
        Source = null;
    }

	public GameEvent( GameEventType type) {

		this.Type = type;
        Source = null;
	}

    public GameEvent( Enemy source, GameEventType type) {

        this.Type = type;
        Source = source;
    }
}
