using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Urho;
using Urho.Urho2D;
using Urho.Actions;

using Matchbox.Components;

namespace Matchbox
{
    public class MatchBoxes : Application
    {
        bool movementsEnabled;
        Scene scene;
        Node plotNode;
        Camera camera;
        PhysicsWorld2D physicsWorld;

        List<MBDot> dots;

        // 5x10 playing field
        int dotsX = 5; 
        int dotsY = 10;

        int padding = 50;

        public MatchBoxes(ApplicationOptions options = null) : base(options) { }

        static MatchBoxes()
        {
            UnhandledException += (s, e) =>
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
                e.Handled = true;
            };
        }

        protected override void Start()
        {
            base.Start();
            CreateScene();
            SetupViewport();
        }

        void SetupViewport()
        {
            var renderer = Renderer;
            renderer.SetViewport(0, new Viewport(Context, scene, camera, null));
        }

        void CreateScene()
        {
            Input.TouchEnd += OnTouched;

            scene = new Scene();

            var cameraNode = scene.CreateChild();
            camera = cameraNode.CreateComponent<Camera>();
            camera.Orthographic = true;

            var graphics = Graphics;
            camera.OrthoSize = graphics.Height * 0.05f;
            camera.Zoom = 1.5f * Math.Min(graphics.Width / 1280.0f, graphics.Height / 800.0f); // Set zoom according to user's resolution to ensure full visibility (initial zoom (1.5) is set for full visibility at 1280x800 resolution)

            // Create 2D physics world component
            physicsWorld = scene.CreateComponent<PhysicsWorld2D>();
            physicsWorld.DrawJoint = (true);
            physicsWorld.Gravity = new Vector2(0, 0);

            dots = new List<MBDot>(dotsX * dotsY);

            for(int i = 1; i <= dotsX; ++i)
            {
                for(int j = 1; j <= dotsY; ++j)
                {
                    MBDot dot = (MBDot)scene.CreateChild("Dot");

                    // Set rigidbody
                    RigidBody2D rb2d = dot.CreateComponent<RigidBody2D>();
                    rb2d.BodyType = BodyType2D.Static;

                    // TODO: Set StaticSprite2D
                    // StaticSprite2D sprite2d = dot.CreateComponent<StaticSprite2D>();

                    // Set collider
                    CollisionCircle2D circle = dot.CreateComponent<CollisionCircle2D>();

                    int xPos = ((graphics.Width-padding) / dotsX) * i + padding/2;
                    int yPos = ((graphics.Height-padding) / dotsY) * j + padding/2;
                    dot.Position = new Vector3(xPos, yPos, 0);

                }
            }

        }

        void OnTouched(TouchEndEventArgs e)
        {
            var results = physicsWorld.GetRigidBody(new Vector2(e.X, e.Y));
            if (results != null)
            {
                // Handle line drawing here
            }
        }
    }
}
