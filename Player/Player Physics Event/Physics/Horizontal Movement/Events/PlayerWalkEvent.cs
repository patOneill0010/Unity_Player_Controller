﻿using UnityEngine;

public class PlayerWalkEvent : AHorizontal, IPlayerEvent {

    public PlayerWalkEvent(PlayerPhysics playerPhysics, Rigidbody playerRigidbody) {
        _PhysicsPlayer = playerPhysics;
        _PlayerRigidbody = playerRigidbody;
        _PlayerTransform = playerRigidbody.transform;
        _MaxVelocity = 3.0f;
        _Acceleration = 3.0f;
    }

    public void ExecuteAnimationEvent() {
        throw new System.NotImplementedException();
    }

    public void ExecutePhysicsEvent() {
        Vector3 direction = _PhysicsPlayer.CalculateHorizontalDirectionWalk();
        MovePlayerHorizontally(direction);
    }
}
