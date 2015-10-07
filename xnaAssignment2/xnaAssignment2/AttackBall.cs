using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/**************************************************
* Adrian Paiva, Patrick Gomes Sanches
* 100864588 
* created: Nov 26 2014
* lastEdit: December 3 2014
**************************************************/

namespace xnaAssignment2
{
    class AttackBall
    {
        Vector2 position;
        float speed; 
        Texture2D picture;

         public AttackBall(Texture2D ballPicture, Vector2 startPosition, float updateSpeed) 
        { 
            picture = ballPicture; 
            position = startPosition; 
            speed = updateSpeed; 
        }
         public Vector2 Position { get { return position; } }

         public void Draw(SpriteBatch batch)
         {
             batch.Draw(picture, position, null, Color.White, 1.0f, new Vector2( 10.0f, 10.0f), 0.5f, SpriteEffects.None, 1.0f);
         }

         public void Update(GameTime gameTime)
         {
             position.Y -= speed *
            (float)gameTime.ElapsedGameTime.TotalSeconds;
         }
    }
}
