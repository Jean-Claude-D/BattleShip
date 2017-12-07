using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BattleShip
{
    class Animations
    {
     
        public static void TypeWriter(string textToAnimate, TextBlock txt, TimeSpan timeSpan)
        {
            Storyboard story = new Storyboard();
            story.FillBehavior = FillBehavior.HoldEnd;

            StringAnimationUsingKeyFrames saukf = new StringAnimationUsingKeyFrames();
            saukf.Duration = new Duration(timeSpan);

            string tmp = string.Empty;
            foreach (char c in textToAnimate)
            {
                DiscreteStringKeyFrame dskf = new DiscreteStringKeyFrame();
                dskf.KeyTime = KeyTime.Paced;
                tmp += c;
                dskf.Value = tmp;
                saukf.KeyFrames.Add(dskf);
            }
            Storyboard.SetTargetName(saukf, txt.Name);
            Storyboard.SetTargetProperty(saukf, new PropertyPath(TextBlock.TextProperty));
            story.Children.Add(saukf);

            story.Begin(txt);
        }

       
        private void TypeWriter(string text, TextBlock txt)
        {
            for (int i = 0; i < text.Length; i++)
            {
                txt.Text = txt.Text + text[i];
                System.Threading.Thread.Sleep(30);
            }
        }
    }
}
