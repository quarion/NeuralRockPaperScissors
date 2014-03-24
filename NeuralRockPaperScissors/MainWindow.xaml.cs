using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using RockPaperScissorsCore;

namespace NeuralRockPaperScisors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //decisionMaker = new RandomDecisionMaker();
            decisionMaker = new TDNNDecisionMaker();
            randomTimer.Interval = new TimeSpan(2000000);
        }

        IDecisionMaker decisionMaker;

        DispatcherTimer randomTimer = new DispatcherTimer();
        IDecisionMaker randomDecisionMaker = new RandomDecisionMaker();

        int playerScore = 0;
        int opponentScore = 0;

        int patternIndex = 0;

        //paper
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            scoreRound(Decision.Paper);
        }

        //rock
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            scoreRound(Decision.Rock);
        }

        //scissors
        private void ScissorsButton_Click(object sender, RoutedEventArgs e)
        {
            scoreRound(Decision.Scissor);
        }

        //obsluga klawiatury
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.A)
                RockButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.S)
                PaperButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D)
                ScissorsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }


        public void scoreRound(Decision playerDecision)
        {
            var opponentDecision = decisionMaker.getNextDecision();
            int score = playerDecision.Score(opponentDecision);

            if (score == -1)
                opponentScore += 1;
            else if (score == 1)
                playerScore += 1;

            ScoreLabel.Content = playerScore + " : " + opponentScore;

            HistoryTextBox.Text = "Ty: " + playerDecision.toLocalString() + ", przeciwnik: " + opponentDecision.toLocalString();

            decisionMaker.rememberDecision(playerDecision);
        }

        private void AutoButton_Click(object sender, RoutedEventArgs e)
        {
            if (randomTimer.IsEnabled)
            {
                randomTimer.Stop();
                patternButton.Content = "Wzorzec";
                AutoButton.Content = "Losowo";
            }
            else
            {
                randomTimer.Start();
                randomTimer.Tick -= patternTimerTick;
                randomTimer.Tick += randomTimerTick;
                AutoButton.Content = "Stop";
                patternButton.Content = "Stop";
            }
        }

        private void patternButton_Click(object sender, RoutedEventArgs e)
        {
            patternIndex = 0;
            if (randomTimer.IsEnabled)
            {
                randomTimer.Stop();
                patternButton.Content = "Wzorzec";
                AutoButton.Content = "Losowo";
            }
            else
            {
                randomTimer.Start();
                randomTimer.Tick += patternTimerTick;
                randomTimer.Tick -= randomTimerTick;
                AutoButton.Content = "Stop";
                patternButton.Content = "Stop";
            }
        }

        private void randomTimerTick(object sender, EventArgs e)
        {
            scoreRound(randomDecisionMaker.getNextDecision());
        }

        private void patternTimerTick(object sender, EventArgs e)
        {
            Decision[] pattern = new Decision[7] { Decision.Rock, Decision.Paper, Decision.Rock, Decision.Scissor, Decision.Paper, Decision.Paper, Decision.Scissor };
            scoreRound(pattern[patternIndex]);
            patternIndex = (patternIndex + 1) % pattern.Length;
        }

    }
}
