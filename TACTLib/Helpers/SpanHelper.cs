using System;
using System.Runtime.InteropServices;

namespace TACTLib.Helpers
{
    public static class SpanHelper
    {
        public static Span<byte> Advance(ref Span<byte> input, int numBytes)
        {
            var result = input.Slice(0, numBytes);
            input = input.Slice(numBytes);

            return result;
        }
        
        public static ReadOnlySpan<byte> Advance(ref ReadOnlySpan<byte> input, int numBytes)
        {
            var result = input.Slice(0, numBytes);
            input = input.Slice(numBytes);

            return result;
        }

        public static unsafe ref T ReadStruct<T>(ref Span<byte> input) where T : unmanaged
        {
            ref var result = ref MemoryMarshal.AsRef<T>(input);
            input = input.Slice(sizeof(T));
            return ref result;
        }
        
        public static unsafe T ReadStruct<T>(ref ReadOnlySpan<byte> input) where T : unmanaged
        {
            var result = MemoryMarshal.Read<T>(input);
            input = input.Slice(sizeof(T));
            return result;
        }
        
        public static unsafe ReadOnlySpan<T> ReadArray<T>(ref ReadOnlySpan<byte> input, int count) where T : unmanaged
        {
            var resultBytes = Advance(ref input, sizeof(T) * count);
            return MemoryMarshal.Cast<byte, T>(resultBytes);
        }

        public static byte ReadByte(ref Span<byte> input)
        {
            var result = input[0];
            input = input.Slice(1);
            return result;
        }
    }
}