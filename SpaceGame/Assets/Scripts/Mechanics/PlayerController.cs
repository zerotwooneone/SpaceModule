using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        
        /*internal new*/ public Collider2D collider2d;
        public Health health;
        public bool controlEnabled = true;

        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;
        public Vector2 thrust = Vector2.zero;
        
        void Awake()
        {
            health = GetComponent<Health>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                var rotationRad = (body.rotation + (Mathf.PI / 2)) * (Mathf.PI/180);
                //sin and cos are flipped because our origin is verticle
                var xFactor = -Mathf.Sin(rotationRad);
                var yFactor = Mathf.Cos(rotationRad);
                thrust = new Vector2(xFactor,yFactor) * Input.GetAxis("Vertical");
                var rotationSpeed = -Input.GetAxis("Horizontal");

                if (body.angularVelocity != 0)
                {
                    int x = 0;
                }

                if (rotationSpeed != 0)
                {
                    int x = 0;
                }
                
                body.rotation += rotationSpeed* 100* Time.deltaTime;
                //body.AddTorque(rotationSpeed*1000* Time.deltaTime, ForceMode2D.Force);
                body.position += thrust * (10f * Time.deltaTime);
            }
            else
            {
                move.x = 0;
                move.y = 0;
            }
            base.Update();
        }

        protected override void ComputeVelocity()
        {
            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }
    }
}