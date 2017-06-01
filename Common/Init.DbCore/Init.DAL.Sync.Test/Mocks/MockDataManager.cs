namespace Init.DAL.Sync.Test.Mocks
{
    using Init.DAL.Sync.Test.DataObjects;
    using Init.DbCore;

    /// <summary>
    /// BaseDataManager
    /// </summary>
    internal class MockDataManager : DataManager
    {
        public MockDataManager()
            : base(new object())
        {
            this.RegisterRepository(new ObjectARepository(this));
            this.RegisterRepository(new ObjectBRepository(this));
        }

        public ObjectARepository ObjectsA
        {
            get { return this.GetRepository<ObjectA>() as ObjectARepository; }
        }

        public ObjectBRepository ObjectsB
        {
            get { return this.GetRepository<ObjectB>() as ObjectBRepository; }
        }
    }
}