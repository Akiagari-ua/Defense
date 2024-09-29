using System;
using Godot;

public partial class Player : CharacterBody2D
{

	private float speed = 200f;

	private float desiredHeightInUnits = 2f;
	private float pixelsPerUnit = 64f;

	string watchDirection = "down";
	string playerState = "idle";

	AnimatedSprite2D sprite = null;


	public override void _Ready()
	{
		sprite = (AnimatedSprite2D)GetNode("AnimatedSprite2D");

		sprite?.Play("idle_down");
	}

	public override void _Process(double delta)
	{

		Walking();
		if (Velocity.X == 0 && Velocity.Y == 0)
		{
			if (watchDirection == "down")
			{
				sprite?.Play("idle_down");
			}
			if (watchDirection == "up")
			{
				sprite?.Play("idle_up");
			}
			if (watchDirection == "left")
			{
				sprite?.Play("idle_left");
			}
			if (watchDirection == "right")
			{
				sprite?.Play("idle_right");
			}
		}


		MoveAndSlide();

	}

	private void Walking()
	{
		Vector2 direction = Vector2.Zero;

		if (Input.IsActionPressed("ui_up"))    // W
		{
			direction.Y -= 1;
			watchDirection = "up";
			playerState = "run";
			sprite?.Play("run_up");
		}
		if (Input.IsActionPressed("ui_down"))  // S
		{
			direction.Y += 1;
			watchDirection = "down";
			playerState = "run";
			sprite?.Play("run_down");


		}
		if (Input.IsActionPressed("ui_left"))  // A
		{
			direction.X -= 1;
			watchDirection = "left";
			playerState = "run";
			sprite?.Play("run_left");

		}
		if (Input.IsActionPressed("ui_right")) // D
		{
			direction.X += 1;
			watchDirection = "right";
			playerState = "run";
			sprite?.Play("run_right");
		}

		if (direction != Vector2.Zero)
		{
			direction = direction.Normalized();
		}


		Velocity = direction * speed;

	}
}
