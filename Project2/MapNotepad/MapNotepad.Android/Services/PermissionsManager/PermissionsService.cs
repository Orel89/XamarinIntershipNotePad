using MapNotepad.Services.PermissionsManager;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MapNotepad.Droid.Services.PermissionsManager
{
    public class PermissionsService : IPermissionsService
    {
        public Task<PermissionStatus> CheckPermissionAsync<T>()
            where T : Permissions.BasePermission, new()
        {
            return Permissions.CheckStatusAsync<T>();
        }

        public async Task<PermissionStatus> RequestPermissionAsync<T>()
            where T : Permissions.BasePermission, new()
        {
            PermissionStatus status = await CheckPermissionAsync<T>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<T>();
            }

            return status;
        }
    }
}