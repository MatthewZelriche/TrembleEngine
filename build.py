# Build script for building and packaging all artifacts and other resources
import argparse
import os
import subprocess
import shutil


parser = argparse.ArgumentParser(prog='TrembleBuildScript')
parser.add_argument('-c', '--config')

args = parser.parse_args()
config = args.config or "debug"

subprocess.run(["dotnet", "build", "--configuration", "{}".format(config)])
if config == "release":
   subprocess.run(["cargo", "build", "--manifest-path", "ext/tremble_lib/Cargo.toml", "--release"])
else:
   subprocess.run(["cargo", "build", "--manifest-path", "ext/tremble_lib/Cargo.toml"])

# Copy library to output path
print("Copying required files to build folder...")
lib_ext = 'dll' if os.name == 'nt' else 'so'
shutil.copyfile("ext/tremble_lib/target/{}/tremble_lib.{}".format(config, lib_ext), "out/bin/TrembleEngine/{}/tremble_lib.{}".format(config, lib_ext))

print("Build completed successfully")