using System;

namespace Usagiga.TTS.Softalk
{
    /// <summary>
    /// Parameters used when executing voice through Softalk.
    /// You must initialize them correctly, before using/calling.
    /// </summary>
    public class Context
    {
        private string word;
        private int volume = 100;
        private int speed = 100;
        private int pitch = 100;
        private int voiceType = 0;

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
            get
            {
                var minspd = VoiceEngineType == VoiceEngineType.AquesTalk10 ? 50 : 1;
                var maxspd = 300;

                return Clamp(speed, minspd, maxspd);
            }
            set
            {
                var minspd = VoiceEngineType == VoiceEngineType.AquesTalk10 ? 50 : 1;
                var maxspd = 300;

                speed = Clamp(value, minspd, maxspd);
            }
        }

        /// <summary>
        /// Voice pitch.
        /// </summary>
        public int Pitch
        {
            get
            {
                var maxpit = VoiceEngineType == VoiceEngineType.AquesTalk10 ? 200 : 300;
                var minpit = 0;

                return Clamp(pitch, minpit, maxpit);
            }
            set
            {
                var maxpit = VoiceEngineType == VoiceEngineType.AquesTalk10 ? 200 : 300;
                var minpit = 0;

                pitch = Clamp(value, minpit, maxpit);
            }
        }

        /// <summary>
        /// Voice-engine type.
        /// </summary>
        public VoiceEngineType VoiceEngineType { get; set; } = VoiceEngineType.AquesTalk;

        /// <summary>
        /// Voice type.
        /// The value is different depending on `VoiceEngineType`
        /// or your own Softalk setting.
        /// </summary>
        public int VoiceType
        {
            get => voiceType;
            set
            {
                if (0 > value) throw new Exception();
                voiceType = value;
            }
        }

        /// <summary>
        /// Return the number clamped by min/max.
        /// The method is alternative implementation of `Math.Clamp`
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
