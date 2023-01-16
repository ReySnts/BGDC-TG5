EXTERNAL Name(charName)
EXTERNAL Icon(charName)

{Name("Mom")}
{Icon("Dialogue - Guide")}
Now that you've recovered some of your memories, do you remember me?
{Name("")}
{Icon("Dialogue - MC")}
...
...
..!
mom..?
{Name("Mom")}
{Icon("Dialogue - Guide")}
Come, my child.
Come closer.
Stay with me.
* [YES]
    -> BadEnding
* [NO]
    {Name("Mom")}
    {Icon("Dialogue - Guide")}
    Why not, my child?
    Do you not trust me?
    ** [YES]
        -> BadEnding
    ** [NO]
        {Name("Mom")}
        {Icon("Dialogue - Guide")}
        Please stay with me, my child.
        I will protect you, you will be safe here with me.
        *** [YES]
            -> BadEnding
        *** [NO]
            {Name("Mom")}
            {Icon("Dialogue - Guide")}
            You leave me no choice then.
            If I can't convince you, then I'll have to make you stay.
            Be not afraid, my child.
            This is for your own good.
            -> GoodEnding

= BadEnding
    -> END

= GoodEnding
    -> END