using DbWebApplication.Models;

namespace DbWebApplication.Interfaces;

public interface IJwtProvider
{
    public string Created(AppUserModel userModel);
}