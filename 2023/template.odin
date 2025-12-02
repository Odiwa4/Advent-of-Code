package main
import "core:os"
import "core:fmt"
import "core:strings"

day: int : 1
testing: bool : true

replace_with_main :: proc() {
	day_str := fmt.tprintf("%d", day)
	builder := strings.builder_make()
	strings.write_string(&builder, "2023/inputs/")
	strings.write_string(&builder, day_str)

	if testing {strings.write_string(&builder, "T.txt")} else {strings.write_string(&builder, ".txt")}
	filepath := strings.to_string(builder)

	data, ok := os.read_entire_file(filepath, context.allocator)

	if !ok {fmt.eprintfln("Error reading input of path: %s", filepath); return}
	defer delete(data, context.allocator)

	input := string(data);

	fmt.printfln("\x1b[%dm----Day %s----\x1b[0m", 31+day%6, day_str)
	fmt.printfln("Part 1: %d", part_one(&input))
	fmt.printfln("Part 2: %d", part_two(&input))
}

part_one :: proc(input : ^string) -> int{
	input_copy := input^;
	for l in strings.split_lines_iterator(&input_copy){
		// line := strings.trim_null(l);
	}
	return -1;
}

part_two :: proc(input: ^string) -> int {
	input_copy := input^;
	fmt.println("Not done!")
	return -1
}
