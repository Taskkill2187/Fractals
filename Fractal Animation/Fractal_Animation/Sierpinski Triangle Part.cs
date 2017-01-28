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
            vertices[0].Color = Color.Yellow;
            vertices[1].Position = new Vector3(0, 2.25f, 0f);
            vertices[1].Color = Color.Orange;
            vertices[2].Position = new Vector3(1.5f, -2.25f, 0f);
            vertices[2].Color = Color.Red;
        }
        public Sierpinski_Triangle_Part(VertexPositionColor Edge1, VertexPositionColor Egde2, VertexPositionColor Edge3)
        {
            vertices = new VertexPositionColor[3];

            vertices[0] = Edge1;
            vertices[1] = Egde2;
            vertices[2] = Edge3;
        }

        public void CreateOffsprings()
        {
            VertexPositionColor A = vertices[0]; // Bottom Left
            VertexPositionColor B = vertices[1]; // Top
            VertexPositionColor C = vertices[2]; // Bottom Right

            VertexPositionColor AB = new VertexPositionColor(A.Position + (B.Position - A.Position) / 2, Color.Lerp(A.Color, B.Color, 0.5f));
            VertexPositionColor BC = new VertexPositionColor(B.Position + (C.Position - B.Position) / 2, Color.Lerp(B.Color, C.Color, 0.5f));
            VertexPositionColor CA = new VertexPositionColor(C.Position + (A.Position - C.Position) / 2, Color.Lerp(C.Color, A.Color, 0.5f));

            // Bottom Left Triangle
            Manager.TriParts.Add(new Sierpinski_Triangle_Part(A, AB, CA));

            // Bottom Right Triangle
            Manager.TriParts.Add(new Sierpinski_Triangle_Part(CA, BC, C));

            // Shrink this one to the Offspring Top Triangle
            vertices[0] = AB;
            vertices[2] = BC;
        }

        public void Draw(GraphicsDevice GD)
        {
            GD.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1, VertexPositionColor.VertexDeclaration);
        }
    }
}
