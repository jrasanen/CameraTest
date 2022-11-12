using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CameraTest;

public class Camera
{
    private bool _changed;
    private Matrix _matrix = Matrix.Identity;
    private Vector2 _origin = Vector2.Zero;
    private Vector2 _position = Vector2.Zero;
    public Viewport Viewport;
    private Vector2 _zoom = Vector2.One;

    public Camera(int width, int height)
    {
        Viewport = new Viewport()
        {
            Width = width,
            Height = height
        };
        UpdateMatrices();
    }

    public Matrix Matrix
    {
        get
        {
            if (_changed)
                UpdateMatrices();
            return _matrix;
        }
    }


    public Vector2 ViewportCenter => new((int)Math.Floor(_origin.X), (int)Math.Floor(_origin.Y));

    private void UpdateMatrices()
    {
        _matrix = Matrix.Identity *
                  Matrix.CreateTranslation(
                      new Vector3(-new Vector2((int)Math.Floor(_position.X), (int)Math.Floor(_position.Y)), 0)) *
                  Matrix.CreateRotationZ(0.0f) *
                  Matrix.CreateScale(new Vector3(_zoom, 1)) *
                  Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
        _changed = false;
    }

    public void CenterOrigin()
    {
        _origin = new Vector2((float)Viewport.Width / 2.0f, (float)Viewport.Height / 2.0f);
        _changed = true;
    }
}