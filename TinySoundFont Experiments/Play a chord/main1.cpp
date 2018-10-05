#include <iostream>
#define TSF_IMPLEMENTATION
#include "tsf.h"

extern "C"{
#include "minisdl_audio.h"
}

// This is the actual synthesizer object. (pointer to the synth actually)
static tsf* TinySoundFont;

// Callback function called by the audio thread
static void AudioCallback(void* data, Uint8 *stream, int len)
{
    // Note we don't do any thread concurrency control here because in this
    // example all notes are started before the audio playback begins.
    // If you do play notes while the audio thread renders output you
    // will need a mutex of some sort.
    int SampleCount = (len / (2 * sizeof(short))); //2 output channels
    tsf_render_short(TinySoundFont, (short*)stream, SampleCount, 0);
}

int main(int argc, char *argv[])
{
    // Audio output format.
	// DON'T touch until we figure out what these are.
    SDL_AudioSpec OutputAudioSpec;
    OutputAudioSpec.freq = 44100;
    OutputAudioSpec.format = AUDIO_S16;
    OutputAudioSpec.channels = 2;
    OutputAudioSpec.samples = 4096;
    OutputAudioSpec.callback = AudioCallback;

    // Initialize the audio system
    if (SDL_AudioInit(NULL) < 0)
    {
		std::cout << "Could not initialize audio hardware or driver" << std::endl;
		std::cin.get();
        return 1;
    }

    //load soundfont
    TinySoundFont = tsf_load_filename("..\\..\\soundfonts\\strings.SF2"); //REPLACE THIS PATH WITH THE PATH TO THE SOUNDFONT FILE
    if (!TinySoundFont)
    {
		std::cout << "Could not load soundfont" << std::endl;
		std::cin.get();
        return 1;
    }

    // Set the rendering output mode to 44.1khz and 0 decibel gain
    tsf_set_output(TinySoundFont, TSF_STEREO_INTERLEAVED, OutputAudioSpec.freq, 0);

    // Start notes before starting the audio playback
    tsf_note_on(TinySoundFont, 0, 38, 1.0f); //D
    tsf_note_on(TinySoundFont, 0, 50, 1.0f); //D2
    tsf_note_on(TinySoundFont, 0, 53, 1.0f); //f2
    tsf_note_on(TinySoundFont, 0, 57, 1.0f); //A2
    tsf_note_on(TinySoundFont, 0, 60, 1.0f); //C3
    tsf_note_on(TinySoundFont, 0, 74, 1.0f); //D4

    // Request the desired audio output format
    if (SDL_OpenAudio(&OutputAudioSpec, NULL) < 0)
    {
		std::cout << "Could not open the audio hardware or the desired audio output format" << std::endl;
		std::cin.get();
        return 1;
    }

    std::cout << "Press enter to start." << std::endl;
    std::cin.get();

    // Start the actual audio playback here
    // The audio thread will begin to call our AudioCallback function
    SDL_PauseAudio(0);

    // Let the audio callback play some sound for 5 seconds
    SDL_Delay(5000);

    // We should call tsf_close(g_TinySoundFont) and SDL_DestroyMutex(g_Mutex)
    // here to free the memory and resources but we just let the OS clean up
    // because the process ends here.
    return 0;
}
