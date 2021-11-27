using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MapNotepad.Services.PermissionsManager
{
    public interface IPermissionsService
    {
        Task<PermissionStatus> CheckPermissionAsync<T>()
           where T : Permissions.BasePermission, new();

        Task<PermissionStatus> RequestPermissionAsync<T>()
            where T : Permissions.BasePermission, new();
    }
}
