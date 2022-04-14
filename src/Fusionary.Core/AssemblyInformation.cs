using System.Reflection;

namespace Fusionary.Core;

/// <summary>
/// Represents assembly information.
/// </summary>
/// <param name="Product">Product.</param>
/// <param name="Description">Description.</param>
/// <param name="Version">Version.</param>
public record class AssemblyInformation(string Product, string Description, string Version) {
    /// <summary>
    /// Gets the assembly information from the currently executing assembly.
    /// </summary>
    public static readonly AssemblyInformation Current = new(Assembly.GetExecutingAssembly());
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AssemblyInformation" /> class.
    /// </summary>
    /// <param name="assembly">Assembly from which to get information.</param>
    public AssemblyInformation(Assembly assembly)
        : this(
            assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "",
            assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? "",
            assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ?? ""
        ) { }
}