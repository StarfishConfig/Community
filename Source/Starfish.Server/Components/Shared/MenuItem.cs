using Microsoft.AspNetCore.Components.Routing;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Nerosoft.Starfish.Server.Components.Shared;

/// <summary>
/// Base type representing a single menu item.
/// </summary>
/// <remarks>
/// This abstract record provides common properties for both navigable links and group headers.
/// Implementations include <see cref="MenuLink"/> and <see cref="MenuGroup"/>.
/// </remarks>
public abstract record MenuItem
{
	/// <summary>
	/// The display title of the menu item.
	/// </summary>
	public string Title { get; init; } = string.Empty;

	/// <summary>
	/// The navigation target for the menu item.
	/// </summary>
	/// <remarks>
	/// For link items this is the target URL/path. For group headers this may be <c>null</c>.
	/// </remarks>
	public string Href { get; init; }

	/// <summary>
	/// Determines how the navigation link's URL matching behaves when rendering active state.
	/// Defaults to <see cref="NavLinkMatch.Prefix"/>.
	/// </summary>
	public NavLinkMatch Match { get; init; } = NavLinkMatch.Prefix;

	/// <summary>
	/// The icon shown next to the menu item.
	/// </summary>
	public Icon Icon { get; init; } = new Microsoft.FluentUI.AspNetCore.Components.Icons.Regular.Size20.Document();
}

/// <summary>
/// A menu item that represents a navigable link.
/// </summary>
public record MenuLink : MenuItem
{
	/// <summary>
	/// Initializes a new instance of <see cref="MenuLink"/>.
	/// </summary>
	/// <param name="href">The target URL or path for this link.</param>
	/// <param name="icon">The icon to display next to the link.</param>
	/// <param name="title">The visible title of the link.</param>
	/// <param name="match">Optional: how the link's URL is matched to determine active state. Defaults to <see cref="NavLinkMatch.Prefix"/>.</param>
	public MenuLink(string href, Icon icon, string title, NavLinkMatch match = NavLinkMatch.Prefix)
	{
		Href = href;
		Icon = icon;
		Title = title;
		Match = match;
	}
}

/// <summary>
/// A menu item that groups other menu items under a header.
/// </summary>
/// <remarks>
/// A group is not a navigable link itself (its <see cref="MenuItem.Href"/> is set to <c>null</c>).
/// It contains child items and visual properties such as expanded state and gap spacing.
/// </remarks>
public record MenuGroup : MenuItem
{
	/// <summary>
	/// Indicates whether the group is expanded (children visible) or collapsed.
	/// </summary>
	public bool Expanded { get; set; }

	/// <summary>
	/// Visual gap/spacing information for rendering children.
	/// </summary>
	public string Gap { get; init; }

	/// <summary>
	/// Read-only list of child menu items contained by this group.
	/// </summary>
	public IReadOnlyList<MenuItem> Children { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="MenuGroup"/>.
	/// </summary>
	/// <param name="icon">The icon used for the group header.</param>
	/// <param name="title">The display title for the group.</param>
	/// <param name="expanded">Initial expanded/collapsed state.</param>
	/// <param name="gap">Visual gap/spacing value used when rendering children.</param>
	/// <param name="children">List of child menu items; will be exposed as a read-only collection.</param>
	public MenuGroup(Icon icon, string title, bool expanded, string gap, List<MenuItem> children)
	{
		Href = null;
		Icon = icon;
		Title = title;
		Expanded = expanded;
		Gap = gap;
		Children = children.AsReadOnly();
	}
}