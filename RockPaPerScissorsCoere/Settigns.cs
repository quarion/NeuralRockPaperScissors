using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsCore
{
    class Settigns
    {
        /// <summary>
        /// ilość zapamietanych ruchów
        /// </summary>
        public static int samplesHistoryLength = 5;

        /// <summary>
        /// Współczynnik szybkosci uczenia
        /// </summary>
        public static double learningRate = 1.0;

        /// <summary>
        /// Współczynnik zmniejszenia szybkosci uczenia w czasie
        /// </summary>
        public static double learnigEnstinguishRate = 0.0;

        /// <summary>
        /// Minimalna szybkość uczenia
        /// </summary>
        public static double minLearningRate = 1.0;
    }
}
