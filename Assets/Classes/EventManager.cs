using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Interface between Gameplay and AI classes. Gameplay adds and event, AI takes event and evaluates
/// </summary>
public class EventManager {
    //From functions
    private static Action actionToReturn;
    private static Guardian guardian;
    /// <summary>
    /// Adds an event. It should be used where type of event is not known otherwise related method should be used.
    /// </summary>
    /// <param name="gameEvent">GameEvent to be added</param>
    public static void AddEvent(GameEvent gameEvent) {

        //TODO: Implement
        return;
    }

    /// <summary>
    /// Removes and retrieves an event.
    /// </summary>
    /// <returns>GameEvent to be returned</returns>
    public static GameEvent RemoveEvent() {

        //TODO: Implement
        return null;
    }

    public static Action Spot(SpotEvent gameEvent) {

        Debug.Log("SpotIn");
        actionToReturn = null;
        if (gameEvent.Source.GetType() == typeof(Guardian)) {
            while (gameEvent.Source.actionQueue.Peek() != null && gameEvent.Source.actionQueue.Peek().priority >= 2) {
                gameEvent.Source.actionQueue.Remove();
            }
            ((Guardian)gameEvent.Source).fov2D.fovMaxDistance = 8;
            actionToReturn = new FireAction(AIController.GetPlayer());
        } else if (gameEvent.Source.GetType() == typeof(Turret)) {
            actionToReturn = new FireAction(AIController.GetPlayer());
        } else {
            Debug.Log("Null in Spot");
        }

        return actionToReturn;
    }

    public static Action SpotOut(SpotOutEvent gameEvent) {

        Debug.Log("SpotOut");
        if (gameEvent.Source.GetType() == typeof(Guardian)) {

            guardian = (Guardian)(gameEvent.Source);
            if (guardian != null) {
                if(guardian.vision.playerInVision){
                    guardian.actionQueue.Remove();
                    if (PlayerGhost.instance.isActive) {
                        guardian.actionQueue.Insert(new SearchAction(PlayerGhost.instance));
                    } else {
                        guardian.actionQueue.Insert(new SearchAction(Player.instance));
                    }
                }
            }
        }
        return new EmptyAction();
    }
}
