using Convertor.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ParserTests
    {
        class DummyIniClass
        {
            public string StringTest { get; set; }
            public int Bar { get; set; }
            public bool BoolTest { get; set; }
            public double DoubleTest { get; set; }
        }

        [TestMethod]
        public void Quick_IniParser_Test()
        {
            var iniFile = @"
stringtest = testtesttest 123
bar = 1
booltest=true
doubletest=8.961532
";
            var parser = new Convertor.Lib.IniParser(iniFile);
            var test = parser.Parse<DummyIniClass>();
            Assert.AreEqual(test.StringTest, "testtesttest 123");
            Assert.AreEqual(test.Bar, 1);
            Assert.AreEqual(test.BoolTest, true);
            Assert.AreEqual(test.DoubleTest, 8.961532);


        }

        [TestMethod]
        public void Realistic_IniParser_Test()
        {
            var iniFile = @"
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
";
            var parser = new Convertor.Lib.IniParser(iniFile);
            var test = parser.Parse<PrusaPrintConfig>();
            Assert.AreEqual(test.Action, "print");
            Assert.AreEqual(test.JobDir, "20x20x20");
            Assert.AreEqual(test.ExpTime, 8);
            Assert.AreEqual(test.ExpTimeFirst, 65);
            Assert.AreEqual(test.FileCreationTimestamp, "2019-09-30 at 11:30:33 UTC");
            Assert.AreEqual(test.LayerHeight, 0.05);
            Assert.AreEqual(test.MaterialName, "Elegoo White");
            Assert.AreEqual(test.NumFade, 10);
            Assert.AreEqual(test.NumFast, 520);
            Assert.AreEqual(test.NumSlow, 0);
            Assert.AreEqual(test.PrintProfile, "0.05 Normal");
            Assert.AreEqual(test.PrintTime, 7210.818182);
            Assert.AreEqual(test.PrinterModel, "SL1");
            Assert.AreEqual(test.PrinterProfile, "Elegoo Mars");
            Assert.AreEqual(test.PrinterVariant, "default");
            Assert.AreEqual(test.PrusaSlicerVersion, "PrusaSlicer-2.1.0+win64-201909160915");
            Assert.AreEqual(test.UsedMaterial, 8.961532);


        }

    }
}
