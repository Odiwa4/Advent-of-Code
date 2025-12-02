const std = @import("std");

pub fn build(b: *std.Build) void {
    const exe = b.addExecutable(.{
        .name = "aoc_2023",
        .root_source_file = b.path("2023/main.zig"),
        .target = b.standardTargetOptions(.{}),
        .optimize = .Debug,
        .version = .{ .major = 0, .minor = 0, .patch = 1 },
    });

    b.installArtifact(exe);
    const run_artifact = b.addRunArtifact(exe);

    const run_step = b.step("run", "Run the project");

    run_step.dependOn(&run_artifact.step);
}
