﻿using System;

namespace EirEngine.Core;
public class GameStateManager
    {
        /// <summary>
        /// The singleton instance of the GameStateManager.
        /// </summary>
        private static GameStateManager instance;
        private Stack<IGameState> screens = new Stack<IGameState>();
        // Singleton Pattern Logic
        public static GameStateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameStateManager();
                }
                return instance;
            }
        }
        /// <summary>
        /// Adds a screen to the top of the stack.
        /// </summary>
        /// <param name="screen">The GameState to push to the top of the stack.</param>
        public void AddScreen(IGameState screen)
        {
            try
            {
                // add screen to the stack
                screens.Push(screen);
                /*
                if (content != null)
                {
                    screens.Peek().LoadContent(content);
                }*/
                screens.Peek().Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Returns the current rendering screen at the top of the stack.
        /// </summary>
        /// <returns></returns>
        public IGameState GetScreen()
        {
            try
            {
                return screens.Peek();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        /// <summary>
        /// Removes the top screen (gamestate) from the stack.
        /// </summary>
        public void RemoveScreen()
        {
            if (screens.Count > 0)
            {
                try
                {
                    // var screen = screens.Peek();
                    screens.Pop();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Removes the top screen (gamestate) from the stack, and appends a new one.
        /// </summary>
        /// <param name="screen"></param>
        public void SwapScreen(IGameState screen)
        {
            RemoveScreen();
            AddScreen(screen);
        }

        /// <summary>
        /// Clears the entire stack of gamestates.
        /// </summary>
        public void ClearScreens()
        {
            screens.Clear();
        }
        /// <summary>
        /// Purges all screens from the stack, and adds a new one.
        /// </summary>
        /// <param name="screen">The new <see cref="IGameState"/> screen to add.</param>
        public void ChangeScreen(IGameState screen)
        {
            ClearScreens();
            AddScreen(screen);
        }
        /// <summary>
        /// Updates the top screen of the stack.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if (screens.Count > 0)
            {
                screens.Peek().Update(gameTime);

            }
        }
        /// <summary>
        /// Renders the top screen of the stack.
        /// </summary>
        public void Draw()
        {
            if (screens.Count > 0)
            {
                screens.Peek().Draw();
            }
        }
        // TODO: make this be called once the screen is added, not iterate over all
        // TODO: suggestion - why do we need to call OnLoad() on every state upon game load? perhaps can only load top of stack?

        /// <summary>
        /// Calls OnLoad methods for all screens when loading.
        /// </summary>
        public void OnLoad()
        {
            foreach (IGameState state in screens)
            {
                state.OnLoad();
            }
        }
        /// <summary>
        /// Calls Dispose methods for all screens when unloading.
        /// </summary>
        public void Dispose()
        {
            foreach (IGameState state in screens)
            {
                state.Dispose();
            }
        }
    }