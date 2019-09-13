namespace ListComparison.Comparers
{
    public class CommonItem<TLeft, TRight>
    {
        public TLeft LeftItem { get; set; }
        public TRight RightItem { get; set; }

        public CommonItem(TLeft leftItem, TRight rightItem)
        {
            LeftItem = leftItem;
            RightItem = rightItem;
        }
    }
}
