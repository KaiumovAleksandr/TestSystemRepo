﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using TestLibrary;

namespace TestDesigner
{
    public partial class FormDesigner : Form
    {
        Question currentQuestion = null;
        Answer currentAnswer = null;
        Test currentTest = null;
        int countQuestions = 0;
        int countAnswers = 0;
        int indexQuestion = -1;
        int indexAnswer = -1;
        public FormDesigner()
        {
            InitializeComponent();
            currentTest = new Test();
            this.Text = $"Test designer: {currentTest.Name}";
            toolStripTextBox_TestName.Text = currentTest.Name;
        }

        private void button_AddQuestion_Click(object sender, EventArgs e)
        {
            textBox_Answer.Clear();
            textBox_Question.Text = "Enter question text here";
            countAnswers = 0;
            indexAnswer = -1;
            currentAnswer = null;
            currentTest.Questions.Add(new Question());
            currentQuestion = currentTest.Questions.LastOrDefault();
            radioListBox_Answers.Items.Clear();
            label_Answers.Text = $"0/{countAnswers}";
            label_Questions.Text = $"{++indexQuestion + 1}/{++countQuestions}";
        }

        private void toolStripTextBox_TestName_TextChanged(object sender, EventArgs e)
        {
            currentTest.Name = toolStripTextBox_TestName.Text;
            this.Text = $"Test designer: {currentTest.Name}";
            if (toolStripTextBox_TestName.Text.Length == 0) MessageBox.Show("Test name is incorrect.");
        }

        private void button_PreviousQuestion_Click(object sender, EventArgs e)
        {
            if (indexQuestion <= 0) return;
            radioListBox_Answers.Items.Clear();
            currentQuestion = currentTest.Questions[--indexQuestion];
            countAnswers = currentQuestion.Answers.Count;
            indexAnswer = 0;
            if (currentQuestion.Answers.Count > 0)
            {
                currentAnswer = currentQuestion.Answers[indexAnswer];
                for(int i = 0; i < currentQuestion.Answers.Count; i++)
                {
                    radioListBox_Answers.Items.Add(currentQuestion.Answers[i].Text);
                    if (currentQuestion.Answers[i].Correct) radioListBox_Answers.SelectedIndex = i;
                }
                    
                textBox_Answer.Text = currentQuestion.Answers[indexAnswer].Text;
                label_Answers.Text = $"{++indexAnswer + 1}/{countAnswers}";
            }
            else
            {
                label_Answers.Text = $"0/{countAnswers}";
            }
            textBox_Question.Text = currentQuestion.Text;
            label_Questions.Text = $"{indexQuestion + 1}/{countQuestions}";
        }

        private void textBox_Question_TextChanged(object sender, EventArgs e)
        {
            if (currentQuestion == null) return;
            if (textBox_Question.Text == "Enter question text here" || textBox_Question.Text == string.Empty) return;
            currentQuestion.Text = textBox_Question.Text;
        }

        private void textBox_Answer_TextChanged(object sender, EventArgs e)
        {
            if (currentAnswer == null) return;
            if (textBox_Answer.Text == "Enter answer text here." || textBox_Answer.Text == string.Empty) return;
            currentAnswer.Text = textBox_Answer.Text;
            radioListBox_Answers.Items[indexAnswer] = textBox_Answer.Text;
        }

        private void button_NextQuestion_Click(object sender, EventArgs e)
        {
            if (indexQuestion == currentTest.Questions.Count - 1) return;
            radioListBox_Answers.Items.Clear();
            currentQuestion = currentTest.Questions[++indexQuestion];
            countAnswers = currentQuestion.Answers.Count;
            indexAnswer = 0;
            if (currentQuestion.Answers.Count > 0)
            {
                currentAnswer = currentQuestion.Answers[indexAnswer];
                for (int i = 0; i < currentQuestion.Answers.Count; i++)
                {
                    radioListBox_Answers.Items.Add(currentQuestion.Answers[i].Text);
                    if (currentQuestion.Answers[i].Correct) radioListBox_Answers.SelectedIndex = i;
                }
                textBox_Answer.Text = currentQuestion.Answers[indexAnswer].Text;
                label_Answers.Text = $"{++indexAnswer + 1}/{countAnswers}";
            }
            else
            {
                label_Answers.Text = $"0/{countAnswers}";
            }
            textBox_Question.Text = currentQuestion.Text;
            label_Questions.Text = $"{indexQuestion + 1}/{countQuestions}";
        }

        private void button_DeleteQuestion_Click(object sender, EventArgs e)
        {
            if (currentTest.Questions.Count == 0) return;
            radioListBox_Answers.Items.Clear();
            currentTest.Questions.RemoveAt(indexQuestion);
            if (indexQuestion == currentTest.Questions.Count) indexQuestion--;
            if (currentTest.Questions.Count == 0)
            {
                countAnswers = 0;
                indexAnswer = -1;
                currentQuestion = null;
                textBox_Question.Clear();
                label_Questions.Text = $"{indexQuestion + 1}/{--countQuestions}";
                label_Answers.Text = $"0/{countAnswers}";
                return;
            }
            currentQuestion = currentTest.Questions[indexQuestion];
            countAnswers = currentQuestion.Answers.Count;
            indexAnswer = -1;
            if (currentQuestion.Answers.Count > 0)
            {
                currentAnswer = currentQuestion.Answers[indexAnswer];
                for (int i = 0; i < currentQuestion.Answers.Count; i++)
                {
                    radioListBox_Answers.Items.Add(currentQuestion.Answers[i].Text);
                    if (currentQuestion.Answers[i].Correct) radioListBox_Answers.SelectedIndex = i;
                }
                textBox_Answer.Text = currentQuestion.Answers[indexAnswer].Text;
                label_Answers.Text = $"{++indexAnswer + 1}/{countAnswers}";
            }
            else
            {
                label_Answers.Text = $"0/{countAnswers}";
            }
            textBox_Question.Text = currentQuestion.Text;
            label_Questions.Text = $"{indexQuestion + 1}/{--countQuestions}";
        }

        private void button_AddAnswer_Click(object sender, EventArgs e)
        {
            if (currentQuestion == null)
            {
                MessageBox.Show("Question doesn't exist");
                return;
            }
            radioListBox_Answers.Items.Add("");
            textBox_Answer.Text = "Enter answer text here.";
            currentQuestion.Answers.Add(new Answer());
            currentAnswer = currentQuestion.Answers.LastOrDefault();
            label_Answers.Text = $"{++indexAnswer + 1}/{++countAnswers}";
        }

        private void button_DeleteAnswer_Click(object sender, EventArgs e)
        {
            if (currentQuestion == null)
            {
                MessageBox.Show("Question doesn't exist");
                return;
            }
            currentQuestion.Answers.RemoveAt(indexAnswer);
            radioListBox_Answers.Items.RemoveAt(indexAnswer);
            if (indexAnswer == currentQuestion.Answers.Count) indexAnswer--;
            if(currentQuestion.Answers.Count == 0)
            {
                countAnswers = 0;
                indexAnswer = -1;
                currentAnswer = null;
                textBox_Answer.Text = string.Empty;
                label_Answers.Text = $"{indexAnswer + 1}/{countAnswers}";
                return;
            }
            currentAnswer = currentQuestion.Answers[indexAnswer];
            textBox_Answer.Text = currentQuestion.Answers[indexAnswer].Text;
            label_Answers.Text = $"{indexAnswer + 1}/{--countAnswers}";
        }

        private void button_PreviousAnswer_Click(object sender, EventArgs e)
        {
            if (currentQuestion == null)
            {
                MessageBox.Show("Question doesn't exist");
                return;
            }
            if (indexAnswer <= 0) return;
            currentAnswer = currentQuestion.Answers[--indexAnswer];
            textBox_Answer.Text = currentAnswer.Text;
            label_Answers.Text = $"{indexAnswer + 1}/{countAnswers}";
        }

        private void button_NextAnswer_Click(object sender, EventArgs e)
        {
            if (currentQuestion == null) return;
            if (indexAnswer == currentQuestion.Answers.Count - 1) return;
            currentAnswer = currentQuestion.Answers[++indexAnswer];
            textBox_Answer.Text = currentAnswer.Text;
            label_Answers.Text = $"{indexAnswer + 1}/{countAnswers}";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTest.Subject = textBox_Subject.Text;
            currentTest.Theme = textBox_Theme.Text;
            currentTest.Author = textBox_Author.Text;
            XmlSerializer formatter = new XmlSerializer(typeof(Test));

            using (FileStream fs = new FileStream($"{toolStripTextBox_TestName.Text}", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, currentTest);
            }
        }

        private void radioListBox_Answers_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var i in currentQuestion.Answers) i.Correct = false;
            currentQuestion.Answers[((RadioListBox)sender).SelectedIndex].Correct = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTest.Questions.Count > 0)
            {
                MessageBox.Show("Do you want save current test?", "Current project isn't empty", MessageBoxButtons.YesNoCancel);
                if(DialogResult == DialogResult.Yes)
                {
                    currentTest.Subject = textBox_Subject.Text;
                    currentTest.Theme = textBox_Theme.Text;
                    currentTest.Author = textBox_Author.Text;
                    XmlSerializer formatter = new XmlSerializer(typeof(Test));

                    using (FileStream fs = new FileStream($"{toolStripTextBox_TestName.Text}", FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, currentTest);
                    }
                }
                if(DialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            for(int i = 1; i < 100; i++)
            {
                if (File.Exists($"NewTest{i}.xml")) continue;
                else
                {
                    currentTest = new Test();
                    break;
                }

            }
            this.Text = $"Test designer: {currentTest.Name}";
            toolStripTextBox_TestName.Text = currentTest.Name;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Test));
            if (currentTest.Questions.Count > 0)
            {
                MessageBox.Show("Do you want save current test?", "Current project isn't empty", MessageBoxButtons.YesNoCancel);
                if (DialogResult == DialogResult.Yes)
                {
                    currentTest.Subject = textBox_Subject.Text;
                    currentTest.Theme = textBox_Theme.Text;
                    currentTest.Author = textBox_Author.Text;
                   

                    using (FileStream fs = new FileStream($"{toolStripTextBox_TestName.Text}", FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, currentTest);
                    }
                }
                if (DialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            OpenFileDialog dialog = new OpenFileDialog() { Filter = "XML files (*.xml)|*.xml" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(dialog.FileName, FileMode.OpenOrCreate))
                {
                    currentTest = (Test)formatter.Deserialize(fs);
                };
                currentQuestion = currentTest.Questions[0];
                currentAnswer = currentTest.Questions[0].Answers[0];
                indexAnswer = 0;
                indexQuestion = 0;
                countAnswers = currentTest.Questions[0].Answers.Count;
                countQuestions = currentTest.Questions.Count;
                foreach (var i in currentQuestion.Answers)
                    radioListBox_Answers.Items.Add(i.Text);
                textBox_Author.Text = currentTest.Author;
                textBox_Subject.Text = currentTest.Subject;
                textBox_Theme.Text = currentTest.Theme;
                textBox_Question.Text = currentQuestion.Text;
                textBox_Answer.Text = currentAnswer.Text;
                radioListBox_Answers.Items.Clear();
                
            }
        }
    }
}
