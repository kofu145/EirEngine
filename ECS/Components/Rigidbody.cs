﻿using System.Numerics;
using GramEngine.Core;

namespace GramEngine.ECS.Components;

public class Rigidbody : Component
{
    public Vector3 Velocity { get; set; }
    public float Mass;
    
    private bool moveWithRotation;

    public Rigidbody(bool moveWithRotation = false)
    {
        Mass = 1;
        Velocity = Vector3.Zero;
        this.moveWithRotation = moveWithRotation;
    }
    public void AddForce(Vector3 force)
    {
        Velocity += force;
    }

    public override void Initialize()
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (moveWithRotation)
            ParentEntity.Transform.Rotation = new Vector3(0, 0, MathUtil.RadToDeg((float)Math.Atan2(Velocity.Y, Velocity.X))+90);

        ParentEntity.Transform.Position += Velocity * gameTime.DeltaTime;
    }
}