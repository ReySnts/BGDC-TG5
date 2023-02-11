EXTERNAL Name(charName)
EXTERNAL Icon(charName)

{Name("Guide")}
{Icon("Dialogue - Guide (1)")}
Now that you've recovered some of your memories, do you remember me?
{Name("")}
{Icon("Dialogue - MC (1)")}
...
...
..!
Mom..?
{Name("Mom")}
{Icon("Dialogue - Mom (1)")}
Come, my child.
Come closer.
Stay with me.
{Name("")}
{Icon("Blank")}
Stay With Mom? 
* [YES]
    -> BadEnding
* [NO]
    {Name("Mom")}
    {Icon("Dialogue - Mom (1)")}
    Why not, my child?
    Do you not trust me?
    {Name("")}
    {Icon("Blank")}
    Stay With Mom?
    ** [YES]
        -> BadEnding
    ** [NO]
        {Name("Mom")}
        {Icon("Dialogue - Mom (1)")}
        Please stay with me, my child.
        I will protect you, you will be safe here with me.
        {Name("")}
        {Icon("Blank")}
        Stay With Mom?
        *** [YES]
            -> BadEnding
        *** [NO]
            {Name("Mom")}
            {Icon("CG - Real Mom")}
            You'll regret your decision!
            ->GoodEnding

= BadEnding
    -> END

= GoodEnding
    -> END