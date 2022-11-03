using ClothingStoreV2.Models;
namespace ClothingStoreV2.Interfaces
{
    public interface IUserDatumRepository
    {
        UserDatum Create(UserDatum userDatum);
        string getAspUserId(string username);
        public bool userFilled(string identityid);
    }
}
