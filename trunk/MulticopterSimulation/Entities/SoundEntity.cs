using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace MulticopterSimulation.Entities
{
    /// <summary>
    /// An entity playing a motor sound of the multicopter
    /// </summary>
    class SoundEntity
    {
        #region Constants

        // The sound file containing the engine sound
        const string ENGINE_SOUND_PATH = @"store\media\MulticopterSimulation\quadrocopter.wav";

        #endregion

        #region Fields

        FileStream fs;                              // The file stream containing the sound data
        SoundEffect engineSound;                    // Sound effect to be loaded
        SoundEffectInstance engineSoundInstance;    // Creates an instance for the sound effect
        AudioEmitter audioEmitter;                  // Creates a sound emitter for 3D sound
        AudioListener audioListener;                // Creates a listener for the 3D sound

        #endregion

        #region Properties

        /// <summary>
        /// The sound pitch to be adjusted by the motor thrust
        /// </summary>
        public float Pitch
        {
            get { return engineSoundInstance.Pitch - 0.5f; }
            set { engineSoundInstance.Pitch = value - 0.5f; }
        }

        /// <summary>
        /// The sound volume to be adjusted by the motor thrust
        /// </summary>
        public float Volume
        {
            get { return engineSoundInstance.Volume; }
            set { engineSoundInstance.Volume = value; }
        }

        /// <summary>
        /// The sound emitter position to be adjusted to the current multicopter position
        /// </summary>
        public Vector3 AudioEmitterPosition
        {
            get 
            {
                return audioEmitter.Position; 
            }
            set 
            {
                audioEmitter.Position = value;
            }
        }

        /// <summary>
        /// The sound listener position to be adjusted to the current camera position
        /// </summary>
        public Vector3 AudioListenerPosition
        {
            get { return audioListener.Position; }
            set { audioListener.Position = value; }
        }

        /// <summary>
        /// The facing of the sound listener to be adjusted to the current camera facing
        /// </summary>
        //public Vector3 AudioListenerLookAt
        //{
        //    get { return audioListener.Forward; }
        //    set { audioListener.Forward = value; }
        //}

        #endregion

        /// <summary>
        /// Creates a new sound entity
        /// </summary>
        public SoundEntity()
        {
            // Open sound file
            fs = File.OpenRead(ENGINE_SOUND_PATH);
            engineSound = SoundEffect.FromStream(fs);
            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
            fs.Close();

            // Create and adjust a sound instance
            engineSoundInstance = engineSound.CreateInstance();
            engineSoundInstance.Volume = 0.0f;
            engineSoundInstance.Pitch = 0.0f;

            // Creates audio emitter and listener
            audioEmitter = new AudioEmitter();
            audioListener = new AudioListener();
            //audioListener.Up = new Vector3(0, 1, 0);
        }

        /// <summary>
        /// Start the sound playback
        /// </summary>
        public void PlaySound()
        {
            if (engineSoundInstance.State == SoundState.Stopped)
            {
                engineSoundInstance.Apply3D(audioListener, audioEmitter);
                engineSoundInstance.IsLooped = true; // Repeat the sound
                engineSoundInstance.Play();
            }
            else
                engineSoundInstance.Resume();
        }

        /// <summary>
        /// If needed, a possibility to pause the sound playback
        /// </summary>
        public void PauseSound()
        {
            if (engineSoundInstance.State == SoundState.Playing)
                engineSoundInstance.Pause();
        }

        /// <summary>
        /// If needed, a possibility to stop the sound playback
        /// </summary>
        public void StopSound()
        {
            if (engineSoundInstance.State != SoundState.Stopped)
                engineSoundInstance.Stop();
        }

        /// <summary>
        /// Update the the listener and the emitter to apply the 3D sound effect
        /// </summary>
        public void Update3DSound()
        {
            engineSoundInstance.Apply3D(audioListener, audioEmitter);
        }
    }
}
