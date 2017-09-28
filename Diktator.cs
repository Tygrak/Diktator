using System;
using OpenTK.Input;
using SharpCanvas;
using System.Drawing;
using System.Drawing.Imaging;

namespace Diktator{
    class Diktator{
        SharpCanvasWindow scWindow;
        MainWindow mainWindow;
        Sprite3D selectionMarker;
        Sprite3D questionDiv;
        DynamicText questionText;
        Button[] buttons = new Button[4];
        Bitmap canvas;
        Color backgroundColor = Color.SteelBlue;
        bool showingMap = false;

        public void Start(){
            scWindow = new SharpCanvasWindow(600, 600, "Diktator 3.0", false);
            scWindow.Start(60, 60);
            mainWindow = scWindow.Window;
            mainWindow.SetClearColor(OpenTK.Color.White);
            int markerTexture = mainWindow.LoadTexture("x.png");
            Button.buttonTexture = mainWindow.LoadTexture("button.png");
            Button.glyphMap = new GlyphMap(GlyphMap.czechGlyphs, 64);
            Button.glyphMap.texture = mainWindow.LoadTextureFromBitmap(Button.glyphMap.bitmap);
            selectionMarker = new Sprite3D(new OpenTK.Vector3(0, 0, 10), new OpenTK.Vector3(0.03f, 0.03f, 1), markerTexture);
            questionDiv = new Sprite3D(new OpenTK.Vector3(0, 0.5f, -2), new OpenTK.Vector3(1.6f, 0.85f, 1), Button.buttonTexture);
            questionText = new DynamicText(new OpenTK.Vector3(0, 0.5f, -2), new OpenTK.Vector3(0.08f, 0.2f, 1), "Kolik sou 1+1???", Button.glyphMap);
            questionText.AlignText(TextAlignment.Centralized);
            buttons[0] = new Button(new OpenTK.Vector3(0.5f, -0.25f, -2), new OpenTK.Vector3(0.8f, 0.3f, 1));
            buttons[1] = new Button(new OpenTK.Vector3(0.5f, -0.6f, -2), new OpenTK.Vector3(0.8f, 0.3f, 1));
            buttons[2] = new Button(new OpenTK.Vector3(-0.5f, -0.25f, -2), new OpenTK.Vector3(0.8f, 0.3f, 1));
            buttons[3] = new Button(new OpenTK.Vector3(-0.5f, -0.6f, -2), new OpenTK.Vector3(0.8f, 0.3f, 1));
            buttons[0].OnClickAction = () => {Console.WriteLine("1 pressed.");};
            buttons[1].OnClickAction = () => {Console.WriteLine("2 pressed.");};
            buttons[2].OnClickAction = () => {Console.WriteLine("3 pressed.");};
            buttons[3].OnClickAction = () => {Console.WriteLine("4 pressed.");};
            canvas = mainWindow.CreateCanvas(backgroundColor);
            mainWindow.AddDrawable(questionDiv);
            mainWindow.AddDrawable(questionText);
            mainWindow.AddDrawable(buttons[0].buttonSprite);
            mainWindow.AddDrawable(buttons[1].buttonSprite);
            mainWindow.AddDrawable(buttons[2].buttonSprite);
            mainWindow.AddDrawable(buttons[3].buttonSprite);
            mainWindow.AddDrawable(buttons[0].buttonText);
            mainWindow.AddDrawable(buttons[1].buttonText);
            mainWindow.AddDrawable(buttons[2].buttonText);
            mainWindow.AddDrawable(buttons[3].buttonText);
            mainWindow.AddDrawable(selectionMarker);
            canvas.RotateFlip(RotateFlipType.RotateNoneFlipY);
            mainWindow.ReloadCanvas();
            mainWindow.AddUpdateAction(Update);
        }

        public void ShowMap(){
            using(Graphics g = Graphics.FromImage(canvas)){
                g.DrawImage(Image.FromFile("map.jpeg"), 0, 0, canvas.Width, canvas.Height);
            }
            mainWindow.ReloadCanvas();
            selectionMarker.position.Z = -10;
            showingMap = true;
        }

        public void DisableMap(){
            using(Graphics g = Graphics.FromImage(canvas)){
                g.FillRectangle(new SolidBrush(backgroundColor), 0, 0, canvas.Width, canvas.Height);
            }
            mainWindow.ReloadCanvas();
            selectionMarker.position.Z = 10;
            showingMap = false;
        }

        public void LoadQuestion(Question q){
            questionText.SetText(q.questionText);
            for (int i = 0; i < 4; i++){
                buttons[i].buttonText.SetText(q.answersText[i]);
            }
        }

        void Update(){
            if(mainWindow.Input.MousePressed(MouseButton.Left)){
                OpenTK.Vector2 clickPosition = mainWindow.ScreenToWorldPos(mainWindow.Input.MousePosX, mainWindow.Input.MousePosY);
                if(showingMap){
                    selectionMarker.position.Xy = clickPosition;
                } else{
                    for (int i = 0; i < buttons.Length; i++){
                        if(buttons[i].CheckHit(clickPosition)){
                            buttons[i].OnClickAction.Invoke();
                        }
                    }
                }
            }
        }
    }
}