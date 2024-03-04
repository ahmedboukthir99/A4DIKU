using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using DIKUArcade.Physics;






namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private Player player;
    private GameEventBus eventBus;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;


    public Game(WindowArgs windowArgs) : base(windowArgs) {

        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));

        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);

        List<Image> images = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);
        for (int i = 0; i < numEnemies; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, images)));
        }

        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

    }


    public override void Render(){
        player.Render();
        enemies.RenderEntities();
        playerShots.RenderEntities();
    }

    public override void Update(){
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                var closeEvent = new GameEvent {
                    EventType = GameEventType.WindowEvent,
                    Message = "CLOSE_WINDOW"
                };
                eventBus.RegisterEvent(closeEvent);
                break;
            case KeyboardKey.Left:
                player.SetMoveLeft(true);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(true);
                break;
            case KeyboardKey.Space:
                Vec2F playerPos = player.GetPosition();
                Vec2F shotPos = new Vec2F(playerPos.X + 0.05f, playerPos.Y + 0.01f); // center shot with player
                PlayerShot newShot = new PlayerShot(shotPos, playerShotImage);
                playerShots.AddEntity(newShot);
                break;
        }
    }


    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(false);
                break;
        }
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        //Leave this empty for now
    }
    private void IterateShots() {
        playerShots.Iterate(shot => {
            shot.Shape.Move();// Move the shot
            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity(); // Guard againstt window border and delete short
            } else {
                // Check for collision with enemies
                enemies.Iterate(enemy => {
                    if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
                        shot.DeleteEntity(); // Marks the shot for deletion
                        enemy.DeleteEntity(); // Marks the enemy for deletion
                    }
                });
            }
        });
            
    }
}
