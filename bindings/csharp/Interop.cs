// Automatically generated by Interoptopus.

#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if UNITY_2018_1_OR_NEWER
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;
#endif
using Gustaf.UnityWrapper;
#pragma warning restore 0105

namespace Gustaf.UnityWrapper
{
    public static partial class Interop
    {
        public const string NativeLib = "rust_lib";

        static Interop()
        {
        }


        /// Destroys the given instance.
        ///
        /// # Safety
        ///
        /// The passed parameter MUST have been created with the corresponding init function;
        /// passing any other value results in undefined behavior.
        [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ffi_player_struct_destroy")]
        public static extern MyFFIError ffi_player_struct_destroy(ref IntPtr context);

        [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ffi_player_struct_build_default")]
        public static extern MyFFIError ffi_player_struct_build_default(ref IntPtr context);

        [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ffi_player_struct_get_player_name")]
        public static extern IntPtr ffi_player_struct_get_player_name(IntPtr context);

        [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "get_player_data")]
        public static extern void get_player_data(ref SliceMutPlayerData ffi_slice);

        #if UNITY_2018_1_OR_NEWER
        public static void get_player_data(NativeArray<IntPtr> ffi_slice)
        {
            var ffi_slice_slice = new SliceMutPlayerData(ffi_slice);
            get_player_data(ref ffi_slice_slice);;
        }
        #endif

    }

    public enum MyFFIError
    {
        Ok = 0,
        NullPassed = 1,
        Panic = 2,
        OtherError = 3,
    }

    ///A pointer to an array of data someone else owns which may be modified.
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct SliceMutPlayerData
    {
        ///Pointer to start of mutable data.
        #if UNITY_2018_1_OR_NEWER
        [NativeDisableUnsafePtrRestriction]
        #endif
        IntPtr data;
        ///Number of elements.
        ulong len;
    }

    public partial struct SliceMutPlayerData : IEnumerable<IntPtr>
    {
        public SliceMutPlayerData(GCHandle handle, ulong count)
        {
            this.data = handle.AddrOfPinnedObject();
            this.len = count;
        }
        public SliceMutPlayerData(IntPtr handle, ulong count)
        {
            this.data = handle;
            this.len = count;
        }
        #if UNITY_2018_1_OR_NEWER
        public SliceMutPlayerData(NativeArray<IntPtr> handle)
        {
            unsafe
            {
                this.data = new IntPtr(NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(handle));
                this.len = (ulong) handle.Length;
            }
        }
        #endif
        public IntPtr this[int i]
        {
            get
            {
                if (i >= Count) throw new IndexOutOfRangeException();
                unsafe
                {
                    var d = (IntPtr*) data.ToPointer();
                    return d[i];
                }
            }
            set
            {
                if (i >= Count) throw new IndexOutOfRangeException();
                unsafe
                {
                    var d = (IntPtr*) data.ToPointer();
                    d[i] = value;
                }
            }
        }
        public IntPtr[] Copied
        {
            get
            {
                var rval = new IntPtr[len];
                for (var i = 0; i < (int) len; i++) {
                    rval[i] = this[i];
                }
                return rval;
            }
        }
        public int Count => (int) len;
        public IEnumerator<IntPtr> GetEnumerator()
        {
            for (var i = 0; i < (int)len; ++i)
            {
                yield return this[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }



    public partial class PlayerData : IDisposable
    {
        private IntPtr _context;

        private PlayerData() {}

        public static PlayerData BuildDefault()
        {
            var self = new PlayerData();
            var rval = Interop.ffi_player_struct_build_default(ref self._context);
            if (rval != MyFFIError.Ok)
            {
                throw new InteropException<MyFFIError>(rval);
            }
            return self;
        }

        public void Dispose()
        {
            var rval = Interop.ffi_player_struct_destroy(ref _context);
            if (rval != MyFFIError.Ok)
            {
                throw new InteropException<MyFFIError>(rval);
            }
        }

        public string GetPlayerName()
        {
            var s = Interop.ffi_player_struct_get_player_name(_context);
            return Marshal.PtrToStringAnsi(s);
        }

        public IntPtr Context => _context;
    }



    public class InteropException<T> : Exception
    {
        public T Error { get; private set; }

        public InteropException(T error): base($"Something went wrong: {error}")
        {
            Error = error;
        }
    }

}
