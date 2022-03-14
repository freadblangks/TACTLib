using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_90774 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[length + 256];
            for (uint i = 0; i != length; ++i)
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
            }
            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = (uint)(2 * digest[5]);
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx -= header.m_buildVersion & 511;
                buffer[i] ^= digest[SignedMod(kidx + header.m_buildVersion, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0xCD, 0xBF, 0xB7, 0xBD, 0x95, 0x05, 0xDD, 0x00, 0x3A, 0x3D, 0xB2, 0x79, 0x4E, 0x29, 0x13, 0x40, 
            0xED, 0xB7, 0xDD, 0x64, 0x53, 0xC8, 0x1A, 0x37, 0x43, 0x3A, 0x9F, 0x5B, 0xA5, 0x8A, 0x3E, 0xF7, 
            0xFA, 0x33, 0xC2, 0x78, 0x0D, 0x89, 0x65, 0xA0, 0x7F, 0xFD, 0xB0, 0xC9, 0xC6, 0x70, 0x1D, 0x92, 
            0x71, 0xFC, 0x18, 0x0C, 0xC0, 0xD4, 0xE1, 0xD3, 0x46, 0x1D, 0x62, 0x7D, 0x32, 0x60, 0xB8, 0x66, 
            0xF4, 0x93, 0x62, 0xD9, 0x09, 0x0D, 0x78, 0x89, 0x88, 0x85, 0x35, 0x1F, 0x17, 0x43, 0x61, 0xA4, 
            0x0C, 0x56, 0x4E, 0x27, 0xDE, 0x51, 0x12, 0xA0, 0x0F, 0x35, 0xCE, 0x44, 0x86, 0x2D, 0x92, 0x32, 
            0x6F, 0xC3, 0x54, 0x8D, 0xAB, 0xF3, 0xF3, 0x94, 0xAB, 0x01, 0x9E, 0x94, 0xD5, 0x91, 0xF3, 0x5B, 
            0x76, 0x76, 0x3A, 0x8B, 0x53, 0xF7, 0x2D, 0xFE, 0xDF, 0x3F, 0x64, 0xF8, 0x79, 0xC3, 0x9D, 0x39, 
            0x94, 0xB2, 0xD0, 0xD5, 0xC7, 0xF6, 0x81, 0xC1, 0xF4, 0x4A, 0xC5, 0xD7, 0x76, 0x5F, 0xB1, 0xF9, 
            0xB8, 0x27, 0x77, 0x7F, 0x81, 0xE6, 0x2D, 0x31, 0x16, 0x6F, 0x36, 0x48, 0x3A, 0x58, 0x91, 0xA5, 
            0x0B, 0x4C, 0x8C, 0x64, 0xC4, 0xE2, 0x06, 0x03, 0xBE, 0xDC, 0x8E, 0x0D, 0xC8, 0xDB, 0x59, 0x40, 
            0x61, 0x8C, 0x1E, 0x61, 0x2B, 0x2F, 0x52, 0x75, 0xAF, 0x08, 0x6E, 0xE2, 0x8D, 0xE1, 0xC6, 0xF9, 
            0xCE, 0x43, 0x5D, 0x8C, 0x6E, 0x81, 0x69, 0xB9, 0x6D, 0xAA, 0xFE, 0x70, 0x3A, 0xB9, 0xA3, 0x8C, 
            0x43, 0x8E, 0x58, 0x65, 0x9A, 0x00, 0xAE, 0x59, 0x61, 0x84, 0xE0, 0xFE, 0xC3, 0xE3, 0x61, 0x70, 
            0xDC, 0xB0, 0xA9, 0xFE, 0x3A, 0x85, 0x5C, 0x95, 0xF0, 0xEE, 0xDF, 0x7F, 0x84, 0x89, 0x2D, 0x66, 
            0x48, 0xAD, 0xED, 0xBA, 0x4D, 0x0B, 0x41, 0x37, 0x92, 0x78, 0xA4, 0xEC, 0xE0, 0x7E, 0x4B, 0x2B, 
            0x8D, 0x4D, 0xC5, 0x22, 0x55, 0x4F, 0x13, 0xB3, 0x80, 0x57, 0xC1, 0x80, 0x03, 0x92, 0xA6, 0xD3, 
            0x9A, 0xD6, 0xD1, 0x10, 0x0B, 0xDD, 0xCB, 0xAE, 0x2C, 0x04, 0x48, 0xE5, 0x2F, 0x86, 0xB0, 0xBB, 
            0x94, 0x48, 0x50, 0xF1, 0xF6, 0xEA, 0x58, 0x5E, 0xAD, 0x1C, 0x8D, 0xDA, 0xF2, 0x0D, 0x33, 0xDB, 
            0xD3, 0x46, 0x20, 0x43, 0xFF, 0x24, 0xE3, 0x39, 0xA2, 0x20, 0xC8, 0xB3, 0xC8, 0xCC, 0x81, 0x14, 
            0x2B, 0xE1, 0x6F, 0xCD, 0xF6, 0x6A, 0x79, 0xCB, 0x6C, 0x90, 0x2E, 0x14, 0x8B, 0x64, 0xA8, 0xC1, 
            0x40, 0x28, 0x55, 0x72, 0x84, 0xE1, 0x4C, 0x6A, 0xC8, 0xF6, 0x14, 0xF5, 0xF0, 0x9A, 0x89, 0x7E, 
            0x4D, 0x85, 0xC3, 0x37, 0x48, 0x0E, 0x94, 0xFC, 0xA9, 0x85, 0xB4, 0x10, 0x7A, 0x93, 0xE9, 0x40, 
            0x23, 0xD7, 0x1E, 0x50, 0x3D, 0x26, 0x6C, 0x1A, 0xD8, 0x2B, 0x66, 0x73, 0xBC, 0x28, 0xCA, 0x6E, 
            0xA6, 0xA2, 0xFA, 0x6C, 0xC4, 0x37, 0xAB, 0x51, 0x88, 0x5C, 0xD4, 0xDF, 0x08, 0xAA, 0x92, 0xCA, 
            0xF6, 0xC8, 0x0B, 0x33, 0x1A, 0xE0, 0x14, 0xD8, 0xC2, 0x1D, 0x6F, 0xAB, 0x5C, 0x3C, 0xD7, 0x84, 
            0xF3, 0xB2, 0x9A, 0x1F, 0x5B, 0x62, 0xAB, 0x6C, 0x70, 0xA4, 0x7D, 0xFF, 0xD6, 0xB3, 0x34, 0xB8, 
            0xC2, 0x32, 0x96, 0xEF, 0x54, 0x2A, 0xEC, 0xA3, 0x96, 0xB5, 0x77, 0x4C, 0xA8, 0xAD, 0x8D, 0x8E, 
            0x03, 0x4B, 0x52, 0x09, 0xA5, 0xEB, 0x12, 0x30, 0x6B, 0xF1, 0xF0, 0x42, 0xF6, 0xCF, 0x56, 0x2B, 
            0xC8, 0x3F, 0x77, 0x3C, 0x62, 0x02, 0x9B, 0x03, 0x79, 0x47, 0xC8, 0x1E, 0x43, 0xE1, 0xF7, 0x10, 
            0xDB, 0x1E, 0x5F, 0xAC, 0x59, 0x81, 0x6D, 0x8F, 0x7F, 0x25, 0xDC, 0x35, 0x14, 0x84, 0x11, 0xA7, 
            0x88, 0x2D, 0x33, 0x03, 0xF0, 0x28, 0xFF, 0x8D, 0x51, 0x41, 0xB8, 0xC3, 0x74, 0x8F, 0x8D, 0xE7
        };
    }
}