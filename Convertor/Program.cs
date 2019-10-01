using Convertor.Lib;
using Convertor.Lib.GCode.Command;
using Convertor.Lib.GCode.Command.G;
using Convertor.Lib.GCode.Command.M;
using Convertor.Model;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Convertor
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filename = "20x20x20.sl1";
            FileInfo file = new FileInfo(filename);

            var p = new Program();
            
            var configuration = p.Extract(filename);

            GCodeWriter gCodeWriter = new GCodeWriter(configuration.config);
            gCodeWriter.WriteHeader();
            gCodeWriter.StartGCode();

            var layerHeight = (decimal)gCodeWriter.PrusaPrintConfig.LayerHeight;
            decimal currentLayerHeight = 0.0m;
            decimal currentLayer = 1m;

            foreach (var image in configuration.images)
            {
                decimal zlevel = (currentLayer * layerHeight);
                gCodeWriter.Write();

                gCodeWriter.Write(new Comment() { Value = $"LAYER_START:{currentLayer-1}" });
                gCodeWriter.Write(new Comment() { Value = $"currPos:{zlevel}" });
                gCodeWriter.Write(new M6054() { Value = image.Name });
                gCodeWriter.Write(new G0() {  Z = 5.0m + zlevel, F = 65  });
                gCodeWriter.Write(new G0() {  Z = zlevel, F = 150  });
                gCodeWriter.Write(new G4() { Value = 0 });
                gCodeWriter.Write(new M106() { Value = 255 });
                gCodeWriter.Write(new G4() { Value = (currentLayer <= gCodeWriter.PrusaPrintConfig.NumFade ? gCodeWriter.PrusaPrintConfig.ExpTimeFirst * 1000 : gCodeWriter.PrusaPrintConfig.ExpTime*1000) });
                gCodeWriter.Write(new M106() { Value = 0 });
                gCodeWriter.Write();
                gCodeWriter.Write(new Comment() { Value = "LAYER_END" });
                gCodeWriter.Write();

                currentLayerHeight += layerHeight;
                currentLayer++;

            }
            var gcode = gCodeWriter.ToString();

            var data = p.RebuildZipFile(gcode, configuration.images);
            var fileName = file.Name + ".zip";

            File.WriteAllBytes(fileName, data);
        }


        private byte[] RebuildZipFile(string gcode, List<PrusaFile> files)
        {
            MemoryStream outputMemStream = new MemoryStream();
            ZipOutputStream zipStream = new ZipOutputStream(outputMemStream);

            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

            var newEntry = new ZipEntry("run.gcode");
            newEntry.DateTime = DateTime.Now;

            zipStream.PutNextEntry(newEntry);
            var gcodeBytes = Encoding.Default.GetBytes(gcode);
            var bytes = gcodeBytes;
            using (MemoryStream inStream = new MemoryStream(bytes))
            {
                StreamUtils.Copy(inStream, zipStream, new byte[4096]);
                inStream.Close();
                zipStream.CloseEntry();
            }



            foreach (var file in files)
            {
                newEntry = new ZipEntry(file.Name);
                newEntry.DateTime = DateTime.Now;

                zipStream.PutNextEntry(newEntry);

                bytes = file.Data;

                using (MemoryStream inStream = new MemoryStream(bytes))
                {
                    StreamUtils.Copy(inStream, zipStream, new byte[4096]);
                    zipStream.CloseEntry();
                }

            }

            zipStream.IsStreamOwner = false;    // False stops the Close also Closing the underlying stream.
            zipStream.Close();          // Must finish the ZipOutputStream before using outputMemStream.

            outputMemStream.Position = 0;

            return outputMemStream.ToArray();
        }




        public (PrusaPrintConfig config, List<PrusaFile> images) Extract(string archivePath)
        {
            IEnumerable<PrusaFile> files;
            using (var outputStream = new MemoryStream())
            // To output to a disk file, replace the above with
            //using(var outputStream = File.Create(newZipFileName);
            using (var zipOutStream = new ZipOutputStream(outputStream))
            {

                // Stops the Close also Closing the underlying stream.
                zipOutStream.IsStreamOwner = false;

                zipOutStream.SetLevel(3);

                // Optionally set password
                //zipOutStream.Password = password;

                using (var inStream = File.OpenRead(archivePath))
                {
                    files = RecursiveExtractRebuild(inStream).ToList();
                }
                var configurationFile = files.FirstOrDefault(x => x.Name == "config.ini");

                string config_ini = System.Text.Encoding.UTF8.GetString(configurationFile.Data);
                var config = new IniParser(config_ini).Parse<PrusaPrintConfig>();

                return (config, files.Where(x=> !x.Name.EndsWith("ini")).ToList());
            }
        }


        // Recursively extract embedded zip files
        private IEnumerable<PrusaFile> RecursiveExtractRebuild(Stream inStream)
        {
            var buffer = new byte[4096];
            using (var zipFile = new ZipFile(inStream, leaveOpen: true))
            {
                foreach (ZipEntry zipEntry in zipFile)
                {
                    if (zipEntry == null || !zipEntry.IsFile)
                        continue;

                    // To omit folder use: 
                    //var entryFileName = Path.GetFileName(zipEntry.Name) 
                    var entryFileName = zipEntry.Name;

                    // Specify any other filtering here.

                    using (var zipStream = zipFile.GetInputStream(zipEntry))
                    {
                        // Extract Zips-within-zips
                        if (entryFileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                        {
                            //RecursiveExtractRebuild(outZipStream, zipStream);
                            continue;
                        }
                        else
                        {
                            var ms = new MemoryStream();
                            StreamUtils.Copy(zipStream, ms, buffer);
                            var data = ms.ToArray();
                            yield return new PrusaFile()
                            {
                                Name = entryFileName,
                                Data = data

                            };
                        }
                    }
                }
            }
        }

    }
}
