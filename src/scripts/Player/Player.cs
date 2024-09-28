using Godot;

public partial class Player : CharacterBody2D
{

	private float speed = 200f;

	private float desiredHeightInUnits = 2f;
	private float pixelsPerUnit = 64f;

	public override void _Ready()
	{
		// Получаем ноду AnimatedSprite2D
		var animatedSprite = (AnimatedSprite2D)GetNode("AnimatedSprite2D");

		// Получаем SpriteSheet, который хранит анимации
		var spriteSheet = animatedSprite.SpriteFrames;

		// Получаем текстуру первого кадра текущей анимации
		Texture2D firstFrameTexture = spriteSheet.GetFrameTexture(animatedSprite.Animation, 0);

		// Получаем размер первого кадра
		Vector2 frameSize = firstFrameTexture.GetSize();

		// Рассчитываем масштаб, чтобы высота игрока соответствовала 2 игровым единицам
		float scaleFactor = desiredHeightInUnits / (frameSize.Y / pixelsPerUnit);

		// Устанавливаем масштаб для увеличения игрока
		Scale = new Vector2(scaleFactor, scaleFactor);
	}

	public override void _Process(double delta)
	{

		Walking();

		MoveAndSlide();

	}

	private void Walking()
	{
		Vector2 direction = Vector2.Zero;

		if (Input.IsActionPressed("ui_up"))    // W
		{
			direction.Y -= 1;
		}
		if (Input.IsActionPressed("ui_down"))  // S
		{
			direction.Y += 1;
		}
		if (Input.IsActionPressed("ui_left"))  // A
		{
			direction.X -= 1;
		}
		if (Input.IsActionPressed("ui_right")) // D
		{
			direction.X += 1;
		}

		if (direction != Vector2.Zero)
		{
			direction = direction.Normalized();
		}

		Velocity = direction * speed;

	}
}
