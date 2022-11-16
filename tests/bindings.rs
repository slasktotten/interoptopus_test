use interoptopus::util::NamespaceMappings;
use interoptopus::{Error, Interop};
use interoptopus_backend_csharp::Unsafe;

#[test]
fn bindings_csharp() -> Result<(), Error> {
    use interoptopus_backend_csharp::{Config, Generator};
    use interoptopus_backend_csharp::overloads::{Unity};

    let config = Config {
        dll_name: "rust_lib".to_string(),
        namespace_mappings: NamespaceMappings::new("Gustaf.UnityWrapper"),
        use_unsafe: Unsafe::UnsafeKeyword,
        ..Config::default()
    };

    Generator::new(config, rust_lib::my_inventory())
        .add_overload_writer(Unity::new())
        .write_file("bindings/csharp/Interop.cs")?;

    Ok(())
}