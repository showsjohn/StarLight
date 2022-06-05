using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


/// <summary>
/// A class used to draw text elements to the screen from a supplied sprite font file.
/// </summary>
internal class TextManager : SpriteGameObject
{
    protected int fontTableWidth;
    protected int fontTableHeight;
    protected int offSet;
    protected string alphanumericals;
    protected Dictionary<char, Rectangle> lookUpTable;
    protected float fontSize;

    //variables for DrawTimer
    private int time = 0;
    private int counter = 0;

    private string zero = "";
    //

    private List<string> textList;
    private List<Vector2> textPositions;

    public TextManager(int width, int height, int offSet, float fontSize) : base("wipfont")
    {
        fontTableWidth = width;
        fontTableHeight = height;
        this.offSet = offSet;
        this.fontSize = fontSize;
        alphanumericals =
            " !cdefgh" + //FILLER
            "()*+,-./" +
            "01234567" +
            "89:;<=>?" +
            "@ABCDEFG" +
            "HIJKLMNO" +
            "PQRSTUVW" +
            "XYZ[\\]^_";
        CreateLookUpTable();
        textList = new List<string>();
        textPositions = new List<Vector2>();
        
    }

    public void CreateLookUpTable()
    {
        lookUpTable = new Dictionary<char, Rectangle>() { };
        var counter = 0;
        for (var y = 0; y < fontTableHeight; y++)
        for (var x = 0; x < fontTableWidth; x++)
        {
            lookUpTable.Add(alphanumericals[counter],
                new Rectangle(x * (offSet), y * (offSet), offSet, offSet));
            counter++;
        }
    }

    /// <summary> Draws text to screen from the TextManager's "font" sprite</summary>
    public void TextToScreen(string text, Vector2 position)
    {
        textList.Add(text);
        //position = new Vector2(position.X-(text.Length*offSet)*0.25f, position.Y);
        textPositions.Add(position);
    }

    public void DrawTimer(Vector2 position)
    {
        var timer = time.ToString();
        if (timer.Length == 2)
            zero = "0";
        else if (timer.Length == 1)
            zero = "00";
        else if (timer.Length >= 3) 
            zero = "";
        TextToScreen(zero + timer, position);

        counter++;
        if (counter == 60)
        {
            time++;
            counter = 0;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        var i = 0;
        var z = 0;
        if (textList != null)
            while (z < textList.Count)
            {
                foreach (var letter in textList[z])
                {
                    Rectangle rect;

                    rect = lookUpTable[letter];

                    spriteBatch.Draw(sprite,
                        new Vector2(textPositions[z].X + i * (offSet * fontSize), textPositions[z].Y), rect,
                        Color.White, 0, origin, fontSize, SpriteEffects.None, 0f);
                    i++;
                }

                i = 0;
                z++;
            }

        textList.Clear();
        textPositions.Clear();
    }
}