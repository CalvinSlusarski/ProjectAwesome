﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameStateManagement;

namespace ProjectAwesome
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : GameScreen
    {
        //GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera = new Camera();
        ContentManager content;
        float pauseAlpha;
        GameObjectManage Gom = new GameObjectManage();

        //stuff for sound by Dan
        //AudioEngine aengine;
        //SoundBank soundBank;
        //WaveBank waveBank;
        MediaLibrary sampleMediaLibrary;
        Random rand;

        public Game1()
        {
            //graphics = new GraphicsDeviceManager(this);
            //graphics.PreferredBackBufferHeight = 600;
            //graphics.PreferredBackBufferWidth = 800;
            //Content.RootDirectory = "Content";
            //aengine = new AudioEngine("test.xgs");
            //soundBank = new SoundBank(aengine, "Sound Bank.xsb");
            //waveBank = new WaveBank(aengine, "Wave Bank.xwb");
            sampleMediaLibrary = new MediaLibrary();
            rand = new Random();
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        //protected override void Initialize()
        //{
        //    //base.Initialize();
        //}

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(ScreenManager.GraphicsDevice);
            //init spritefont for debugging
            Gom.LoadContent(content);
            //background

            //song
            MediaPlayer.Stop(); // stop current audio playback 
            MediaPlayer.Volume = 0.35f;
            // generate a random valid index into Albums
            int i = rand.Next(0, sampleMediaLibrary.Albums.Count - 1);
            int j = rand.Next(0, sampleMediaLibrary.Albums[i].Songs.Count - 1);
            // play the first track from the album
            MediaPlayer.Play(sampleMediaLibrary.Albums[i].Songs[j]);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                // Allows the game to exit
                //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                //    this.Exit();
                Gom.Update(gameTime);
                //Added by calvin

                //Added by calvin moves camera to player position
                camera.Position = Gom.player.Position;//mPlayerSprite.Position;
                //base.Update(gameTime);
            }
            /*if (Controls.playSound1 == true)
            {
                Cue cue = soundBank.GetCue("snd1");
                cue.Play();
            }
            if (Controls.playSound2 == true)
            {
                Cue cue = soundBank.GetCue("snd2");
                cue.Play();
            }
            if (Controls.playSound3 == true)
            {
                Cue cue = soundBank.GetCue("snd3");
                cue.Play();
            }*/
            if (Controls.stopSong == true)
            {
                MediaPlayer.Stop();
            }
            if ((MediaPlayer.State == MediaState.Stopped || Controls.nextSong ==true)
                && Controls.stopSong == false)
            {
                int i = rand.Next(0, sampleMediaLibrary.Albums.Count - 1);
                int j = rand.Next(0, sampleMediaLibrary.Albums[i].Songs.Count - 1);
                MediaPlayer.Play(sampleMediaLibrary.Albums[i].Songs[j]);
            }
        }
     
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);
            Gom.DrawOcean(gameTime, spriteBatch, ScreenManager.GraphicsDevice);
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                          BlendState.AlphaBlend,
                          null,
                          null,
                          null,
                          null,
                          camera.Transform(ScreenManager.GraphicsDevice));
            Gom.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
            // This is where we draw the gui
            Gom.DrawGUI(spriteBatch);
            spriteBatch.End();
            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
            //base.Draw(gameTime);
        }
    }
}
