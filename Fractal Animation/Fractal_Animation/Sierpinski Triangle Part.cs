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
    public class Sierpinski_Triangle_Part
    {
        public VertexPositionColor[] vertices;

        public Sierpinski_Triangle_Part()
        {
            vertices = new VertexPositionColor[3];

            vertices[0].Position = new Vector3(-1.5f, -2.25f, 0f);
            vertices[0].Color = Color.LightBlue;
            vertices[1].Position = new Vector3(0, 2.25f, 0f);
            vertices[1].Color = Color.LightBlue;
            vertices[2].Position = new Vector3(1.5f, -2.25f, 0f);
            vertices[2].Color = Color.Red;
        }
        public Sierpinski_Triangle_Part(Vector3 Edge1, Vector3 Egde2, Vector3 Edge3)
        {
            vertices = new VertexPositionColor[3];

            vertices[0].Position = Edge1;
            vertices[0].Color = Color.LightBlue;
            vertices[1].Position = Egde2;
            vertices[1].Color = Color.LightBlue;
            vertices[2].Position = Edge3;
            vertices[2].Color = Color.LightBlue;
        }

        public void CreateOffsprings()
        {
            Vector3 A = vertices[0].Position; // Bottom Left
            Vector3 B = vertices[1].Position; // Top
            Vector3 C = vertices[2].Position; // Bottom Right

            Vector3 AB = A + (B - A) / 2;
            Vector3 BC = B + (C - B) / 2;
            Vector3 CA = C + (A - C) / 2;

            // Bottom Left Triangle
            Manager.TriParts.Add(new Sierpinski_Triangle_Part(A, AB, CA));

            // Bottom Right Triangle
            Manager.TriParts.Add(new Sierpinski_Triangle_Part(CA, BC, C));

            // Shrink this one to the Offspring Top Triangle
            vertices[0].Position = AB;
            vertices[2].Position = BC;
        }

        public void Draw(GraphicsDevice GD)
        {
            GD.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1, VertexPositionColor.VertexDeclaration);
        }
    }
}
