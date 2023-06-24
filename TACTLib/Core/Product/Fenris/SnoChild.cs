using System.Runtime.InteropServices;

namespace TACTLib.Core.Product.Fenris;

[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x10)]
public struct SnoChild {
    public uint SnoId;
    public uint SubId;
}