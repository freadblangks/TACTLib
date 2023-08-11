using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance;
using TACTLib.Core;

namespace TACTLib.Container {
    public class CKeyOrderComparer : IComparer<CKey>,
                                     IComparer<EncodingHandler.EKeyESpecEntry>,
                                     IComparer<EncodingHandler.CKeyEKeyEntry> {
        public static readonly CKeyOrderComparer Instance = new CKeyOrderComparer();

        public int Compare(CKey x, CKey y) {
            return CKeyCompare(y, x);
        }

        public int Compare(EncodingHandler.EKeyESpecEntry x, EncodingHandler.EKeyESpecEntry y) {
            return Compare(x.EKey, y.EKey);
        }

        public int Compare(EncodingHandler.CKeyEKeyEntry x, EncodingHandler.CKeyEKeyEntry y) {
            return Compare(x.CKey, y.CKey);
        }

        public static int CKeyCompare(CKey left, CKey right)
        {
            var leftSpan = MemoryMarshal.CreateReadOnlySpan(ref left, 1).AsBytes();
            var rightSpan = MemoryMarshal.CreateReadOnlySpan(ref right, 1).AsBytes();

            var leftU0 = BinaryPrimitives.ReadUInt64BigEndian(leftSpan);
            var rightU0 = BinaryPrimitives.ReadUInt64BigEndian(rightSpan);

            var compareA = rightU0.CompareTo(leftU0);
            if (compareA != 0) return compareA;

            var leftU1 = BinaryPrimitives.ReadUInt64BigEndian(leftSpan);
            var rightU1 = BinaryPrimitives.ReadUInt64BigEndian(rightSpan);
            return rightU1.CompareTo(leftU1);
        }
    }
}