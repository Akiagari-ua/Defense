using Godot;
using System;

public partial class Camera : Camera2D
{

	// Размер отступа для игрока перед началом движения камеры
	private float deadZoneSize = 100f;

	// Скорость сглаживания камеры (чем больше, тем быстрее)
	private float smoothingFactor = 0.5f;

	private CharacterBody2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (CharacterBody2D)GetNode("../Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Получаем текущие позиции камеры и игрока
		Vector2 cameraPosition = GlobalPosition;
		Vector2 playerPosition = player.GlobalPosition;

		// Рассчитываем разницу между позицией камеры и игрока
		Vector2 offset = playerPosition - cameraPosition;

		// Если игрок вышел за пределы мертвой зоны
		if (Math.Abs(offset.X) > deadZoneSize || Math.Abs(offset.Y) > deadZoneSize)
		{
			// Рассчитываем целевую позицию камеры
			Vector2 targetPosition = cameraPosition + offset;

			// Плавное экспоненциальное сглаживание камеры
			GlobalPosition = cameraPosition.Lerp(targetPosition, 1 - Mathf.Pow(1 - smoothingFactor, (float)delta));
		}
	}
}


