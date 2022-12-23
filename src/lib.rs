
use std::ffi::CString;

use interoptopus::{ffi_function, ffi_type, Inventory, InventoryBuilder, function, patterns::{slice::{FFISliceMut, FFISlice}, string::AsciiPointer, result::FFIError}, ffi_service, ffi_service_ctor, ffi_service_method, pattern};
use rand::Rng;


#[ffi_type(opaque)]
#[derive(Default, Clone)]
pub struct PlayerData {
    c_string: CString,
    id: u32,
}

#[ffi_service(error = "MyFFIError", prefix = "ffi_player_struct_")]
impl PlayerData {
    #[ffi_service_ctor]
    pub fn build_default() -> Result<Self, Error> {   
        Ok(Self::default())
    }

    #[ffi_service_method(on_panic = "return_default")]
    pub fn get_player_name(&self) -> AsciiPointer{
        AsciiPointer::from_cstr(&self.c_string)
    }
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn get_player_data(ffi_slice: &mut FFISliceMut<PlayerData>) {
    // data we want to access in C# 
    ffi_slice.clone_from_slice(test_data().as_slice());
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn get_player_len() -> u64 {
    test_data().len() as u64
}

fn test_data() -> Vec<PlayerData> {
    vec![
        PlayerData { c_string: CString::new("foo").unwrap(), id: 1 },
        PlayerData { c_string: CString::new("bar").unwrap(), id: 2 }
    ]
}





//// BOILER PLATE BELOW 

/// FFI ERRORS

// Some Error used in your application.
#[derive(Debug)]
pub enum Error {
    Bad,
}

// The error FFI users should see
#[ffi_type(patterns(ffi_error))]
#[repr(C)]
pub enum MyFFIError {
    Ok = 0,
    NullPassed = 1,
    Panic = 2,
    OtherError = 3,
}

// Gives special meaning to some of your error variants.
impl FFIError for MyFFIError {
    const SUCCESS: Self = Self::Ok;
    const NULL: Self = Self::NullPassed;
    const PANIC: Self = Self::Panic;
}

// How to map an `Error` to an `MyFFIError`.
impl From<Error> for MyFFIError {
    fn from(x: Error) -> Self {
        match x {
            Error::Bad => Self::OtherError,
        }
    }
}

/// EOF FFI ERRORS


pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .register(pattern!(PlayerData))
        .register(function!(get_player_data))
        .inventory()
    }
}