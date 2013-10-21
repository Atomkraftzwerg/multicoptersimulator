using Microsoft.Ccr.Adapters.WinForms;
using Microsoft.Robotics.PhysicalModel;
using MulticopterSimulation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xna = Microsoft.Xna.Framework;

namespace MulticopterSimulation
{
    /// <summary>
    /// A singleton containing global accessible values and resources
    /// </summary>
    public class Globals
    {
        #region Globals singleton construct

        private static Globals instance;

        /// <summary>
        /// A handle to the global singleton instance
        /// </summary>
        public static Globals Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Globals();
                }
                return instance;
            }
        }

        /// <summary>
        /// Initializes everything
        /// </summary>
        private Globals()
        {
            Initialize();
        }

        #endregion

        #region Fields

        public static readonly Random RandomGenerator = new Random();
        public static WinFormsServicePort GlobalWinFormsServicePort;

        public readonly Dictionary<byte, string> multicopterDescriptions = new Dictionary<byte, string>();
        public readonly Dictionary<MulticopterType, string> multicopterDesignation = new Dictionary<MulticopterType, string>();
        public readonly Dictionary<MulticopterType, string> multicopterMeshFiles = new Dictionary<MulticopterType,string>();
        public readonly Dictionary<MulticopterType, byte> multicopterPropellerCount = new Dictionary<MulticopterType, byte>();

        public static string currentMulticopterType = "";
        public static string currentInclinationAngle = "";

        #endregion

        /// <summary>
        /// InitializePlot the global accessible fields
        /// </summary>
        private void Initialize()
        {
            multicopterDescriptions.Add(3, "Tricopter (3 propellers)");
            multicopterDescriptions.Add(4, "Quadrocopter (4 propellers)");
            multicopterDescriptions.Add(5, "Pentacopter (5 propellers)");
            multicopterDescriptions.Add(6, "Hexacopter (6 propellers)");
            multicopterDescriptions.Add(7, "Heptacopter (7 propellers)");
            multicopterDescriptions.Add(8, "Octocopter (8 propellers)");

            multicopterDesignation.Add(MulticopterType.TYPE_3_TRICOPTER, "Tricopter");
            multicopterDesignation.Add(MulticopterType.TYPE_4_QUADROCOPTER, "Quadrocopter");
            multicopterDesignation.Add(MulticopterType.TYPE_5_PENTACOPTER, "Pentacopter");
            multicopterDesignation.Add(MulticopterType.TYPE_6_HEXACOPTER, "Hexacopter");
            multicopterDesignation.Add(MulticopterType.TYPE_7_HEPTACOPTER, "Heptacopter");
            multicopterDesignation.Add(MulticopterType.TYPE_8_OCTOCOPTER, "Octocopter");

            multicopterPropellerCount.Add(MulticopterType.TYPE_3_TRICOPTER, 3);
            multicopterPropellerCount.Add(MulticopterType.TYPE_4_QUADROCOPTER, 4);
            multicopterPropellerCount.Add(MulticopterType.TYPE_5_PENTACOPTER, 5);
            multicopterPropellerCount.Add(MulticopterType.TYPE_6_HEXACOPTER, 6);
            multicopterPropellerCount.Add(MulticopterType.TYPE_7_HEPTACOPTER, 7);
            multicopterPropellerCount.Add(MulticopterType.TYPE_8_OCTOCOPTER, 8);

            multicopterMeshFiles.Add(MulticopterType.TYPE_3_TRICOPTER, "MulticopterSimulation/tetracopter_base.obj");
            multicopterMeshFiles.Add(MulticopterType.TYPE_4_QUADROCOPTER, "MulticopterSimulation/quadrocopter_base.obj");
            multicopterMeshFiles.Add(MulticopterType.TYPE_5_PENTACOPTER, "MulticopterSimulation/pentacopter_base.obj");
            multicopterMeshFiles.Add(MulticopterType.TYPE_6_HEXACOPTER, "MulticopterSimulation/hexacopter_base.obj");
            multicopterMeshFiles.Add(MulticopterType.TYPE_7_HEPTACOPTER, "MulticopterSimulation/heptacopter_base.obj");
            multicopterMeshFiles.Add(MulticopterType.TYPE_8_OCTOCOPTER, "MulticopterSimulation/octocopter_base.obj");
        }
    }
}
