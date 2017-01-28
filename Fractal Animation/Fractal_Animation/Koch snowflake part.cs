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
    public class Koch_snowflake_part
    {
        public VertexPositionColor[] vertices;

        public Koch_snowflake_part()
        {
            vertices = new VertexPositionColor[3];

            vertices[0].Position = new Vector3(-1.5f, -2.25f, 0f);
            vertices[0].Color = Color.Yellow;
            vertices[1].Position = new Vector3(0, 2.25f, 0f);
            vertices[1].Color = Color.Orange;
            vertices[2].Position = new Vector3(1.5f, -2.25f, 0f);
            vertices[2].Color = Color.Red;
        }
        public Koch_snowflake_part(VertexPositionColor Edge1, VertexPositionColor Edge2, VertexPositionColor Edge3)
        {
            vertices = new VertexPositionColor[3];

            vertices[0] = Edge1;
            vertices[1] = Edge2;
            vertices[2] = Edge3;
        }

        public void CreateOffsprings()
        {
            VertexPositionColor A = vertices[0];
            VertexPositionColor B = vertices[1];
            VertexPositionColor C = vertices[2];

            VertexPositionColor Ab = new VertexPositionColor(A.Position + (B.Position - A.Position) / 3, Color.Lerp(A.Color, B.Color, 0.3333f));
            VertexPositionColor aB = new VertexPositionColor(A.Position + (B.Position - A.Position) * 2 / 3, Color.Lerp(A.Color, B.Color, 0.6666f));

            VertexPositionColor Bc = new VertexPositionColor(B.Position + (C.Position - B.Position) / 3, Color.Lerp(B.Color, C.Color, 0.3333f));
            VertexPositionColor bC = new VertexPositionColor(B.Position + (C.Position - B.Position) * 2 / 3, Color.Lerp(B.Color, C.Color, 0.6666f));

            VertexPositionColor Ca = new VertexPositionColor(C.Position + (A.Position - C.Position) / 3, Color.Lerp(C.Color, A.Color, 0.3333f));
            VertexPositionColor cA = new VertexPositionColor(C.Position + (A.Position - C.Position) * 2 / 3, Color.Lerp(C.Color, A.Color, 0.6666f));

            Vector3 M = (A.Position + B.Position + C.Position) / 3;

            VertexPositionColor AB = new VertexPositionColor(C.Position + (M - C.Position) * 2, Color.Lerp(A.Color, B.Color, 0.5f));
            VertexPositionColor BC = new VertexPositionColor(A.Position + (M - A.Position) * 2, Color.Lerp(B.Color, C.Color, 0.5f));
            VertexPositionColor CA = new VertexPositionColor(B.Position + (M - B.Position) * 2, Color.Lerp(C.Color, A.Color, 0.5f));

            Manager.SnowflakeParts.Add(new Koch_snowflake_part(Ab, AB, aB));
            Manager.SnowflakeParts.Add(new Koch_snowflake_part(Bc, BC, bC));
            Manager.SnowflakeParts.Add(new Koch_snowflake_part(Ca, CA, cA));

            Manager.SnowflakeParts.Add(new Koch_snowflake_part(A, Ab, cA));
            Manager.SnowflakeParts.Add(new Koch_snowflake_part(B, Bc, aB));
            Manager.SnowflakeParts.Add(new Koch_snowflake_part(C, Ca, bC));
        }

        public void Draw(GraphicsDevice GD)
        {
            GD.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1, VertexPositionColor.VertexDeclaration);
        }
    }
}
