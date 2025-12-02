package day6
import "core:fmt"
import "core:os"
import "core:strings"

day: int : 6
testing: bool : true

main :: proc() {
	day_str := fmt.tprintf("%d", day)
	builder := strings.builder_make()
	strings.write_string(&builder, "2023/inputs/")
	strings.write_string(&builder, day_str)

	if testing {strings.write_string(&builder, "T.txt")} else {strings.write_string(&builder, ".txt")}
	filepath := strings.to_string(builder)

	data, ok := os.read_entire_file(filepath, context.allocator)

	if !ok {fmt.eprintfln("Error reading input of path: %s", filepath);return}
	defer delete(data, context.allocator)

	input := string(data)

	fmt.printfln("\x1b[%dm----Day %s----\x1b[0m", 31 + day % 6, day_str)
	fmt.printfln("Part 1: %d", part_one(&input))
	fmt.printfln("Part 2: %d", part_two(&input))
}

race :: struct {
	time:     int,
	distance: int,
}

parse_input :: proc(input: ^string) -> [dynamic]race {
	input_copy := input^
	lines := strings.split_lines(input_copy)
	for c in strings.split(&lines[0], "") {
		
	}

	return -1
}
part_one :: proc(input: ^string) -> int {
	input_copy := input^
	for l in strings.split_lines_iterator(&input_copy) {
		line := strings.trim_Spa
		fmt.println(l)


	}
	return -1
}

part_two :: proc(input: ^string) -> int {
	input_copy := input^
	fmt.println("Not done!")
	return -1
}
