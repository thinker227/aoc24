namespace Utils;

public readonly record struct Vector2(int X, int Y)
{
    public int Length =>
        int.Abs(X) + int.Abs(Y);

    public override string ToString() => $"({X}, {Y})";

    public Vector2 Abs() =>
        new(int.Abs(X), int.Abs(Y));
    
    public Vector2 VectorTo(Vector2 other) =>
        other - this;

    public static Vector2 operator +(Vector2 a, Vector2 b) =>
        new(a.X + b.X, a.Y + b.Y);
    
    public static Vector2 operator -(Vector2 a, Vector2 b) =>
        new(a.X - b.X, a.Y - b.Y);
    
    public static Vector2 operator *(Vector2 vector, int x) =>
        new(vector.X * x, vector.Y * x);
    
    public static Vector2 operator /(Vector2 vector, int x) =>
        new(vector.X / x, vector.Y / x);
}
