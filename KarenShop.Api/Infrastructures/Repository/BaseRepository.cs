namespace KarenShop.Api.Infrastructures.Repository
{
    public class BaseRepository
    {
        protected KarenShopDbContext _context;
        public BaseRepository(KarenShopDbContext context)
        {
            _context = context;
        }
    }
}