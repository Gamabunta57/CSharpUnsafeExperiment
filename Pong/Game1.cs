using System;
using ECSFoundation.ECS.Entities;
using ECSFoundation.MemoryManagement.MemoryAllocators;
using ECSImplementation.Global;
using ECSImplementation.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SceneManager sceneManager;
        RenderTarget2D mainRender;
        MemoryAllocator allocator;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 640,
                PreferredBackBufferHeight = 480
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        private void OnResize(object sender, EventArgs e)
        {
            var window = (GameWindow)sender;

            graphics.PreferredBackBufferWidth = window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = window.ClientBounds.Height;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            sceneManager = new SceneManager(new PressStartScene());
            allocator = MemoryBuilder.BuildMemoryAllocator();
            EntityManager.Init(allocator);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainRender = new RenderTarget2D(GraphicsDevice,640,480);

            ECSImplementation.Global.Texture.MainTexture = new Texture2D(GraphicsDevice, 1, 1);
            ECSImplementation.Global.Texture.MainTexture.SetData(new Color[] { Color.White });
            ECSImplementation.Global.Texture.MainFont = Content.Load<SpriteFont>("File");

            sceneManager.Load();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (MatchState.AskForExit)
                Exit();
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here
            sceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(mainRender);
            sceneManager.Draw(gameTime, spriteBatch);

            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
            spriteBatch.Draw(mainRender, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                allocator.Dispose();

            base.Dispose(disposing);
        }
    }
}