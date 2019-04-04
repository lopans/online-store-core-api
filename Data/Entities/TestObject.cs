using Base.DAL;

namespace Data.Entities
{
    public class TestObject: BaseEntity
    {
        public int Number { get; set; }
        public string Title { get; set; }
    }
}
