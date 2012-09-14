using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsCore
{
    class RPSNetwork:OneLayerNetwork
    {
        /// <summary>
        /// Klasa realizująca sieć przewidującą kolejne ruchy w grze, na podstawie jednowarstwowej sieci meuronowej
        /// Argumentem jest ilość zapamietanych ruchów. Neurony wejsciowe są pobudzane zapamiętanymi ruchami z przeszłosci - zarówno własnymi, jak i przeciwnika.
        /// Potrzebna ilość neuronów wejściowych wynosi: (ilość zapamietanych ruchów) * (ilość graczy) * (ilość możliwych ruchów) = (ilość zapamietanych ruchów) * 2 * 3
        /// </summary>
        /// <param name="memoryLength">ilość zapamietanych ruchó</param>
        public RPSNetwork(int memoryLength):base(memoryLength * 6, 3)
        {
            Random rnd = new Random();
            //inicjalizujemy wagi, standardowo
            resetWeights(10.0);

            //generujemy pierwotne wejscie, losowo
            for (int i = 0; i < inputs.Length; i += 3)
            {
                Decision d = (Decision)(rnd.Next(3) + 1);
                for(int j = 0; j < 3; ++j)
                    inputs[i + j] = d.ToNeural()[j];
            }
        }

        double learningRate = Settigns.learningRate;
        double learnigEnstinguishRate = Settigns.learnigEnstinguishRate;
        double minLearningRate = Settigns.minLearningRate;

        /// <summary>
        /// Zwraca decyzję na podstawie pobudzeń neuronó wyjściowych
        /// </summary>
        /// <returns></returns>
        public Decision getDecision()
        {
            updateOutput();
            double bestValue = outputs.Max();
            int maxIndex = Array.IndexOf(outputs, bestValue); //znajdujemy najbardziej pobudzony neuron
            return (Decision)( (maxIndex + 1) % 4 );
        }

        /// <summary>
        /// Funkcja odpowiedzialna za proces nauki
        /// </summary>
        /// <param name="AIDecision">decyzja podjeta przez siec</param>
        /// <param name="opponentDecision">decyzja przeciwnika</param>
        public void updateWeights(Decision AIDecision, Decision opponentDecision)
        {
            Decision expectedDecision = opponentDecision.GetCounter(); //decyzja, ktora powinnismy podjac
            double[] desiredOutput = expectedDecision.ToNeural();
            double[] currentOutput = AIDecision.ToNeural();

            for (int i = 0; i < outputs.Length; ++i)
            {
                for (int j = 0; j < inputs.Length; ++j)
                    weights[i, j] += (desiredOutput[i] - currentOutput[i]) * inputs[j] * learningRate;
            }

            if (minLearningRate < learningRate - learnigEnstinguishRate)
                learningRate -= learnigEnstinguishRate;

        }

        /// <summary>
        /// Aktualizuje wejscia po kolejnej rundzie gry
        /// </summary>
        /// <param name="AIDecision">własna decyzja</param>
        /// <param name="opponentDecision">decyzja przeciwnika</param>
        public void updateInputs(Decision AIDecision, Decision opponentDecision )
        {
            /*for (int j = 6; j < inputs.Length; ++j)
                inputs[j] = inputs[j - 6]; //przesuwamy wszystko, co zapamietalismy
            
            //zapamietujemy nowe
            for (int j = 0; j < 3; ++j)
            {
                inputs[j] = AIDecision.ToNeural()[j];
                inputs[j + 3] = opponentDecision.ToNeural()[j];
            }*/

            for (int j = 3; j < inputs.Length; ++j)
                inputs[j] = inputs[j - 3];

            for (int j = 0; j < 3; ++j)
            {
                inputs[j] = AIDecision.ToNeural()[j];
                inputs[j + inputs.Length / 2] = opponentDecision.ToNeural()[j];
            }

        }

    }
}
