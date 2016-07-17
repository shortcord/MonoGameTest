using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DTest {
    class _3DTestGame : Game {

        GraphicsDeviceManager graphics;

        Model model;

        VertexPositionTexture[] floorVerts;
        BasicEffect effect;

        public _3DTestGame() {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent() {
            model = Content.Load<Model>("Enneper Curve Ring 17.53mm with Round Gem");

            floorVerts = new VertexPositionTexture[6];
            floorVerts[0].Position = new Vector3(-20, -20, 0);
            floorVerts[1].Position = new Vector3(-20, 20, 0);
            floorVerts[2].Position = new Vector3(20, -20, 0);
            floorVerts[3].Position = floorVerts[1].Position;
            floorVerts[4].Position = new Vector3(20, 20, 0);
            floorVerts[5].Position = floorVerts[2].Position;

            effect = new BasicEffect(graphics.GraphicsDevice);
            effect.TextureEnabled = true;
            effect.Texture = Content.Load<Texture2D>("text");

            base.LoadContent();
        }


        void DrawPrimitive() {
            var cameraPosition = new Vector3(0, 120, 20);
            var cameraLookAtVector = Vector3.Zero;
            var cameraUpVector = Vector3.UnitZ;

            effect.View = Matrix.CreateLookAt(
                cameraPosition, cameraLookAtVector, cameraUpVector);

            float aspectRatio =
                graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            float fieldOfView = MathHelper.PiOver4;
            float nearClipPlane = 1;
            float farClipPlane = 200;

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                fieldOfView, aspectRatio, nearClipPlane, farClipPlane);

            foreach (var pass in effect.CurrentTechnique.Passes) {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, floorVerts, 0, 2);
            }
        }

        void Draw(Vector3 rot, Vector3 pos) {
            foreach (var mesh in model.Meshes) {
                foreach (BasicEffect effect in mesh.Effects) {

                    effect.EnableDefaultLighting();

                    effect.PreferPerPixelLighting = true;

                    effect.World = Matrix.CreateRotationZ(rot.Z)
                        * Matrix.CreateRotationX(rot.X)
                        * Matrix.CreateRotationY(rot.Y)
                        * Matrix.CreateTranslation(pos);

                    var cameraPosition = new Vector3(0, 80, 0);
                    var cameraLookAtVector = Vector3.Zero;
                    var cameraUpVector = Vector3.UnitZ;

                    effect.View = Matrix.CreateLookAt(
                        cameraPosition,
                        cameraLookAtVector,
                        cameraUpVector);

                    float aspectRatio = graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight;

                    float fieldOfView = MathHelper.PiOver4;

                    float nearClipPlane = 1;
                    float farClipPlane = 200;

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        fieldOfView,
                        aspectRatio,
                        nearClipPlane,
                        farClipPlane);
                }

                mesh.Draw();
            }
        }
        Vector3 post = Vector3.One;
        protected override void Update(GameTime gameTime) {
            var KeyboardState = Keyboard.GetState();
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var postPre = Vector3.Zero;

            if (KeyboardState.IsKeyDown(Keys.W)) {
                postPre += Vector3.Up * 5;
            }

            if (KeyboardState.IsKeyDown(Keys.S)) {
                postPre += Vector3.Down * 5;
            }

            if (KeyboardState.IsKeyDown(Keys.A)) {
                postPre += Vector3.Left * 5;
            }

            if (KeyboardState.IsKeyDown(Keys.D)) {
                postPre += Vector3.Right * 5;
            }

            post += postPre;

            base.Update(gameTime);
        }

        float pp = 0f;
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //DrawPrimitive();
            var ppValue = pp += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Draw(new Vector3(ppValue, ppValue, 0), post);
        }
    }
}
