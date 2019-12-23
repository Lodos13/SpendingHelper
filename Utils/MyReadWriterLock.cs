using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utils
{
    public class CMyReadWriterLock
    {
        private readonly SemaphoreSlim _writeSemaphore;
        private readonly SemaphoreSlim _readSemaphore;
        private readonly Object _writeLock;
        private readonly Object _readLock;
        private Int32 _writersCounter;
        private Int32 _readersCounter;

        public CMyReadWriterLock()
        {
            _writeSemaphore = new SemaphoreSlim(1, 1);
            _readSemaphore = new SemaphoreSlim(1, 1);
            _writeLock = new Object();
            _readLock = new Object();
            _writersCounter = 0;
            _readersCounter = 0;
        }

        public void EnterReadLock()
        {
            _readSemaphore.Wait();
            lock (_readLock)
            {
                _readersCounter++;

                if (_readersCounter == 1)
                    _writeSemaphore.Wait();
            }
            _readSemaphore.Release();
        }
        public void ExitReadLock()
        {
            lock (_readLock)
            {
                _readersCounter--;

                if (_readersCounter == 0)
                    _writeSemaphore.Release();
            }
        }
        public void EnterWriteLock()
        {
            lock (_writeLock)
            {
                _writersCounter++;

                if (_writersCounter == 1)
                    _readSemaphore.Wait();
            }

            _writeSemaphore.Wait();
        }
        public void ExitWriteLock()
        {
            _writeSemaphore.Release();

            lock (_writeLock)
            {
                _writersCounter--;

                if (_writersCounter == 0)
                    _readSemaphore.Release();
            }
        }
    }
}
