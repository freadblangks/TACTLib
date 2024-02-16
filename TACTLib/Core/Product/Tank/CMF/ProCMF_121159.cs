using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_121159 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[length + 256];
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx = header.m_buildVersion - kidx;
            }
            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[header.m_buildVersion & 511];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                switch (SignedMod(kidx, 3))
                {
                case 0:
                    kidx += 103;
                    break;
                case 1:
                    kidx = (uint)SignedMod(kidx * 4, header.m_buildVersion);
                    break;
                case 2:
                    --kidx;
                    break;
                }
                buffer[i] ^= digest[SignedMod(kidx + header.m_buildVersion, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x21, 0x37, 0x35, 0xD2, 0xA3, 0x45, 0x5F, 0x93, 0xEE, 0x5B, 0x51, 0x5F, 0x40, 0x7B, 0x7C, 0x58, 
            0x40, 0xE2, 0x21, 0x09, 0x58, 0x22, 0xF3, 0x54, 0x60, 0x9B, 0xB6, 0xF6, 0x68, 0x4F, 0x74, 0xC2, 
            0x1D, 0x58, 0x3D, 0xCC, 0x0C, 0xF0, 0x7E, 0xF9, 0x79, 0x4E, 0x03, 0x18, 0x37, 0x7D, 0x3C, 0xB3, 
            0x6E, 0x2E, 0xB9, 0x38, 0x0F, 0xB3, 0xC2, 0x0A, 0x18, 0xA4, 0x1B, 0x4B, 0x41, 0xC3, 0xB7, 0xAB, 
            0x99, 0x6B, 0x8D, 0xB4, 0x9D, 0x38, 0x6C, 0x8C, 0xC7, 0x92, 0xA8, 0x90, 0x34, 0xE5, 0x18, 0x7D, 
            0x30, 0x1A, 0x82, 0x5F, 0x5F, 0x66, 0xF7, 0x5C, 0x83, 0xA3, 0xA9, 0xCC, 0x41, 0xA3, 0x4A, 0x1E, 
            0x39, 0x63, 0xF1, 0x2D, 0x81, 0x0A, 0x97, 0x4E, 0x12, 0x3E, 0x87, 0xF3, 0x08, 0x3D, 0x3A, 0xA0, 
            0xC6, 0x30, 0xD6, 0xD0, 0x84, 0xED, 0x2D, 0x5C, 0xB6, 0x25, 0xA9, 0xF4, 0x81, 0x4A, 0x82, 0x29, 
            0x9B, 0xAB, 0xC8, 0x64, 0x9E, 0x1F, 0x09, 0xB5, 0xF0, 0x5F, 0x9F, 0xA2, 0x6A, 0x3B, 0x0F, 0x73, 
            0xD5, 0xBA, 0x6D, 0x1A, 0x5B, 0x2C, 0x18, 0x53, 0x04, 0xA0, 0xD5, 0xC9, 0xB7, 0x22, 0xD0, 0x89, 
            0x10, 0x46, 0x92, 0xDC, 0xB3, 0xAB, 0xF5, 0xA1, 0x51, 0xA7, 0x2A, 0x13, 0x58, 0x23, 0x21, 0xBB, 
            0xF3, 0x15, 0x70, 0xA1, 0x52, 0xB7, 0x46, 0xA1, 0xEC, 0xF9, 0x9C, 0xAB, 0x1A, 0x26, 0xFD, 0x3E, 
            0xFB, 0x02, 0xCE, 0xB8, 0x2F, 0x7B, 0xD3, 0x8A, 0x59, 0x16, 0xB4, 0xE4, 0x64, 0xAB, 0xFF, 0x5C, 
            0x1B, 0x3F, 0xB2, 0x4C, 0x72, 0x55, 0x93, 0x2B, 0x44, 0x37, 0x11, 0x74, 0x3D, 0x9F, 0x9C, 0x3D, 
            0x96, 0xF1, 0xDB, 0x5A, 0x48, 0xEF, 0xFF, 0x27, 0xEE, 0x4A, 0x7C, 0x0B, 0x9D, 0xD1, 0xB0, 0xCF, 
            0xE0, 0x00, 0x9A, 0xB9, 0x74, 0x80, 0xC2, 0x6D, 0xBF, 0x04, 0x94, 0x84, 0x40, 0x49, 0xBE, 0x4F, 
            0x7E, 0x28, 0x08, 0xD1, 0x3A, 0xF4, 0x72, 0x3A, 0xBB, 0x31, 0x4E, 0x28, 0xA3, 0x68, 0xC7, 0x0E, 
            0xE0, 0x9A, 0xE7, 0xFF, 0xF4, 0xB6, 0x48, 0x00, 0x60, 0x00, 0x0A, 0xD7, 0x96, 0x7B, 0xC2, 0xA1, 
            0xE9, 0x85, 0xEA, 0xDE, 0xD2, 0x58, 0x2D, 0x70, 0x84, 0x58, 0x68, 0xAC, 0x99, 0xC8, 0x1A, 0x7C, 
            0xFD, 0x13, 0x20, 0x10, 0x5E, 0xF7, 0x52, 0xF4, 0xBF, 0x42, 0x53, 0x9F, 0xD4, 0x5E, 0x6A, 0xF5, 
            0xDB, 0x0D, 0xBE, 0x7F, 0x27, 0xB9, 0xB2, 0xE2, 0x2E, 0x1D, 0x81, 0x88, 0xF2, 0xC2, 0xA8, 0x8F, 
            0x6B, 0x98, 0xC7, 0x22, 0x20, 0xD7, 0x3B, 0x82, 0x0A, 0x93, 0xBC, 0x01, 0xC2, 0xB5, 0x54, 0xA4, 
            0xB3, 0xDA, 0x9B, 0x47, 0xE2, 0x07, 0x99, 0x70, 0x13, 0x1C, 0x17, 0xD1, 0xFE, 0xC2, 0x00, 0xB7, 
            0x49, 0x16, 0x0B, 0x12, 0x61, 0x3D, 0xF8, 0xAC, 0x0E, 0x64, 0x0B, 0x3E, 0xB2, 0x55, 0x4B, 0x97, 
            0x6B, 0xE0, 0xBA, 0x2F, 0x8E, 0xB5, 0x77, 0x50, 0x39, 0xFB, 0xDA, 0x15, 0xF6, 0x28, 0x98, 0x40, 
            0x5A, 0x71, 0x61, 0xCE, 0xE6, 0x65, 0xF0, 0xFA, 0x8D, 0xEA, 0x77, 0xE9, 0xE9, 0x7B, 0xFC, 0x21, 
            0x63, 0x36, 0xF9, 0x36, 0x73, 0x33, 0xD9, 0x52, 0xA9, 0xAF, 0x63, 0x5A, 0xF0, 0xC1, 0x79, 0xF6, 
            0xC7, 0xC0, 0x3B, 0x8C, 0xBB, 0x4F, 0x9C, 0x14, 0xEF, 0x9C, 0x69, 0x88, 0xA3, 0x5F, 0x27, 0x11, 
            0x2C, 0x0A, 0xBA, 0x02, 0xA1, 0x36, 0x1B, 0x3D, 0x4D, 0xD9, 0x42, 0x9F, 0xBE, 0xA6, 0xA4, 0xC0, 
            0x2D, 0xE1, 0x82, 0x42, 0x8E, 0xE0, 0xFE, 0xCE, 0x1E, 0x79, 0x3C, 0x1B, 0xBC, 0x02, 0xFB, 0xED, 
            0xEE, 0xBE, 0x7D, 0xC0, 0xE0, 0x97, 0x6E, 0xF6, 0xDE, 0xA5, 0x49, 0x4F, 0x38, 0x32, 0x24, 0x82, 
            0xEA, 0x34, 0xC3, 0x91, 0x82, 0xD5, 0x2E, 0x61, 0x25, 0xC7, 0xAF, 0x18, 0x6F, 0x6E, 0x49, 0xCE
        };
    }
}