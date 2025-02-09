using System;
using System.Windows.Forms;
using System.Timers;

public class IQTestForm : Form
{
    private int score = 0;
    private int incorrectAnswers = 0;
    private int currentQuestion = 0;
    private Label questionLabel;
    private TextBox answerTextBox;
    private Button nextButton;
    private Label timerLabel;
    private System.Timers.Timer questionTimer;
    private int timeLeft;

    private Panel startPanel;
    private Panel endPanel;
    private Button startButton;
    private Button playAgainButton;
    private Button closeButton;
    private Label resultLabel;
    private Label scoreLabel;

    private string[] questions = {
        "What is 2 + 2?",
        "What is 5 + 5?",
        "What is the square root of 64?",
        "What is 3 x 20?",
        "What is 5 x 10?",
        "What is 12 / 4?",
        "What is 15 - 7?",
        "What is 9 + 6?",
        "What is 8 x 7?",
        "What is 100 - 45?",
        "What is 7 x 7?",
        "What is 81 / 9?",
        "What is 14 + 28?",
        "What is 6 x 6?",
        "What is 49 - 7?",
        "What is 25 + 25?",
        "What is 36 / 6?",
        "What is 11 x 11?",
        "What is 64 - 32?",
        "What is 18 + 22?"
    };

    private string[] answers = { "4", "10", "8", "60", "50", "3", "8", "15", "56", "55", "49", "9", "42", "36", "42", "50", "6", "121", "32", "40" };

    public IQTestForm()
    {
        this.Text = "IQ Test";
        this.Size = new System.Drawing.Size(400, 250);

        InitializeComponents();
        ShowStartMenu();
    }

    private void InitializeComponents()
    {
        // Initialize start menu components
        startPanel = new Panel() { Dock = DockStyle.Fill };
        startButton = new Button() { Text = "Start Test", Left = 150, Top = 100, Width = 100 };
        startButton.Click += new EventHandler(StartButton_Click);
        startPanel.Controls.Add(startButton);

        // Initialize end menu components
        endPanel = new Panel() { Dock = DockStyle.Fill };
        resultLabel = new Label() { Left = 10, Top = 20, Width = 360 };
        scoreLabel = new Label() { Left = 10, Top = 50, Width = 360 };
        playAgainButton = new Button() { Text = "Play Again", Left = 50, Top = 100, Width = 100 };
        playAgainButton.Click += new EventHandler(PlayAgainButton_Click);
        closeButton = new Button() { Text = "Close App", Left = 200, Top = 100, Width = 100 };
        closeButton.Click += new EventHandler(CloseButton_Click);
        endPanel.Controls.Add(resultLabel);
        endPanel.Controls.Add(scoreLabel);
        endPanel.Controls.Add(playAgainButton);
        endPanel.Controls.Add(closeButton);

        // Initialize question components
        questionLabel = new Label() { Left = 10, Top = 20, Width = 360 };
        answerTextBox = new TextBox() { Left = 10, Top = 50, Width = 360 };
        nextButton = new Button() { Text = "Next", Left = 10, Top = 80, Width = 360 };
        nextButton.Click += new EventHandler(NextButton_Click);
        timerLabel = new Label() { Left = 10, Top = 110, Width = 360 };

        this.Controls.Add(questionLabel);
        this.Controls.Add(answerTextBox);
        this.Controls.Add(nextButton);
        this.Controls.Add(timerLabel);

        answerTextBox.KeyDown += new KeyEventHandler(AnswerTextBox_KeyDown);

        questionTimer = new System.Timers.Timer(1000); // 1 second intervals
        questionTimer.Elapsed += OnTimedEvent;
        questionTimer.AutoReset = true;
    }

    private void ShowStartMenu()
    {
        this.Controls.Clear();
        this.Controls.Add(startPanel);
    }

    private void ShowEndMenu(int iq)
    {
        resultLabel.Text = "Your IQ is: " + iq;
        scoreLabel.Text = "You got " + score + " out of " + questions.Length + " questions right.";
        this.Controls.Clear();
        this.Controls.Add(endPanel);
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        this.Controls.Clear();
        this.Controls.Add(questionLabel);
        this.Controls.Add(answerTextBox);
        this.Controls.Add(nextButton);
        this.Controls.Add(timerLabel);
        StartTest();
    }

    private void PlayAgainButton_Click(object sender, EventArgs e)
    {
        score = 0;
        incorrectAnswers = 0;
        currentQuestion = 0;
        ShowStartMenu();
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void StartTest()
    {
        ShowQuestion();
    }

    private void ShowQuestion()
    {
        if (currentQuestion < questions.Length)
        {
            questionLabel.Text = questions[currentQuestion];
            answerTextBox.Text = "";
            timeLeft = 30;
            timerLabel.Text = "Time left: " + timeLeft + " seconds";
            questionTimer.Start();
        }
        else
        {
            int iq = CalculateIQ(score, incorrectAnswers);
            ShowEndMenu(iq);
        }
    }

    private void NextButton_Click(object sender, EventArgs e)
    {
        questionTimer.Stop();
        CheckAnswer();
        currentQuestion++;
        ShowQuestion();
    }

    private void AnswerTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true; // Prevent the beep sound
            NextButton_Click(sender, e);
        }
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        this.Invoke((MethodInvoker)delegate {
            timeLeft--;
            timerLabel.Text = "Time left: " + timeLeft + " seconds";
            if (timeLeft <= 0)
            {
                questionTimer.Stop();
                CheckAnswer();
                currentQuestion++;
                ShowQuestion();
            }
        });
    }

    private void CheckAnswer()
    {
        if (answerTextBox.Text.ToLower() == answers[currentQuestion])
        {
            score++;
        }
        else
        {
            incorrectAnswers++;
        }
    }

    private int CalculateIQ(int score, int incorrectAnswers)
    {
        // Simple IQ calculation based on the number of correct answers and incorrect answers
        return 80 + (score * 10) - (incorrectAnswers * 5);
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new IQTestForm());
    }
}
