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

        public static Vector3 CameraPos = Vector3.Zero;
        public static float CameraZoom = 1;
        static float CameraSpeed = 0.03f;

        public static void SetUp()
        {
            TriParts.Add(new Sierpinski_Triangle_Part());
        }

        public static void Update()
        {
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
            }

            CameraZoom += ((Control.CurMS.ScrollWheelValue - Control.LastMS.ScrollWheelValue) / 1000f) * CameraZoom;
            if (CameraZoom < 0.33f) { CameraZoom = 0.33f; }

            World = Matrix.Multiply(Matrix.CreateTranslation(CameraPos), Matrix.CreateScale(CameraZoom));
        }

        public static void Draw(GraphicsDevice GD)
        {
            Assets.Shader.CurrentTechnique = Assets.Shader.Techniques["Pretransformed"];
            Assets.Shader.Parameters["xWorld"].SetValue(World);

            foreach (EffectPass pass in Assets.Shader.CurrentTechnique.Passes)
            {
                pass.Apply();
                
                foreach (Sierpinski_Triangle_Part Tri in TriParts)
                    Tri.Draw(GD);
            }
        }
    }
}
