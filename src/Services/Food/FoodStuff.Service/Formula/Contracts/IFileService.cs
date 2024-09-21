using Common;

namespace FoodStuff.Service.Contracts
{
    public interface IFileService : ITransientDependency
    {
        public string AddImage(string imageFile, string path2, string imageName);
        public void RemoveImage(string imageName, string path2);
    }
}
