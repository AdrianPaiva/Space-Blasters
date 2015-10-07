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
    class Enemy
    {
        Vector2 position;
        Texture2D picture;
        float speed = 100.0f;

        float deltaX = 0.0f;
        float xLength = 0.0f;
        float xStart = 0.0f;

        bool firingActive = false;
        bool firing = false;
        float fireSpeed = 3.0f;
        float totalTime = 0.0f;

        float radius = 40.0f;

        public Enemy(Texture2D picture,Vector2 startPosition, float speed)
        {
            this.picture = picture;

            position = startPosition;

            this.speed = speed;
        }

        public void SetAcrossMovement(float deltaX, float xLength)
        {
            this.deltaX = deltaX;
            this.xLength = xLength;
            xStart = position.X;
        }
        public bool FiringActive
        {
            set { firingActive = value; }
            get { return firingActive; }
        }
        public bool Firing
        {
            set { firing = value; }
            get { return firing; }
        }
        public Vector2 Position
        {
            set { position = value; }
            get { return position; }
        }
        public float Radius
        {
            get { return radius; }
        } 

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(picture, position, null, Color.White, 0.0f, new Vector2(40.0f,
            20.0f), 0.7f, SpriteEffects.None, 0.0f);
        }
        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X += deltaX * elapsed;

            if (position.X < xStart - xLength || position.X > xStart + xLength)
            {
                deltaX *= -1.0f;
                
            }

            position.Y += speed * elapsed;

            totalTime += elapsed;

            if (totalTime > fireSpeed)
            {
                totalTime = 0.0f;
                firing = true;
            }
            
                
        }

        public int CollisionBall(List<AttackBall> attackBallList)
        {
            for (int i = 0; i < attackBallList.Count; i++)
            {
                if ((attackBallList[i].Position - position).Length() < radius)
                    return i;
            }

            return -1;
        } 


    }
}
