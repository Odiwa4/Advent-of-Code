package day1
import "core:fmt"
import "core:os"
import "core:strconv"
import "core:strings"

day: int : 1
testing: bool : false

main :: proc() {
	day_str := fmt.tprintf("%d", day)
	builder := strings.builder_make()
	strings.write_string(&builder, "2023/inputs/")
	strings.write_string(&builder, day_str)

	if testing {strings.write_string(&builder, "T.txt")} else {strings.write_string(&builder, ".txt")}
	filepath := strings.to_string(builder)
	strings.builder_destroy(&builder)

	data, ok := os.read_entire_file(filepath, context.allocator)
	defer delete(data, context.allocator)

	if !ok {fmt.eprintfln("Error reading input of path: %s", filepath);return}

	input := string(data)

	fmt.printfln("\x1b[%dm----Day %s----\x1b[0m", 31+day%6, day_str)
	fmt.printfln("Part 1: %d", part_one(&input)) //part 1 and 2 can run in either order, the second always doesnt have an input
	fmt.printfln("Part 2: %d", part_two(&input))
}

part_one :: proc(input: ^string) -> int {
	total: int = 0
	input_copy := input^;
	for l in strings.split_lines_iterator(&input_copy) {
		line := strings.trim_null(l)
		first_num := -1
		second_num := -1

		for char in strings.split_iterator(&line, "") {
			num, ok := strconv.parse_int(char, 10)
			if ok {
				if (first_num == -1) {first_num = num}
				if (first_num != -1) {second_num = num}
			}
		}

		builder := strings.builder_make()
		if (second_num != -1) {
			strings.write_int(&builder, first_num)
			strings.write_int(&builder, second_num)
		} else if (first_num == -1 && second_num == -1) {
			continue
		} else {
			strings.write_int(&builder, first_num)
			strings.write_int(&builder, first_num)
		}

		num, ok := strconv.parse_int(strings.to_string(builder), 10)
		strings.builder_destroy(&builder)

		if !ok {fmt.eprintln("Failed to convert what should be a number, to a number, in short: PANIC.")}
		total += num
	}
	return total
}

part_two :: proc(input: ^string) -> int {

	digit_strings := make(map[string]int)
	digit_strings["one"] = 1
	digit_strings["two"] = 2
	digit_strings["three"] = 3
	digit_strings["four"] = 4
	digit_strings["five"] = 5
	digit_strings["six"] = 6
	digit_strings["seven"] = 7
	digit_strings["eight"] = 8
	digit_strings["nine"] = 9
	digit_strings["1"] = 1
	digit_strings["2"] = 2
	digit_strings["3"] = 3
	digit_strings["4"] = 4
	digit_strings["5"] = 5
	digit_strings["6"] = 6
	digit_strings["7"] = 7
	digit_strings["8"] = 8
	digit_strings["9"] = 9
	defer delete(digit_strings)

	total: int = 0
	input_copy := input^;
	for l in strings.split_lines_iterator(&input_copy) {
		line := strings.trim_null(l)
		found := make(map[int]string)
		defer delete(found)
		for key in digit_strings {
			index := strings.index(line, key)

			if (index != -1) {
				found[index] = key
			}

			last_index := strings.last_index(line, key)

			if (last_index != -1) {
				found[last_index] = key
			}
		}

		lowest := 100000
		highest := -100000

		for key in found {
			if (key > highest) {
				highest = key
			}
			if (key < lowest) {
				lowest = key
			}
		}

		builder := strings.builder_make()
		strings.write_int(&builder, digit_strings[found[lowest]])
		strings.write_int(&builder, digit_strings[found[highest]])
		result_str := strings.to_string(builder)
		strings.builder_destroy(&builder)
		num, ok := strconv.parse_int(result_str, 10)

		if !ok {
			fmt.eprintfln(
				"Failed to convert what should be a number, to a number, in short: PANIC. %s",
				result_str,
			)}
		total += num
	}
	return total
}
