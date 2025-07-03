namespace Backend.Modules.Blog.Contracts.IService;

public interface ITreeHoleService {
    Task<bool> AddTreeHole(string content);
}