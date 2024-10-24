namespace Items
{
    public class ItemHandler
    {
        //List to keep track of existing items
        public List<Item> createdItems = new List<Item>();

        public Item GenerateRandomItem()
        {
            Item item = new Item();
            createdItems.Add(item);

            return item;
        }
    }
}
