﻿using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_37865 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.m_buildVersion & 511];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[kidx % 512];
                kidx -= 489;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.m_dataCount & 511];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[kidx % 512];
                kidx = header.m_buildVersion - kidx;
                buffer[i] ^= digest[(i + kidx) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x29, 0x02, 0x18, 0x1D, 0xA1, 0xA3, 0x43, 0x4F, 0xDC, 0xF7, 0x5A, 0x9E, 0x86, 0x8D, 0xA9, 0xF8,
            0x7F, 0xFE, 0x83, 0x5C, 0x1F, 0x08, 0xC9, 0x35, 0x16, 0x26, 0xE4, 0x64, 0xB7, 0x14, 0x34, 0x93,
            0x64, 0x76, 0xF1, 0xDE, 0x8F, 0x38, 0xDB, 0xC7, 0xBA, 0x2B, 0xC2, 0x7F, 0xD3, 0x3C, 0xFA, 0x19,
            0xA4, 0x01, 0x7E, 0x19, 0x08, 0x9B, 0xD6, 0xA3, 0x02, 0xE1, 0xC9, 0xF0, 0x88, 0xE2, 0x98, 0xED,
            0xA3, 0x9B, 0xFC, 0x28, 0x22, 0xB3, 0x37, 0xFA, 0xAA, 0x99, 0x79, 0x75, 0xA0, 0x82, 0x94, 0x53,
            0x7C, 0x66, 0xDF, 0xC8, 0x83, 0xC3, 0xD1, 0x15, 0xAF, 0xDF, 0x6A, 0xA7, 0x9A, 0xE2, 0x1D, 0x61,
            0xF8, 0xF7, 0x34, 0xBA, 0x70, 0xA5, 0x9C, 0x88, 0xC2, 0x64, 0x3A, 0x1D, 0x1D, 0x88, 0x7A, 0x78,
            0x0F, 0x4F, 0x54, 0xF0, 0x63, 0x4B, 0x55, 0x4F, 0x22, 0x56, 0xDD, 0xBD, 0xC9, 0x89, 0x0D, 0xA7,
            0xD2, 0xE7, 0xE9, 0xC3, 0x2B, 0x16, 0xD2, 0xA8, 0x38, 0x07, 0x7C, 0x56, 0xA3, 0x24, 0x4F, 0x53,
            0x92, 0x1F, 0xEF, 0xD5, 0xC3, 0xE5, 0x53, 0xF1, 0x21, 0x57, 0x75, 0x62, 0x34, 0x37, 0x50, 0xF2,
            0xC3, 0xDE, 0x1A, 0x67, 0x41, 0x0E, 0x13, 0xAC, 0x31, 0xE8, 0x06, 0x89, 0x74, 0xAF, 0x33, 0xF6,
            0xC2, 0x78, 0x68, 0x75, 0x1F, 0xD2, 0xA0, 0xE1, 0xEF, 0xCD, 0x7D, 0x18, 0x5D, 0xA4, 0x9B, 0x3A,
            0x79, 0x51, 0xAB, 0x7F, 0x65, 0xC9, 0xCA, 0x78, 0x40, 0x12, 0x27, 0xE0, 0xEA, 0x24, 0xA1, 0x41,
            0x60, 0x21, 0x49, 0x07, 0x81, 0x18, 0x08, 0xB6, 0x82, 0xDF, 0x0D, 0x33, 0x55, 0xB6, 0xC4, 0x26,
            0x9A, 0xB1, 0xA0, 0xEF, 0xA9, 0xA1, 0x66, 0xEF, 0xAB, 0x8E, 0x5B, 0x30, 0x69, 0x4F, 0x0E, 0xFB,
            0xAC, 0x3A, 0xC7, 0x12, 0xE0, 0x83, 0x97, 0xD9, 0x46, 0xE5, 0x6F, 0x20, 0xEF, 0x26, 0x79, 0xEC,
            0x4B, 0x76, 0xB7, 0xC5, 0xE5, 0x32, 0x82, 0xE3, 0xB3, 0x82, 0xAC, 0xA4, 0xDB, 0x2A, 0x30, 0x35,
            0x58, 0x1A, 0xDC, 0x41, 0xD8, 0x87, 0x88, 0x0B, 0x86, 0x0A, 0xEA, 0x80, 0x1F, 0x46, 0x47, 0xC5,
            0xC1, 0xE5, 0x02, 0x88, 0xCA, 0xA8, 0x09, 0xFE, 0xEE, 0x0C, 0xA2, 0x9D, 0x61, 0xDE, 0x53, 0x87,
            0xCC, 0xC3, 0x86, 0x6D, 0x91, 0xED, 0x46, 0x61, 0xCA, 0x74, 0xF2, 0x53, 0x0D, 0x6F, 0xF2, 0x3D,
            0xB6, 0x0F, 0xCC, 0x26, 0x90, 0x66, 0xFD, 0xAF, 0xEE, 0x96, 0xF4, 0x11, 0x32, 0xAE, 0x02, 0x7B,
            0x26, 0x08, 0x4D, 0x3C, 0x52, 0x5F, 0x4E, 0x7B, 0xEA, 0xAE, 0x7C, 0xA7, 0x63, 0xED, 0x05, 0x88,
            0xED, 0x84, 0x0E, 0x70, 0x50, 0x0B, 0xD7, 0xF4, 0x83, 0x2A, 0xFC, 0xC9, 0x01, 0xD8, 0xC5, 0x1C,
            0x4F, 0x0B, 0x50, 0x94, 0x1F, 0x5C, 0xD0, 0x1C, 0x8B, 0x50, 0x27, 0xF6, 0x9A, 0xE9, 0x4E, 0x2C,
            0xF7, 0x32, 0x72, 0x7B, 0x46, 0xE3, 0x78, 0x02, 0xED, 0x31, 0x70, 0xA2, 0x02, 0x75, 0x71, 0xC5,
            0x65, 0x45, 0x83, 0x7D, 0x1E, 0xB5, 0x9D, 0x5A, 0x2A, 0x9D, 0x4C, 0xD4, 0xF8, 0x2D, 0xDA, 0xAF,
            0xCD, 0x24, 0x70, 0xB5, 0x78, 0xAD, 0xD6, 0x19, 0x43, 0xF5, 0x49, 0x5D, 0xFD, 0x79, 0xA8, 0x1A,
            0xD1, 0x5D, 0x96, 0xD6, 0x93, 0x39, 0x1B, 0xB6, 0x61, 0x10, 0x4D, 0xE6, 0x99, 0x05, 0xBA, 0xEA,
            0xEA, 0xAD, 0x3D, 0x61, 0x5B, 0xE3, 0x7D, 0x1B, 0x8C, 0x47, 0xD9, 0x7D, 0x82, 0xFB, 0x5C, 0x49,
            0xE3, 0x5E, 0xFD, 0xF1, 0x5B, 0x2E, 0x97, 0x73, 0xDA, 0x4C, 0xA4, 0xBF, 0x0A, 0x95, 0xA5, 0xAA,
            0xDB, 0x70, 0x52, 0x6E, 0xBC, 0xE5, 0xEF, 0x14, 0x4E, 0x4B, 0x1D, 0xE9, 0xCE, 0x26, 0x1F, 0xDB,
            0x02, 0x1D, 0x81, 0x38, 0xF3, 0xCD, 0xFF, 0xB4, 0x6A, 0x5E, 0x19, 0xD4, 0x6B, 0xA6, 0xB9, 0xA1
        };
    }
}