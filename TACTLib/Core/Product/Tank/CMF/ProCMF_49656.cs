﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_49656 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.DataCount & 511];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                switch (SignedMod(kidx, 3))
                {
                    case 0:
                        kidx += 103;
                        break;
                    case 1:
                        kidx = (uint)SignedMod(4 * kidx, header.BuildVersion);
                        break;
                    case 2:
                        --kidx;
                        break;
                }
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            int kidx = 2 * digest[5];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx -= 504;
                buffer[i] ^= digest[SignedMod(kidx + header.BuildVersion, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x60, 0xE6, 0x1A, 0x16, 0xED, 0x45, 0x78, 0x1C, 0x3D, 0x4B, 0xA3, 0xFA, 0x8F, 0xFD, 0xD0, 0x75, 
            0x31, 0x77, 0x79, 0xB5, 0x87, 0xBE, 0xB1, 0x41, 0x60, 0xA9, 0xCC, 0x39, 0x17, 0x12, 0xB5, 0x2D, 
            0x16, 0xEE, 0x88, 0xD2, 0xF4, 0x58, 0xE3, 0x9B, 0xA8, 0x71, 0xB0, 0xD8, 0x39, 0x93, 0x2F, 0xCA, 
            0xD7, 0x1B, 0x36, 0xF5, 0x72, 0x08, 0xA8, 0x4F, 0xD3, 0x1A, 0x78, 0xDB, 0x57, 0x87, 0xBB, 0x72, 
            0x1A, 0x24, 0x4D, 0xEE, 0x1E, 0xBA, 0x5E, 0xF9, 0x5D, 0x3B, 0x20, 0x97, 0xB5, 0x24, 0xD9, 0x0A, 
            0x9C, 0xAB, 0x1B, 0x10, 0x0A, 0xD4, 0xD5, 0x76, 0xEF, 0xFD, 0x38, 0x40, 0x1E, 0x34, 0xFD, 0xFF, 
            0x9B, 0xA5, 0x46, 0x3C, 0x25, 0xAD, 0x82, 0xBA, 0x5E, 0xA2, 0xB7, 0x78, 0xE3, 0xB6, 0x15, 0x4B, 
            0x3F, 0xC1, 0xFC, 0xEC, 0xEF, 0x60, 0xBC, 0xEE, 0x07, 0x58, 0xDC, 0xA2, 0x0B, 0xBC, 0x00, 0x35, 
            0x96, 0x3D, 0x39, 0xAE, 0x00, 0xDA, 0x82, 0x6A, 0x08, 0x1E, 0x0B, 0xFF, 0x0F, 0x6A, 0x3E, 0xF6, 
            0x88, 0x2D, 0xCC, 0x3F, 0xD8, 0x79, 0xDC, 0x8B, 0xEB, 0xCE, 0xAD, 0x28, 0x26, 0xB7, 0x60, 0xD7, 
            0x3F, 0xC5, 0xDF, 0xA1, 0xAB, 0xB2, 0x62, 0xD1, 0xA0, 0x56, 0x0C, 0x62, 0x36, 0x07, 0xA0, 0x48, 
            0x0F, 0xC4, 0xD7, 0x12, 0xCB, 0xAD, 0x42, 0x5B, 0x3F, 0x2A, 0xDC, 0xCC, 0xEE, 0x69, 0xAB, 0xF4, 
            0xBE, 0x7E, 0x4E, 0x0B, 0x1B, 0xF3, 0x02, 0xF7, 0xE5, 0x8A, 0x7B, 0xBA, 0xF4, 0xCD, 0xFE, 0xBC, 
            0x82, 0x30, 0x05, 0xCE, 0x6F, 0x0B, 0x21, 0xE6, 0xE2, 0x36, 0xE1, 0xEE, 0x65, 0x85, 0x6F, 0x04, 
            0xDD, 0x63, 0x33, 0xB9, 0x59, 0x6C, 0x3E, 0xFD, 0x7B, 0xF0, 0x12, 0x09, 0x98, 0x35, 0xBF, 0xD4, 
            0x03, 0xC7, 0x26, 0x74, 0x22, 0x59, 0x42, 0xB1, 0xEC, 0x72, 0x60, 0x3A, 0x25, 0x22, 0xB0, 0x55, 
            0xDD, 0x4A, 0x34, 0x8D, 0xED, 0x5B, 0x9F, 0x8B, 0x39, 0x70, 0x3C, 0x3C, 0xDB, 0x89, 0x06, 0xCC, 
            0x69, 0x1F, 0x59, 0x25, 0x56, 0x1D, 0x5D, 0xE2, 0x74, 0xAD, 0xFF, 0x34, 0x27, 0xBD, 0xF4, 0xBA, 
            0x60, 0xFC, 0x59, 0xF5, 0x42, 0xB1, 0x2F, 0x42, 0x9B, 0x3A, 0xF9, 0xC6, 0xF7, 0x7D, 0x6C, 0xAC, 
            0x37, 0x56, 0xF3, 0x45, 0xE1, 0xB5, 0x52, 0x42, 0x08, 0x1B, 0xC4, 0xBA, 0x27, 0xB3, 0x5F, 0x88, 
            0x28, 0x01, 0x51, 0xE8, 0x04, 0xB9, 0x68, 0x0C, 0x3C, 0xD4, 0x5D, 0x25, 0xA3, 0x6E, 0xE1, 0x8D, 
            0xFC, 0xA0, 0x8B, 0x6F, 0x71, 0x13, 0xFD, 0xD9, 0x65, 0xB2, 0x01, 0xF0, 0x3D, 0xAD, 0xA2, 0xDD, 
            0xD7, 0x10, 0xA4, 0x67, 0xB3, 0x39, 0x21, 0x89, 0xB8, 0x17, 0x61, 0x5B, 0xDB, 0x60, 0x48, 0x86, 
            0x4E, 0xAA, 0x0E, 0xA4, 0xAC, 0x0F, 0xB2, 0x79, 0xF4, 0x81, 0xDD, 0xF9, 0x0F, 0x66, 0xE2, 0x7C, 
            0x90, 0x4F, 0x63, 0x9F, 0xA3, 0x9B, 0x76, 0x02, 0x0D, 0x86, 0xA3, 0x0D, 0x7D, 0x8C, 0xFE, 0xCA, 
            0xBD, 0xB8, 0x52, 0x2E, 0xF3, 0x66, 0xB9, 0xC6, 0x2B, 0xC4, 0xFE, 0x5C, 0x23, 0xC7, 0x45, 0xD5, 
            0x54, 0xFA, 0xD0, 0x7D, 0xB6, 0xC4, 0x6D, 0x55, 0x31, 0x61, 0xEC, 0xFC, 0x28, 0xE0, 0x28, 0x90, 
            0xA5, 0xD0, 0xB6, 0x3F, 0x52, 0x00, 0xCC, 0x6A, 0x88, 0x11, 0x06, 0xB0, 0x8E, 0x62, 0xD5, 0xA6, 
            0xAE, 0x39, 0x72, 0x3C, 0x72, 0x80, 0x6C, 0x4F, 0xF9, 0x96, 0xA0, 0x7B, 0x82, 0x1A, 0x96, 0x57, 
            0xC4, 0x8F, 0xB6, 0x0B, 0xB8, 0x90, 0x3F, 0x6F, 0xDF, 0xB0, 0x49, 0xDC, 0xB8, 0x2F, 0xCE, 0x70, 
            0x6E, 0x37, 0x93, 0x8F, 0x9E, 0x80, 0x86, 0xA5, 0xAC, 0x9E, 0x76, 0x68, 0x99, 0xE1, 0x27, 0x2E, 
            0xD8, 0x4D, 0x57, 0x4D, 0xB2, 0xF8, 0x95, 0xE2, 0xE5, 0xD0, 0xB7, 0x4F, 0x6D, 0x8A, 0xED, 0xB1
        };
    }
}