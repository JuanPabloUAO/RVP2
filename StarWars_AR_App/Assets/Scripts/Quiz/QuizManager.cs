using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text progressText;

    public MovieQuizData[] allQuizzes;

    private Question[] questions;
    private int currentQuestionIndex = 0;

    private MovieID currentMovie;

    private int correctAnswers = 0;
    private float timer = 0f;
    private bool quizRunning = false;

    private Question currentQuestion;

    void Update()
    {
        if (quizRunning)
            timer += Time.deltaTime;
    }

    void Start()
    {
        int movieIndex = PlayerPrefs.GetInt("SelectedMovie", 0);
        currentMovie = (MovieID)movieIndex;

        StartQuizByMovie(currentMovie);
    }

    public void StartQuizByMovie(MovieID movie)
    {
        MovieQuizData data = GetQuiz(movie);

        if (data == null)
        {
            Debug.LogError("No se encontró quiz para: " + movie);
            return;
        }

        questions = data.questions;

        currentQuestionIndex = 0;
        correctAnswers = 0;
        timer = 0f;
        quizRunning = true;

        ShowQuestion();
    }

    MovieQuizData GetQuiz(MovieID id)
    {
        foreach (var quiz in allQuizzes)
        {
            if (quiz.movieID == id)
                return quiz;
        }
        return null;
    }

    void ShowQuestion()
    {
        currentQuestion = questions[currentQuestionIndex];

        questionText.text = currentQuestion.questionText;
        progressText.text = (currentQuestionIndex + 1) + "/10";

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;

            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => Answer(index));
        }
    }

    void Answer(int index)
    {
        if (currentQuestionIndex >= questions.Length)
            return;

        // ✔ usamos tu estructura real
        if (index == currentQuestion.correctIndex)
        {
            correctAnswers++;
        }

        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            FinishQuiz();
        }
    }

    void FinishQuiz()
    {
        quizRunning = false;

        int finalTime = Mathf.RoundToInt(timer);

        int finalScore = (correctAnswers * 1000) - finalTime;

        Debug.Log("Correctas: " + correctAnswers);
        Debug.Log("Tiempo: " + finalTime);
        Debug.Log("ScoreFinal: " + finalScore);

        SaveProgress(correctAnswers, finalTime, finalScore);

        questionText.text = "Resultado: " + correctAnswers + "/10";

        foreach (var btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        Invoke("GoToMenu", 3f);
    }

    void SaveProgress(int correct, int time, int score)
    {
        int movieIndex = PlayerPrefs.GetInt("SelectedMovie", 0);

        PlayerPrefs.SetInt("Movie_" + movieIndex, 1);
        PlayerPrefs.SetInt("Score_" + movieIndex, correct);
        PlayerPrefs.SetInt("FinalScore_" + movieIndex, score);

        PlayerPrefs.Save();
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}