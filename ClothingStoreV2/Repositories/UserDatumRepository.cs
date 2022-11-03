using ClothingStoreV2.Interfaces;
using ClothingStoreV2.Models;
namespace ClothingStoreV2.Repositories
{
    public class UserDatumRepository : IUserDatumRepository
    {
        private readonly ClothingStoreContext _context;
        public UserDatumRepository(ClothingStoreContext context)
        {
            _context = context;
        }
        public UserDatum Create(UserDatum userDatum)
        {
            _context.UserData.Add(userDatum);
            _context.SaveChanges();
            return userDatum;
        }
        public string getAspUserId(string username)
        {
            string usrid = _context.Users.FirstOrDefault(u => u.NormalizedUserName == username.ToUpper()).Id;
            return usrid;
        }
        public bool userFilled(string identityid)
        {
            return _context.UserData.FirstOrDefault(u => u.IdentityId == identityid) != null;
        }
    }
}
