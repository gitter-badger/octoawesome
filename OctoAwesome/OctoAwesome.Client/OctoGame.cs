using OctoAwesome;
using OctoAwesome.Client.Components;
using OctoAwesome.Client.Controls;
using OctoAwesome.Runtime;
using System;
using System.Configuration;
using System.Linq;
using engenious;

namespace OctoAwesome.Client
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    internal class OctoGame : Game
    {

        public CameraComponent Camera { get; private set; }

        public PlayerComponent Player { get; private set; }

        public SimulationComponent Simulation { get; private set; }

        public ScreenComponent Screen { get; private set; }

        public OctoGame()
            : base()
        {

            Content.RootDirectory = "Content";
            Title = "OctoAwesome";
            IsMouseVisible = true;
            //AllowUserResizing = true;

            //TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 15);

            int viewrange;
            if (int.TryParse(SettingsManager.Get("Viewrange"), out viewrange))
            {
                if (viewrange < 1)
                    throw new NotSupportedException("Viewrange in app.config darf nicht kleiner 1 sein");

                SceneControl.VIEWRANGE = viewrange;
            }

            Simulation = new SimulationComponent(this);
            Simulation.UpdateOrder = 4;
            Components.Add(Simulation);

            Player = new PlayerComponent(this);
            Player.UpdateOrder = 2;
            Components.Add(Player);

            Camera = new CameraComponent(this);
            Camera.UpdateOrder = 3;
            Components.Add(Camera);

            Screen = new ScreenComponent(this);
            Screen.UpdateOrder = 1;
            Screen.DrawOrder = 1;
            Components.Add(Screen);

            Resized += (s, e) =>
            {
                //TODO;
                /*if (Window.ClientBounds.Height == graphics.PreferredBackBufferHeight &&
                    Window.ClientBounds.Width == graphics.PreferredBackBufferWidth)
                    return;

                graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
                graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                graphics.ApplyChanges();*/
            };
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            Player.RemovePlayer();
            Simulation.ExitGame();

            base.OnExiting(sender, args);
        }
    }
}
