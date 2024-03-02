using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        // TODO: Set key event handler (inherited window field of DIKUGame class)

    }


    public override void Render(){
        throw new System.NotImplementedException("Galaga game has nothing to render yet.");
    }

    public override void Update(){
        throw new System.NotImplementedException("Galaga game has no entities to update yet.");
    }

    private void KeyPress(KeyboardKey key) {
        // TODO: Close window if escape is pressed
        // TODO: switch on key string and set the player's move direction
    }

    private void KeyRelease(KeyboardKey key) {
        // TODO: switch on key string and disable the player's move direction
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        // TODO: Switch on KeyBoardAction and call proper method
        }

    public void ProcessEvent(GameEvent gameEvent) {
        // Leave this empty for now
    }
}
