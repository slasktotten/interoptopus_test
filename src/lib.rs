use interoptopus::{ffi_function, ffi_type, Inventory, InventoryBuilder, function, patterns::slice::{FFISliceMut}};
use rand::Rng;

#[ffi_type]
#[repr(C)]
#[derive(Default, Clone)]
pub struct Vec3 {
    pub x: f32,
    pub y: f32,
    pub z: f32,
}


#[ffi_type]
#[repr(C)]
#[derive(Default, Clone)]
pub struct Transform {
    pub translation: Vec3,
    pub rotation: Vec3,
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn mutate_slice_u32(slice: &mut FFISliceMut<u32>) {
    let data = vec![1, 2, 3, 4, 5];
    for (idx, el) in data.iter().enumerate() {
        slice[idx] = *el;
    }
}


#[ffi_function]
#[no_mangle]
pub extern "C" fn mutate_slice_transform(slice: &mut FFISliceMut<Transform>) {
    let data = vec![Transform::default(), Transform::default(), Transform::default(), Transform::default(), Transform::default()];
    for (idx, el) in data.iter().enumerate() {
        slice[idx] = el.clone();
    }
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn get_random_int() -> i32 {
    let mut rng = rand::thread_rng();
    rng.gen_range(0..100)
}

pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .register(function!(get_random_int))
        .register(function!(mutate_slice_u32))
        .register(function!(mutate_slice_transform))
        .inventory()
    }
}