using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Nerosoft.Starfish.Server.Components.Shared;
using Icons = Microsoft.FluentUI.AspNetCore.Components.Icons;

namespace Nerosoft.Starfish.Server.Components.Layout;

public partial class MainMenuPanel : ComponentBase
{
	private ObservableCollection<MenuItem> Items { get; } = [];

	/// <inheritdoc />
	protected override async Task OnInitializedAsync()
	{
		Items.Add(new MenuLink("/", new Icons.Regular.Size20.Home(), "首页", NavLinkMatch.All));
		Items.Add(new MenuLink("/team", new Icons.Regular.Size20.ChartMultiple(), "团队管理"));
		Items.Add(new MenuGroup(new Icons.Regular.Size20.Folder(), "用户管理", true, "10px", new List<MenuItem>
		{
			new MenuLink("/users/list", null, "用户列表"),
			new MenuLink("/users/roles", null, "角色管理"),
			new MenuLink("/users/permissions", null, "权限管理")
		}));
		Items.Add(new MenuGroup(new Icons.Regular.Size20.Settings(), "设置", false, "10px", new List<MenuItem>
		{
			new MenuLink("/settings/profile", null, "个人资料"),
			new MenuLink("/settings/preferences", null, "偏好设置")
		}));
		await base.OnInitializedAsync();
	}
}