﻿using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_51948 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = header.m_buildVersion * (uint)length;
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += (uint)header.m_entryCount;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[SignedMod((2 * digest[7]) - length, 512)];
            uint increment = (digest[6] & 1) == 1 ? 37 : kidx % 61;
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += increment;
                buffer[i] ^= digest[SignedMod(kidx - i, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0xA3, 0xB0, 0xE5, 0x73, 0x2D, 0x28, 0x05, 0xEC, 0xD1, 0xF3, 0x32, 0x89, 0xFE, 0xEA, 0x81, 0x51, 
            0xB2, 0x42, 0x02, 0x81, 0x74, 0x3B, 0xBF, 0x21, 0xED, 0x0A, 0x7E, 0x77, 0x82, 0x82, 0xF0, 0x0A, 
            0x18, 0xAD, 0x4A, 0x01, 0x79, 0xFD, 0x07, 0x23, 0x27, 0x7B, 0x57, 0x9F, 0x42, 0x81, 0x44, 0x4B, 
            0x2D, 0x7E, 0xC4, 0x7C, 0x31, 0x6E, 0x43, 0x3D, 0x1D, 0x61, 0x64, 0xC1, 0x52, 0x3E, 0x07, 0xC4, 
            0x42, 0xBC, 0x4E, 0x9D, 0xCB, 0x5C, 0xD3, 0x84, 0xFF, 0xB3, 0x71, 0xD3, 0xF6, 0x33, 0xEB, 0x3B, 
            0x48, 0x9E, 0x15, 0x2C, 0x3C, 0x68, 0x66, 0x7A, 0x8A, 0xA4, 0xDE, 0x58, 0xFC, 0x36, 0xE4, 0x20, 
            0x70, 0x7C, 0x6B, 0x78, 0xE9, 0x59, 0x81, 0xA9, 0x3B, 0x89, 0xF4, 0x4A, 0x2C, 0x52, 0x56, 0x74, 
            0xE6, 0x8F, 0xD7, 0x2E, 0xDA, 0x7E, 0x1A, 0x0A, 0xE1, 0xF0, 0xA1, 0x54, 0xF9, 0x6B, 0x8A, 0xDF, 
            0x4F, 0x1F, 0x7D, 0x84, 0x7D, 0x7A, 0x86, 0xB9, 0x00, 0x65, 0x75, 0x46, 0xBC, 0x38, 0x57, 0xF0, 
            0x19, 0x6F, 0xE2, 0xB1, 0xF8, 0xE5, 0x30, 0xE5, 0x6D, 0xA9, 0x5C, 0xF4, 0x81, 0x18, 0x39, 0x9B, 
            0x8F, 0xBE, 0x09, 0x00, 0xF8, 0xF3, 0x17, 0x64, 0x2E, 0xF1, 0xB8, 0x7A, 0x75, 0xDA, 0xB6, 0x63, 
            0x99, 0x7D, 0x8F, 0x6E, 0x67, 0xCA, 0x55, 0x74, 0xBE, 0x19, 0xBF, 0x1A, 0xD8, 0x40, 0x05, 0x70, 
            0x57, 0x06, 0x77, 0x98, 0x66, 0x09, 0x79, 0x6F, 0x62, 0xD6, 0xAA, 0xBD, 0x37, 0x24, 0x0B, 0xE5, 
            0x10, 0xBC, 0x51, 0xC5, 0xEB, 0xAA, 0xA7, 0x87, 0xEA, 0xEC, 0x1C, 0x60, 0x06, 0x0C, 0x29, 0xD1, 
            0x48, 0x75, 0xCB, 0xC2, 0x18, 0x35, 0x25, 0x97, 0xEA, 0xB0, 0x1C, 0x19, 0x48, 0xB6, 0x7A, 0x20, 
            0xC4, 0x5A, 0xD9, 0x2C, 0x25, 0xF4, 0xE1, 0xDC, 0xF9, 0x35, 0x88, 0x14, 0x2C, 0xC7, 0x53, 0x5F, 
            0xA8, 0x58, 0x2F, 0xA0, 0x40, 0xDE, 0x92, 0x3F, 0xAF, 0x30, 0x7C, 0x78, 0x0B, 0xEE, 0x18, 0xA1, 
            0x41, 0xC6, 0x88, 0x24, 0xE9, 0x4C, 0x1C, 0x46, 0x47, 0xC4, 0xF2, 0x14, 0xC1, 0xB8, 0xCC, 0xA3, 
            0x77, 0xE9, 0x34, 0xA3, 0x22, 0x5C, 0xBA, 0x5F, 0x03, 0xF3, 0xFB, 0x62, 0xD3, 0x9D, 0xF2, 0xD3, 
            0xC5, 0xE4, 0x19, 0xC1, 0x83, 0x8D, 0x53, 0x7F, 0x0A, 0xB2, 0x21, 0xAB, 0x2C, 0x37, 0x2C, 0x31, 
            0x06, 0xEC, 0x58, 0x96, 0xC7, 0x85, 0x67, 0x7D, 0x89, 0xEE, 0xD9, 0x64, 0x2B, 0xAB, 0xC6, 0x1E, 
            0xA8, 0xC9, 0x59, 0x8E, 0x1F, 0x51, 0x2E, 0x5A, 0xC7, 0xCD, 0xC5, 0x9A, 0x87, 0x7D, 0x2C, 0x3D, 
            0x91, 0xA9, 0x56, 0x5C, 0xFF, 0xFC, 0xBA, 0x05, 0xB0, 0x86, 0x7B, 0xA8, 0xEB, 0x79, 0x60, 0xA9, 
            0x56, 0x88, 0x22, 0xE1, 0x89, 0x9A, 0x4C, 0x14, 0x3C, 0x6E, 0xB7, 0x2C, 0xDA, 0x79, 0xEE, 0x0A, 
            0x05, 0x2D, 0x20, 0xB5, 0xDD, 0xBE, 0x15, 0x14, 0x9C, 0xEF, 0xCB, 0x20, 0xAF, 0x8C, 0xE5, 0x03, 
            0x1A, 0x4C, 0xA7, 0xFD, 0x10, 0x72, 0x17, 0xD8, 0xBD, 0xD8, 0x86, 0x84, 0x26, 0xB7, 0xEC, 0x21, 
            0x1F, 0xA9, 0x15, 0xEB, 0x40, 0xE1, 0xB5, 0x87, 0xC6, 0x96, 0x3E, 0x75, 0xB0, 0x0B, 0xE3, 0x81, 
            0x61, 0x49, 0x67, 0x76, 0xAF, 0x7E, 0x6C, 0x50, 0x74, 0xCC, 0xBE, 0x69, 0xA4, 0x16, 0xCC, 0xA3, 
            0x78, 0x2C, 0x2E, 0x69, 0x2D, 0x3D, 0xFA, 0x58, 0x01, 0x8B, 0x1F, 0xB5, 0xBA, 0x67, 0x92, 0xBE, 
            0xD3, 0x2D, 0x6D, 0x88, 0xB8, 0xAB, 0x99, 0xCE, 0x76, 0xA2, 0x60, 0xE6, 0x17, 0xCC, 0xF0, 0xB6, 
            0x0E, 0x83, 0xB1, 0xEE, 0x7B, 0x71, 0x57, 0xB3, 0x70, 0x8B, 0x7A, 0x28, 0x59, 0x02, 0x3F, 0xD1, 
            0x76, 0x7C, 0x5A, 0x57, 0x8D, 0x95, 0x8D, 0x41, 0x0D, 0xC2, 0xCE, 0x4C, 0x6E, 0x44, 0xE0, 0x03
        };
    }
}