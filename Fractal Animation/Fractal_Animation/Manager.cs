using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fractal_Animation
{
    public static class Manager
    {
        public static Matrix World;

        public static List<Sierpinski_Triangle_Part> TriParts = new List<Sierpinski_Triangle_Part>();
        public static List<Koch_snowflake_part> SnowflakeParts = new List<Koch_snowflake_part>();

        public static Vector3 CameraPos = Vector3.Zero;
        public static float CameraZoom = 1;
        static float CameraSpeed = 0.03f;

        static bool Sierpinski_Triangle_Active;

        public static void SetUp()
        {
            TriParts.Clear();
            SnowflakeParts.Clear();

            TriParts.Add(new Sierpinski_Triangle_Part());
            SnowflakeParts.Add(new Koch_snowflake_part());

            Sierpinski_Triangle_Active = true;
        }

        public static void Update()
        {
            if (Control.WasKeyJustPressed(Keys.Up))
                Sierpinski_Triangle_Active = !Sierpinski_Triangle_Active;

            if (Control.WasKeyJustPressed(Keys.Right))
                Sierpinski_Triangle_Active = true;

            if (Control.WasKeyJustPressed(Keys.Left))
                Sierpinski_Triangle_Active = false;

            CameraSpeed = CameraZoom > 0.33 ? 0.02f * (1f / CameraZoom) : 0.02f;

            if (Control.CurKS.IsKeyDown(Keys.W))
                CameraPos.Y -= CameraSpeed;

            if (Control.CurKS.IsKeyDown(Keys.S))
                CameraPos.Y += CameraSpeed;

            if (Control.CurKS.IsKeyDown(Keys.A))
                CameraPos.X += CameraSpeed;

            if (Control.CurKS.IsKeyDown(Keys.D))
                CameraPos.X -= CameraSpeed;

            if (Control.WasKeyJustPressed(Keys.Enter))
            {
                int j = TriParts.Count;
                for (int i = 0; i < j; i++)
                    TriParts[i].CreateOffsprings();

                j = SnowflakeParts.Count;
                for (int i = 0; i < j; i++)
                    SnowflakeParts[i].CreateOffsprings();
            }

            if (Control.WasKeyJustPressed(Keys.X))
                SetUp();

            CameraZoom += ((Control.CurMS.ScrollWheelValue - Control.LastMS.ScrollWheelValue) / 1000f) * CameraZoom;
            if (CameraZoom < 0.175f) { CameraZoom = 0.175f; }

            World = Matrix.Multiply(Matrix.CreateTranslation(CameraPos), Matrix.CreateScale(CameraZoom));
        }

        public static void Draw(GraphicsDevice GD)
        {
            Assets.Shader.CurrentTechnique = Assets.Shader.Techniques["Pretransformed"];
            Assets.Shader.Parameters["xWorld"].SetValue(World);

            foreach (EffectPass pass in Assets.Shader.CurrentTechnique.Passes)
            {
                pass.Apply();
                
                if (Sierpinski_Triangle_Active)
                    foreach (Sierpinski_Triangle_Part Tri in TriParts)
                        Tri.Draw(GD);
                else
                    foreach (Koch_snowflake_part flake in SnowflakeParts)
                        flake.Draw(GD);
            }
        }
    }
}
