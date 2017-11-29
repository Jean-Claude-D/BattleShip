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
            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 5));
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
            Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 5));
        }
		/**
		 * Text displayed after a player enters their name if they're new.
		 * 
		 * @author karina
		 **/
		public static void newName(TextBlock msgTxt, string name)
		{
			string text = "Hi" + name + "\n"
				+ "something something new person?";
			Animations.TypeWriter(text, msgTxt, new TimeSpan(0, 0, 5));
		}
	}
}
