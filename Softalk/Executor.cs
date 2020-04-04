using System;
using System.Diagnostics;
using System.IO;

namespace Usagiga.TTS.Softalk
{
    public class Executor : IDisposable
    {
        /// <summary>
        /// Create `Executor`.
        /// Prepare connection to BouyomiChan at the same time.
        /// </summary>
        /// <param name="softalkPath"></param>
        public Executor(string softalkPath)
        {
            if (!File.Exists(softalkPath)) throw new FileNotFoundException();

            SoftalkPath = softalkPath;
            Run("/X:1");
        }

        /// <summary>
        /// Dispose `Executor` safely.
        /// </summary>
        public void Dispose()
        {
            Run("/close_now");
        }

        /// <summary>
        /// File path of `softalk.exe`
        /// </summary>
        public string SoftalkPath { get; }

        /// <summary>
        /// Play specific voice through Softalk.
        /// </summary>
        /// <param name="ctx">Parameters of voice.</param>
        public void Play(Context ctx)
        {
            var args = $"/T:{ctx.VoiceEngineType} /U:{ctx.VoiceType} /V:{ctx.Volume} /S:{ctx.Speed} /O:{ctx.Pitch} /Q:4 /W:{ctx.Word}";
            Run(args);
        }

        /// <summary>
        /// Record specific voice through Softalk.
        /// </summary>
        /// <param name="ctx">Parameters of voice.</param>
        /// <param name="dest">Exporting path.</param>
        public void Record(Context ctx, string dest)
        {
            var baseDir = Path.GetDirectoryName(dest);
            var args = $"/T:{ctx.VoiceEngineType} /U:{ctx.VoiceType} /V:{ctx.Volume} /S:{ctx.Speed} /O:{ctx.Pitch} /Q:4 /R:\"{dest}\" /W:{ctx.Word}";

            // Create specific dir if the dir doesn't exist.
            Directory.CreateDirectory(baseDir);

            var proc = Run(args);
            proc.WaitForExit();
        }

        /// <summary>
        /// Run Softalk with specific args.
        /// </summary>
        /// <param name="args">Softalk command-line argments.</param>
        /// <returns></returns>
        private Process Run(string args) => Process.Start(SoftalkPath, args);
    }
}
