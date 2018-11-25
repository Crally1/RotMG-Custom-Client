using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Crypto
{
    public class RC4
    {
        private const int N = 256;
        private int[] _sbox;
        private readonly byte[] _seedKey;
        private byte[] _text;
        private int _i, _j;

        public RC4(byte[] seedKey, byte[] text)
        {
            _seedKey = seedKey;
            _text = text;
            Rc4Initialize();
        }

        public RC4(byte[] seedKey)
        {
            _seedKey = seedKey;
            Rc4Initialize();
        }

        public byte[] Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public byte[] EnDeCrypt()
        {
            List<byte> bytes = new List<byte>();

            foreach (var t in _text)
            {
                var k = GetNextKeyByte();

                var cipherBy = (t) ^ k;

                bytes.Add((byte)cipherBy);
            }

            return bytes.ToArray();
        }

        public byte GetNextKeyByte()
        {
            _i = (_i + 1) % N;
            _j = (_j + _sbox[_i]) % N;

            var tempSwap = _sbox[_i];

            _sbox[_i] = _sbox[_j];
            _sbox[_j] = tempSwap;

            var k = _sbox[(_sbox[_i] + _sbox[_j]) % N];

            return (byte)k;
        }

        public void Rc4Initialize()
        {
            Initialize();
        }

        public void Rc4Initialize(int drop)
        {
            Initialize();

            for (var i = 0; i < drop; i++)
            {
                GetNextKeyByte();
            }
        }

        private void Initialize()
        {
            _i = 0;
            _j = 0;

            _sbox = new int[N];
            var key = new int[N];

            for (var a = 0; a < N; a++)
            {
                key[a] = _seedKey[a % _seedKey.Length];
                _sbox[a] = a;
            }

            var b = 0;
            for (var a = 0; a < N; a++)
            {
                b = (b + _sbox[a] + key[a]) % N;
                var tempSwap = _sbox[a];

                _sbox[a] = _sbox[b];
                _sbox[b] = tempSwap;
            }
        }
    }
}
