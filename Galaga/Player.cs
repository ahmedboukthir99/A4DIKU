using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Player {
    private Entity entity;
    private DynamicShape shape;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private float MOVEMENT_SPEED = 0.01f; 


    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;

    }

    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        if (shape.Position.X < 0 && shape.Direction.X > 0.0f) shape.MoveX(shape.Direction.X);
        if (shape.Position.X > 1 - shape.Extent.X && shape.Direction.X < 0.0f) shape.MoveX(shape.Direction.X);
        if (shape.Position.X > 0 && shape.Position.X < 1 - shape.Extent.X) shape.MoveX(shape.Direction.X);
    }

    public void SetMoveLeft(bool val) {
        if (val) moveLeft = -MOVEMENT_SPEED;
        if (!val) moveLeft = 0.0f;
        UpdateDirection();
    }

    public void SetMoveRight(bool val) {
        if (val) moveRight = MOVEMENT_SPEED;
        if (!val) moveRight = 0.0f;
        UpdateDirection();
    }

    private void UpdateDirection()
        {
            shape.Direction.X = moveLeft + moveRight;
        }
    public Vec2F GetPosition() {
        return shape.Position;
    }

}
