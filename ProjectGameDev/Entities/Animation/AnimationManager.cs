using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Entities.Animation
{
    internal class AnimationManager
    {
        private List<Animations.Animation> animations;
        public Animations.Animation currentAnimation;
        public ActionState currentActionState;

        public AnimationManager()
        {
            animations = new List<Animations.Animation>();

        }
        public void AddAnimation(Texture2D texture, ActionState actionState,int startX, int startY, int schuifOp, int width, int height, int amount, Rectangle boundingBox)
        {
            var animation = new Animations.Animation(texture);
            animation.AddFrames(startX, startY, schuifOp, width, height, amount);
            animation.ActionState = actionState;
            animations.Add(animation);

            if (currentAnimation == null && actionState == ActionState.idle)
            {
                currentAnimation = animation;
            }
        }

        public Animations.Animation GetAnimation(ActionState actionState)
        {
            return animations.Find(a => a.ActionState == actionState);
        }


        public void Update(GameTime gameTime, ActionState actionState)
        {
            var animation = animations.Find(a => a.ActionState == actionState);
            if (animation != null)
            {
                currentAnimation = animation;

                if (currentAnimation.NLoops < currentAnimation.MaxLoops)
                {
                    currentAnimation.Update(gameTime);

                    if (currentAnimation.IsComplete)
                    {
                        currentAnimation.NLoops++;
                        if (currentAnimation.NLoops >= currentAnimation.MaxLoops)
                        {
                            // Perform any necessary actions after the animation loops
                            if (actionState == ActionState.hit)
                            {
                                // Handle hit animation looping completion

                                currentActionState = ActionState.idle;
                                currentAnimation = animations.Find(a => a.ActionState == ActionState.idle);
                            }
                            else if (actionState == ActionState.death)
                            {
                                // Handle death animation looping completion
                                Game1.GameState = GameState.death;
                            }
                        }
                    }
                }
            }

        }

    }

}
