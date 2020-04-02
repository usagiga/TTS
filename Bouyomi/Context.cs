using System;

namespace Usagiga.TTS.Bouyomi
{
    /// <summary>
    /// Parameters used when executing voice through BouyomiChan.
    /// You must initialize them correctly, before using/calling.
    /// </summary>
    public class Context
    {
        private string word;
        private int volume = 100;
        private int speed = 100;
        private int pitch = 100;

        /// <summary>
        /// Recording word.
        /// </summary>
        public string Word
        {
            get => word ?? throw new Exception();
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception();
                word = value;
            }
        }

        /// <summary>
        /// Recording word.
        /// </summary>
        public int Volume
        {
            get => volume;
            set
            {
                var maxvol = 100;
                var minvol = 0;

                volume = Clamp(value, minvol, maxvol);
            }
        }

        /// <summary>
        /// Talking speed.
        /// </summary>
        public int Speed
        {
            get => speed;
            set
            {
                var minspd = 50;
                var maxspd = 200;

                speed = Clamp(value, minspd, maxspd);
            }
        }

        /// <summary>
        /// Voice pitch.
        /// </summary>
        public int Pitch
        {
            get => pitch;
            set
            {
                var minpit = 50;
                var maxpit = 200;

                pitch = Clamp(value, minpit, maxpit);
            }
        }

        /// <summary>
        /// Voice type.
        /// </summary>
        public VoiceType VoiceType { get; set; } = VoiceType.Female1;

        /// <summary>
        /// Return the number clamped by min/max.
        /// The method is alternative implementation of `Math.Clamp`.
        /// </summary>
        /// <returns>The number clamped by min/max.</returns>
        private static int Clamp(int value, int min, int max)
        {
            // Avoid to reversing input
            if (max < min) Clamp(value, max, min);

            // Clamp
            var result = value;
            if (value < min) result = min;
            if (max < value) result = max;
            return result;
        }
    }
}