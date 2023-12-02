﻿using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_47452 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[SignedMod(length * Keytable[0], 512)];
            uint increment = kidx % 61;
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = (uint)(digest[7] + (ushort)header.m_dataCount) & 511;
            uint increment = (uint)header.m_entryCount + digest[SignedMod(header.m_entryCount, SHA1_DIGESTSIZE)];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += increment;
                buffer[i] ^= digest[SignedMod(header.m_buildVersion + i, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0xD7, 0x4E, 0x89, 0x5A, 0x94, 0xF6, 0x16, 0x6D, 0xCC, 0xDB, 0x37, 0x3D, 0xCB, 0xBD, 0x61, 0xDE, 
            0x54, 0x68, 0xEE, 0x88, 0x41, 0xC6, 0xA4, 0x0F, 0x3F, 0xCE, 0xA3, 0x97, 0xC3, 0x90, 0xF4, 0x45, 
            0x37, 0x9E, 0x93, 0xCC, 0xFF, 0xA4, 0x24, 0xB6, 0x43, 0x1D, 0xED, 0xF3, 0xAC, 0x05, 0x7B, 0x44, 
            0xC8, 0x8E, 0xB2, 0xDF, 0xA1, 0x9A, 0x65, 0x23, 0x6A, 0x75, 0x1B, 0xE7, 0xB0, 0x04, 0x25, 0x0D, 
            0x0C, 0x93, 0x12, 0x1C, 0x1D, 0x81, 0x3F, 0xA5, 0x09, 0x52, 0x8D, 0x7F, 0x31, 0xC9, 0xB5, 0x96, 
            0x7E, 0x4E, 0x90, 0x9E, 0x89, 0x1D, 0x3C, 0x63, 0x6F, 0xD0, 0xD1, 0x83, 0x3F, 0x8E, 0x39, 0x33, 
            0x3C, 0xC7, 0xDD, 0xD9, 0xBC, 0x3F, 0xFE, 0x58, 0xB3, 0x84, 0xCB, 0x63, 0xA7, 0x7C, 0xCC, 0xCF, 
            0x31, 0x9A, 0x8C, 0x69, 0xD5, 0x98, 0x7F, 0x49, 0x6B, 0xAB, 0x16, 0x44, 0x5A, 0xD0, 0x8E, 0x5F, 
            0x6A, 0xEA, 0x70, 0x6B, 0xB4, 0x03, 0x7A, 0x3D, 0x9E, 0x40, 0xFE, 0x09, 0x42, 0xF8, 0xB4, 0x28, 
            0x91, 0x9B, 0x16, 0xF0, 0xE0, 0xD0, 0x68, 0x41, 0xE1, 0xB0, 0x76, 0xD1, 0x7B, 0xBC, 0x70, 0x69, 
            0xE4, 0xDB, 0x77, 0xAF, 0x32, 0xE4, 0xD6, 0x78, 0x24, 0xB5, 0x91, 0xA2, 0xC8, 0x95, 0x40, 0x43, 
            0xC7, 0x1F, 0xF7, 0xF8, 0xF2, 0xB1, 0xA1, 0x7B, 0xD6, 0x83, 0xCB, 0x1E, 0x72, 0xEE, 0x1A, 0xFD, 
            0x0A, 0xF9, 0xB0, 0x58, 0xD6, 0x64, 0xFA, 0x95, 0x87, 0x91, 0xB6, 0x2A, 0x02, 0x28, 0x12, 0x8F, 
            0xE5, 0x22, 0x83, 0xCC, 0x9B, 0x2C, 0xDD, 0x23, 0xC7, 0xC5, 0x1D, 0xC6, 0x46, 0x5F, 0x5B, 0x65, 
            0xAC, 0x64, 0x3A, 0xA3, 0x3A, 0x3A, 0xEB, 0x84, 0x09, 0xF7, 0xEA, 0x4F, 0x1D, 0xF5, 0x0C, 0x28, 
            0xD2, 0xEB, 0xF9, 0xFC, 0xE6, 0xFA, 0x3B, 0x14, 0x8F, 0x66, 0xAA, 0x8E, 0x6A, 0xD9, 0x4A, 0xFD, 
            0x9A, 0x29, 0x8D, 0x0D, 0x34, 0x37, 0x7A, 0xBA, 0xDD, 0xF1, 0x7D, 0xBD, 0xD8, 0x58, 0x2B, 0xB7, 
            0xB5, 0x8D, 0xB9, 0xB0, 0xDA, 0x52, 0x56, 0xC1, 0xA9, 0x52, 0x1B, 0x64, 0x5F, 0x0B, 0xE1, 0x6B, 
            0x8F, 0x2F, 0x99, 0xC4, 0x19, 0xC7, 0x2F, 0x9C, 0x54, 0x24, 0x57, 0xE3, 0x92, 0xB1, 0xA1, 0xD7, 
            0xC2, 0x16, 0x50, 0x96, 0x3A, 0xE2, 0x59, 0x88, 0x90, 0x3C, 0xAA, 0x06, 0xE3, 0x92, 0x49, 0x0E, 
            0xFA, 0x81, 0xB9, 0x59, 0x05, 0xAC, 0x21, 0x3F, 0x49, 0xA1, 0xA9, 0xD1, 0x74, 0x65, 0x29, 0x7A, 
            0x60, 0xBE, 0x1E, 0x31, 0x5B, 0x37, 0x51, 0xE8, 0xC0, 0xA5, 0x90, 0x8F, 0x31, 0x48, 0x3A, 0x2D, 
            0x26, 0xFF, 0xC0, 0xC6, 0x84, 0xEE, 0xAD, 0x3F, 0xB4, 0xA6, 0xAD, 0x1F, 0xF0, 0xCA, 0x1B, 0x02, 
            0x5D, 0x79, 0x6A, 0x1E, 0xE7, 0x71, 0xCA, 0x93, 0x25, 0x46, 0xB5, 0x26, 0xCC, 0x67, 0x48, 0x0E, 
            0x95, 0xAA, 0x04, 0x3B, 0x76, 0x73, 0x41, 0x19, 0x04, 0x1B, 0xEA, 0x2E, 0x9F, 0x08, 0x0A, 0xB5, 
            0xF8, 0xA9, 0xFE, 0x88, 0x26, 0x3E, 0x04, 0x9B, 0xE5, 0x56, 0x30, 0x27, 0x50, 0xFC, 0x91, 0x3F, 
            0x22, 0xE9, 0xDD, 0xC5, 0x2D, 0x3E, 0x36, 0xB3, 0x71, 0x5F, 0x30, 0x04, 0x2D, 0x93, 0x57, 0x78, 
            0x0F, 0xA0, 0x74, 0x9C, 0x02, 0x8B, 0x9D, 0x03, 0x3C, 0x98, 0x78, 0x5B, 0x00, 0x76, 0xA1, 0xFD, 
            0x87, 0xDB, 0xFE, 0xDD, 0x1F, 0x29, 0xC2, 0x31, 0xAC, 0x2A, 0xF7, 0x84, 0x6A, 0xF4, 0x39, 0x49, 
            0xA4, 0x7A, 0x3A, 0x7B, 0xD6, 0x77, 0xA9, 0xE5, 0x40, 0x50, 0x36, 0x40, 0xE7, 0x9C, 0xDD, 0x3A, 
            0x14, 0x45, 0x26, 0xBB, 0x67, 0xBA, 0x33, 0xDB, 0x8A, 0x87, 0xAC, 0x6E, 0x10, 0x2E, 0x9D, 0x4A, 
            0x6A, 0x10, 0x44, 0x63, 0xBA, 0xEA, 0x53, 0x93, 0xF8, 0xCB, 0xCD, 0xC4, 0x1B, 0x12, 0xBE, 0x75
        };
    }
}