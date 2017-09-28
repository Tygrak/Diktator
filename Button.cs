using System;
using OpenTK.Input;
using SharpCanvas;
using System.Drawing;
using System.Drawing.Imaging;

namespace Diktator{
    public class Button{
        public static int buttonTexture;
        public static GlyphMap glyphMap;
        public int value;
        public Action OnClickAction;
        public Sprite3D buttonSprite;
        public DynamicText buttonText;

        public Button(OpenTK.Vector3 pos, OpenTK.Vector3 scale){
            buttonSprite = new Sprite3D(pos, scale, buttonTexture);
            buttonText = new DynamicText(pos, new OpenTK.Vector3(scale.X*0.125f, scale.Y*0.4f, 1f), "testButton", glyphMap);
            buttonText.AlignText(TextAlignment.Centralized);
        }

        public bool CheckHit(OpenTK.Vector2 clickPosition){
            if(clickPosition.X > buttonSprite.position.X - buttonSprite.scale.X/2 && clickPosition.X < buttonSprite.position.X + buttonSprite.scale.X/2 && 
               clickPosition.Y > buttonSprite.position.Y - buttonSprite.scale.Y/2 && clickPosition.Y < buttonSprite.position.Y + buttonSprite.scale.Y/2){
                return true;
            }
            return false;
        }
    }
}