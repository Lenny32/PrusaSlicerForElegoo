using System;

namespace Convertor.Model
{
    /*
     
action = print
jobDir = 20x20x20
expTime = 8
expTimeFirst = 65
fileCreationTimestamp = 2019-09-30 at 11:30:33 UTC
layerHeight = 0.05
materialName = Elegoo White
numFade = 10
numFast = 520
numSlow = 0
printProfile = 0.05 Normal
printTime = 7210.818182
printerModel = SL1
printerProfile = Elegoo Mars
printerVariant = default
prusaSlicerVersion = PrusaSlicer-2.1.0+win64-201909160915
usedMaterial = 8.961532
         */
    public class PrusaPrintConfig
    {
        public string Action { get; set; }
        public string JobDir { get; set; }
        public int ExpTime { get; set; }
        public int ExpTimeFirst { get; set; }
        public string FileCreationTimestamp { get; set; }
        public double LayerHeight { get; set; }
        public string MaterialName { get; set; }
        public int NumFade { get; set; }
        public int NumFast { get; set; }
        public int NumSlow { get; set; }
        public string PrintProfile { get; set; }
        public double PrintTime { get; set; }
        public string PrinterModel { get; set; }
        public string PrinterProfile { get; set; }
        public string PrinterVariant { get; set; }
        public string PrusaSlicerVersion { get; set; }
        public double UsedMaterial { get; set; }
    }
}
