using System;
using OpenTK.Input;
using SharpCanvas;
using System.Drawing;
using System.Drawing.Imaging;

namespace Diktator{
    public class Question{
        public string questionText;
        public string[] answersText;

        public Question(string questionText, string[] answersText){
            this.questionText = questionText;
            this.answersText = answersText;
        }
    }
}