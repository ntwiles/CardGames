using System;
using System.Collections.Generic;
using System.Text;

namespace CardGames.Drawing
{
    public abstract class GameDrawer
    {
        // "PADDING" used to apply spacing in relation to nearby element.
        // "OFFSET" used to apply spacing in relation to the screen.

        protected const int GAME_PADDING_X = 4;
        protected const int GAME_PADDING_Y = 1;

        const int SIDEBAR_WIDTH = 100;

        const int MESSAGES_OFFSET_Y = 8;

        protected virtual void drawTitle(string[] titleLines)
        {
            int posX = Console.LargestWindowWidth - GAME_PADDING_X - SIDEBAR_WIDTH;
            int posY = GAME_PADDING_Y;

            for (int i = 0; i < titleLines.Length; i++)
            {
                string titleLine = titleLines[i];
                Console.SetCursorPosition(posX, posY + i);
                Console.Write(titleLine);
            }
        }

        protected virtual void writeMessages(List<string> messages)
        {
            int posX = Console.LargestWindowWidth - GAME_PADDING_X - SIDEBAR_WIDTH;
            int posY = GAME_PADDING_Y + MESSAGES_OFFSET_Y;

            for (int i = 0; i < messages.Count; i++)
            {
                string message = messages[i];
                Console.SetCursorPosition(posX, posY + i);
                Console.Write(message);
            }
        }
    }
}
