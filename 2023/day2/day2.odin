package day2
import "core:fmt"
import "core:os"
import "core:strconv"
import "core:strings"

day: int : 2
testing: bool : false

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

	games := parse_input(&input)

	fmt.printfln("\x1b[%dm----Day %s----\x1b[0m", 31+day%6, day_str)
	fmt.printfln("Part 1: %d", part_one(games))
	fmt.printfln("Part 2: %d", part_two(games))

	round1 := games[0].rounds;
	for g in games{
		delete (g.rounds)
	}
	delete (games)
}

round :: struct {
	r: int,
	g: int,
	b: int,
}

game :: struct {
	id:     int,
	rounds: [dynamic]round,
}

error :: proc() {
	fmt.eprintln(
		"an error occured somewhere where it didnt matter enough to write a unique error message",
	)
}
parse_input :: proc(input: ^string) -> [dynamic]game {
	input_copy := input^
	games := make([dynamic]game)

	i := 0
	for l in strings.split_lines_iterator(&input_copy) {
		i += 1
		line, line_ok := strings.remove_all(l, " ")
		if !line_ok {fmt.eprintln("line_err");break}
		colon_split, colon_ok := strings.split(line, ":")
		if colon_ok != nil {fmt.eprintln("colon_err");break}

		current_game := game{}

		current_game.id = i

		round_split, round_ok := strings.split(colon_split[1], ";")
		if round_ok != nil {fmt.eprintln("round_err");break}

		for round_str in round_split {
			current_round := round{}

			grab_split, grab_ok := strings.split(round_str, ",")
			if grab_ok != nil {fmt.eprintln("grab_err");break}
			for grab in grab_split {
				colour := ""
				if strings.contains(grab, "red") {
					colour = "red"
				} else if strings.contains(grab, "green") {
					colour = "green"
				} else if strings.contains(grab, "blue") {
					colour = "blue";
				}

				num_str, num_str_ok := strings.remove(grab, colour, 1)
				if (!num_str_ok) {fmt.eprintln("num_str_err");break}

				num, num_ok := strconv.parse_int(num_str, 10)
				if (!num_ok) {fmt.eprintln("num_conv_err");break}

				switch (colour){
					case "red":
						current_round.r = num;
					case "green":
						current_round.g = num;
					case "blue":
						current_round.b = num;
				}
			}

			append(&current_game.rounds, current_round);
		}

		append(&games, current_game);
	}
	
	return games
}

part_one :: proc(input: [dynamic]game) -> int {
	total := 0;
	for g in input{
		possible := true;
		for r in g.rounds{
			if r.r > 12 || r.g > 13 || r.b > 14{
				possible = false;
				break;
			}
		}

		if possible{
			total += g.id;
		}
		
	}
	return total;
}

part_two :: proc(input: [dynamic]game) -> int {
total := 0;
	for g in input{
		max_r := 0;
		max_g := 0;
		max_b := 0;

		for r in g.rounds{
			if r.r > max_r{
				max_r = r.r;
			}
			if r.g > max_g{
				max_g = r.g;
			}
			if r.b > max_b{
				max_b = r.b;
			}
		}

		total += max_r * max_g * max_b;
		
	}
	return total;
}
