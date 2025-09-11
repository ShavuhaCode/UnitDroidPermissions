#if UNITY_EDITOR || UNITY_ANDROID
using System;
using UnityEngine;

namespace UnitDroidPermissionsNamespace
{
	public class PermissionCallback : AndroidJavaProxy
	{
		private readonly string[] permissions;
		private readonly Action<UnitDroidPermissions.Permission[]> callback;
		private readonly PermissionCallbackHelper callbackHelper;

		internal PermissionCallback(string[] permissions, Action<UnitDroidPermissions.Permission[]> callback) : base("com.shavuhacode.runtimepermissions.RuntimePermissionsReceiver")
		{
			this.permissions = permissions;
			this.callback = callback;
			callbackHelper = PermissionCallbackHelper.Create(true);
		}

		[UnityEngine.Scripting.Preserve]
		public void OnPermissionResult(string result)
		{
			callbackHelper.CallOnMainThread(() => callback(UnitDroidPermissions.ProcessPermissionRequestResult(permissions, result)));
		}
	}
}
#endif