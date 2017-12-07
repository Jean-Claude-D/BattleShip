using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BattleShip
{
    /**
     * The narrator class hold all the text that the narrotor says and displays it
     * using animations.
     * 
     * @author karina
     **/
    class Narrator
    {
        /**
         * Text displayed when the game is openned
         *
         * @author karina
         **/
        public static void displayIntro(TextBlock msgTxt)
        {
            string text = "Welcome to Battle Ship." + "\n"
                + "I don't think we've met before..." + "\n"
                + "What's your name?";
            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 3));
        }

        /**
         * Text displayed after a player enters their name if their name is 
         * already in the database and has an unfinished game.
         * 
         * @author karina
         **/
        public static void displayNameFoundSaved(TextBlock msgTxt, string name)
        {
            string text = name + "! There you are!" + "\n"
                + "We've been waiting for you to for ages..." + "\n"
                + "Would you like to continue where you left off?";
            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 3));
        }

        /**
         * Text displayed after a player enters their name if their name is 
         * already in the database and doesn't have an unfinished game.
         * 
         * @author karina
         **/
        public static void displayNameFound(TextBlock msgTxt, string name)
        {
            string text = name + ", there you are" + "\n"
                + "Which level would you like to play this time?";
            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 3));
        }
		/**
		 * Text displayed after a player enters their name if they're new.
		 * 
		 * @author Simon
		 **/
		public static void newName(TextBlock msgTxt, string name)
		{
			string text = "Hi " + name + "\nIs this your very first time playing?";

			Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 3));
		}

		/* Text displayed while player places ships and other
		 * 
		 * @author Simon
		 **/
		public static void placing(TextBlock msgTxt, string name)
		{
			string text =( "Let us proceed " + name + ". Click on a ship to select it. Once selected use the arrow keys to move it." +
				"\nIf the orientation of a ship does not suit you, double clicking it will rotate it." +
                 "\nIf you are feeling reckless, you may shuffle the board");

			Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 4));
		}

        /* Text displayed while the player is playing
          * 
             * @author Simon
            **/
        public static void startplaying(TextBlock msgTxt, string name)
        {
            string text = ("Very well. The larger board is your opponent's. Select a square to shoot it. If you are confused by these clear instructions, " +
                "researching the rules of \"Battleship\" could help you. ");

            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 4));
        }

        /* Text displayed when the computer plays
             * 
             * @author Simon
            **/
        public static void firstturn(TextBlock msgTxt, string name)
        {
            string text = ("You just shot for the first time this game! I'd congratulate you, but a monkey would be able to do the same...");
            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 4));
        }

        /* Text displayed when the computer plays for the second time
            * 
             * @author Simon
              **/
        public static void secondturn(TextBlock msgTxt, string name)
        {
            string text = ("You seem have to gotten the hang of it. I will turn myself off now if you no longer require my assistance. If anything remotely interesting " +
                "happens, ring me. ");
            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 4));
        }


        /* Text displayed when the idle timer kicks in
            * 
        * @author Simon
            **/
        public static void timedout(TextBlock msgTxt, string name)
        {
            string text = ("I must admit, you have impressed me. Never in my short artificial life have I seen someone get timed out by the timer" +
                " they created themselves. Kudos.");
            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 4));
        }

        /* Text displayed when the first ship is shot down
           * 
       * @author Simon
           **/
        public static void shipdown(TextBlock msgTxt, string name)
        {
            string text = ("Wow! W to the O to the W. You've struck your first ship. Quite the accomplishment. Again, a blindfolded monkey could have done" +
                " the same.");
            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 4));
        }

        /* Text displayed after the first ship is shot down
         * 
     * @author Simon
         **/
        public static void normal(TextBlock msgTxt, string name)
        {
            string text = ("Well, you seemed to have figured it out. This time I'm truly leaving forever. Ring me when the robots have taken over.");
            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 4));
        }

    }
}
