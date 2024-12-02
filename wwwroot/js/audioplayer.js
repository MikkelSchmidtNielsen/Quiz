const audioHarryPotterTheme = new Audio('audio/HedwigTheme.mp3'); // Sørg for at filstien er korrekt
const audioWelcomeHogwartsQuiz = new Audio('audio/WelcomeHogwartsQuiz.mp3');

let isPlaying = false;

function playMusic(soundFile) {
    const audio = new Audio(soundFile);
    audio.play();
};

window.playWelcomeHogwartsQuiz = () => {
    console.log("Quiz called");
    if (!isPlaying) {
        audioWelcomeHogwartsQuiz.play();
        isPlaying = true;
    }
};

window.soundFadeOut = () => {
    if (!isPlaying) {
        audioWelcomeHogwartsQuiz.play();
        isPlaying = true;
    }

    let fadeOutInterval = setInterval(() => {
        if (audioWelcomeHogwartsQuiz.volume > 0.10) {
            audioWelcomeHogwartsQuiz.volume -= 0.10;
        }
        else {
            clearInterval(fadeOutInterval);
            audioWelcomeHogwartsQuiz.pause();
            audioWelcomeHogwartsQuiz.currentTime = 0;
            isPlaying = false;
        }
    }, 200);
};