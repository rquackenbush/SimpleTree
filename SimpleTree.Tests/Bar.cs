namespace CaptiveAire.SimpleTree.Tests
{
    public class Bar
    {
        public Bar(int id, int? parentId)
        {
            this.Id = id;
            this.ParentId = parentId;
        }

        public int Id { get; set; }

        public int? ParentId { get; set; }
    }
}
