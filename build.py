#!/usr/bin/env python3
# Build script for building and packaging all artifacts and other resources
import argparse
import os
import subprocess
import shutil


parser = argparse.ArgumentParser(prog='TrembleBuildScript')
parser.add_argument('-c', '--config')

args = parser.parse_args()
config = args.config or "debug"

# Build rust lib first, as C# project relies on C# bindings to already exist
# TODO: Put this in its own build script for trembleLib
f = open("ext/tremble_lib/src/error/expanded.g.rs", 'w+')
subprocess.run(["cargo", "expand", "--manifest-path", "ext/tremble_lib/Cargo.toml", "--release", "--features", "disable_gen", "error"], stdout=f)
if config == "release":
   subprocess.run(["cargo", "build", "--manifest-path", "ext/tremble_lib/Cargo.toml", "--release"])
else:
   subprocess.run(["cargo", "build", "--manifest-path", "ext/tremble_lib/Cargo.toml"])

# Now build the CSharp project
subprocess.run(["dotnet", "build", "--configuration", "{}".format(config)])

# Copy library to output path
print("Copying required files to build folder...")
lib_ext = 'dll' if os.name == 'nt' else 'so'
lib_prefix = '' if os.name == 'nt' else 'lib'
shutil.copyfile("ext/tremble_lib/target/{}/{}tremble.{}".format(config, lib_prefix, lib_ext), 
               "out/bin/TrembleEngine/{}/{}tremble.{}".format(config, lib_prefix, lib_ext))

print("Build completed successfully")