using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CameraTest;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Camera _camera;
    private static Matrix _screenMatrix;
    private readonly int _width;
    private readonly int _height;
    private int _windowWidth;
    private int _windowHeight;
    private Viewport _viewport;
    private Texture2D _example;

    public Game1()
    {
        _windowWidth = 1280;
        _windowHeight = 720;

        _width = 320;
        _height = 240;

        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = _windowWidth;
        _graphics.PreferredBackBufferHeight = _windowHeight;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _camera = new Camera(_width, _height);
        _camera.CenterOrigin();
    }

    protected override void Initialize()
    {
        float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
        float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        // get View Size
        if (screenWidth / _width > screenHeight / _height)
        {
            _windowWidth = (int)(screenHeight / _height * _width);
            _windowHeight = (int)screenHeight;
        }
        else
        {
            _windowWidth = (int)screenWidth;
            _windowHeight = (int)(screenWidth / _width * _height);
        }

        _screenMatrix = Matrix.CreateScale(_windowWidth / (float)_width);

        _viewport = new Viewport
        {
            X = (int)(screenWidth / 2 - _windowWidth / 2),
            Y = (int)(screenHeight / 2 - _windowHeight / 2),
            Width = _windowWidth,
            Height = _windowHeight,
            MinDepth = 0,
            MaxDepth = 1
        };

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _example = Content.Load<Texture2D>("Graphics/collisions");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        var spritePosition = new Vector2(0.0f, 0.0f);
        var spriteOffset = new Vector2(_example.Width, _example.Height) / 2.0f;

        _spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: _camera.Matrix * _screenMatrix
        );
        _spriteBatch.Draw(_example, spritePosition - spriteOffset, Color.White);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}